using AutoMapper;
using RW.PMS.Application.Contracts.Basics;
using System;
using System.Collections.Generic;
using System.Linq;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Basics;
using RW.PMS.Application.Contracts.DTO.Configuration;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Basics;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using RW.PMS.Application.Contracts.DTO.Problem;
using System.Dynamic;

namespace RW.PMS.Application.Basics
{
    public class ToolService : CrudApplicationService<ToolEntity, int>, IToolService
    {
        private readonly IToolDetailService _toolDetailService;
        public ToolService(IDataValidatorProvider dataValidator,
                           IRepository<ToolEntity, int> productionRepository,
                           IMapper mapper,
                           Lazy<ICurrentUser> currentUser,
                           IToolDetailService toolDetailService) :
          base(dataValidator, productionRepository, mapper, currentUser)
        {
            _toolDetailService = toolDetailService;
        }

        public PagedResult<ToolDto> GetPagedList(ToolSearchDto input)
        {
            var list = Repository.Select.From<DictionaryEntity, DictionaryEntity>((r, d1, d2) => r.LeftJoin(rd => rd.toolClassID == d1.Id).LeftJoin(rc => rc.toolTypeID == d2.Id))
                .WhereIf(input.toolName.NotNullOrWhiteSpace(), (r, d1, d2) => r.toolName.Contains(input.toolName))
                .Count(out var total).Page(input.PageNo, input.PageSize).ToList((r, d1, d2) => new {
                    model = r,
                    toolClassTxt = d1.DictionaryText,
                    toolTypeTxt = d2.DictionaryText,
                }).Select(t =>
                {
                    var mapp = Mapper.Map<ToolDto>(t.model);
                    mapp.toolClassTxt = t.toolClassTxt;
                    mapp.toolTypeTxt = t.toolTypeTxt;
                    return mapp;
                }).ToList();

            var pList = _toolDetailService.GetList().Where(a => a.IsDeleted == false);
            var ResultList = new List<ToolDto>(list);


            foreach (var pItem in pList)
            {
                foreach (var item in ResultList)
                {
                    if (pItem.PID == item.Id)
                    {
                        item.PicId = new ExpandoObject();
                        item.PicId.value = pItem.Id;
                        item.PicId.imgUrl = pItem.RelativePath;
                    }
                }
            }

            return new PagedResult<ToolDto>(Mapper.Map<List<ToolDto>>(list), total);
        }

        public bool Repeatjudgment(ToolSearchDto input)
        {
            return Repository.Select.WhereIf(input.Id.HasValue, r => r.Id != input.Id).Where(r => r.toolCode == input.toolCode).Count() > 0;
        }
    }
}
