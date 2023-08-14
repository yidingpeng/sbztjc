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
    public class MaterialService : CrudApplicationService<MaterialEntity, int>, IMaterialService
    {
        private readonly IMaterialDetailService _materialDetailService;
        public MaterialService(IDataValidatorProvider dataValidator,
                           IRepository<MaterialEntity, int> productionRepository,
                           IMapper mapper,
                           Lazy<ICurrentUser> currentUser,
                           IMaterialDetailService materialDetailService) :
          base(dataValidator, productionRepository, mapper, currentUser)
        {
            _materialDetailService = materialDetailService;
        }

        public PagedResult<MaterialDto> GetPagedList(MaterialSearchDto input)
        {
            var list = Repository.Select.From<DictionaryEntity, DictionaryEntity, DictionaryEntity, DictionaryEntity, DictionaryEntity>((r, d1, d2, d3, d4, d5) =>
            r.LeftJoin(rd => rd.MtlClassID == d1.Id).LeftJoin(rc => rc.MtlTypeID == d2.Id).LeftJoin(rc => rc.baseUnitID == d3.Id).LeftJoin(rc => rc.importance == d4.Id).LeftJoin(rc => rc.texture == d5.Id))
                .WhereIf(input.MtlName.NotNullOrWhiteSpace(), (r, d1, d2, d3, d4, d5) => r.MtlName.Contains(input.MtlName))
                .Count(out var total).Page(input.PageNo, input.PageSize).ToList((r, d1, d2, d3, d4, d5) => new
                {
                    model = r,
                    MtlClassTxt = d1.DictionaryText,
                    MtlTypeTxt = d2.DictionaryText,
                    baseUnitTxt = d3.DictionaryText,
                    importanceTxt = d4.DictionaryText,
                    textureTxt = d5.DictionaryText,
                }).Select(t =>
                {
                    var mapp = Mapper.Map<MaterialDto>(t.model);
                    mapp.MtlClassTxt = t.MtlClassTxt;
                    mapp.MtlTypeTxt = t.MtlTypeTxt;
                    mapp.baseUnitTxt = t.baseUnitTxt;
                    mapp.importanceTxt = t.importanceTxt;
                    mapp.textureTxt = t.textureTxt;
                    return mapp;
                }).ToList();

            var pList = _materialDetailService.GetList().Where(a => a.IsDeleted == false);
            var ResultList = new List<MaterialDto>(list);


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

            return new PagedResult<MaterialDto>(Mapper.Map<List<MaterialDto>>(list), total);
        }

        public bool Repeatjudgment(MaterialSearchDto input)
        {
            return Repository.Select.WhereIf(input.Id.HasValue, r => r.Id != input.Id).Where(r => r.MtlCode == input.MtlCode).Count() > 0;
        }
    }
}
