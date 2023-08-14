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
using NPOI.SS.Formula.Functions;

namespace RW.PMS.Application.Basics
{
    public class GjwlOpcPointService : CrudApplicationService<GjwlOpcPointEntity, int>, IGjwlOpcPointService
    {
        public GjwlOpcPointService(IDataValidatorProvider dataValidator,
                        IRepository<GjwlOpcPointEntity, int> productionRepository,
                        IMapper mapper,
                        Lazy<ICurrentUser> currentUser
                      ) :
       base(dataValidator, productionRepository, mapper, currentUser)
        { }

        public PagedResult<GjwlOpcPointDto> GetPagedList(GjwlOpcPointSearchDto input)
        {

            List<GjwlOpcPointDto> list = Mapper.Map<List<GjwlOpcPointDto>>(Repository.Select
                .WithSql(@"select * from base_gjwlOpcPoint group by gwID,gjID,wlID,opcDeviceName")
                .From<WorkCenterEntity, ToolEntity, MaterialEntity>((r, w, t, m) => r.LeftJoin(rw => rw.gwID == w.Id).LeftJoin(rt => rt.gjID == t.Id).LeftJoin(rm => rm.wlID == m.Id))
                .WhereIf(input.gwname.NotNullOrWhiteSpace(), (r, w, t, m) => w.workName.Contains(input.gwname))
               .Count(out var total).Page(input.PageNo, input.PageSize).ToList((r, w, t, m) => new
               {
                   model = r,
                   workname = w.workName,
                   gjname = t.toolName,
                   wlname = m.MtlName
               }).Select(t =>
               {
                   GjwlOpcPointDto dto = Mapper.Map<GjwlOpcPointDto>(t.model);

                   dto.gwname = t.workname;
                   dto.gjname = t.gjname;
                   dto.wlname = t.wlname;

                   return dto;
               }));

            foreach (var item in list)
            {
                item.opclist = GetGjWlOpcTypes(item);
            }

            return new PagedResult<GjwlOpcPointDto>(list, total);
        }

        public bool Repeatjudgment(GjwlOpcPointSearchDto input)
        {
            return Repository.Select.Where(r => r.gwID == input.gwID).WhereIf(input.gjID.HasValue, r => r.gjID == input.gjID).WhereIf(input.wlID.HasValue, r => r.wlID == input.wlID).Count() > 0;
        }

        public List<GjWlOpcType> GetGjWlOpcTypes(GjwlOpcPointDto dto)
        {
            var pro = Repository.Select.Where(r => r.gwID == dto.gwID).Where(r => r.gjID == dto.gjID).Where(r => r.wlID == dto.wlID)
                    .ToList();

            List<GjWlOpcType> list = new List<GjWlOpcType>();
            foreach (var item in pro)
            {
                GjWlOpcType model = new GjWlOpcType();

                model.Id = item.opcTypeID;
                model.Code = item.opcTypeCode;
                model.Value = item.opcValue;

                list.Add(model);
            }

            return list;
        }

        public void EditGjWlOpc(GjwlOpcPointDto input)
        {
            GjwlOpcPointEntity gjwlOpcPoint = Repository.Select.Where(r => r.Id == input.Id).ToList().FirstOrDefault();

            foreach (var item in input.opclist)
            {
                GjwlOpcPointEntity model = Repository.Select.Where(r => r.gwID == gjwlOpcPoint.gwID && r.gjID == gjwlOpcPoint.gjID && r.wlID == gjwlOpcPoint.wlID && r.opcTypeID == item.Id && r.opcTypeCode == item.Code).ToList().FirstOrDefault();

                if (model != null)
                {
                    model.opcValue = item.Value;
                    model.gwID = input.gwID;
                    model.gjID = input.gjID.HasValue ? input.gjID.Value : 0;
                    model.wlID = input.wlID.HasValue ? input.wlID.Value : 0;
                    model.opcDeviceName = input.opcDeviceName;
                    model.remark = input.remark;

                    Repository.Update(model);
                }
                else
                {
                    model = new GjwlOpcPointEntity();

                    model.opcValue = item.Value;
                    model.gwID = input.gwID;
                    model.gjID = input.gjID.HasValue ? input.gjID.Value : 0;
                    model.wlID = input.wlID.HasValue ? input.wlID.Value : 0;
                    model.opcTypeID = item.Id;
                    model.opcTypeCode = item.Code;
                    model.opcDeviceName = input.opcDeviceName;
                    model.remark = input.remark;

                    Repository.Insert(model);
                }
            }
        }

        public void DeleteGjWlOpc(int[] ids)
        {

            foreach (var id in ids)
            {
                var model = Repository.Select.Where(r => r.Id == id).ToList().FirstOrDefault();

                if (model != null)
                {
                    Repository.Select.Where(r => r.gwID == model.gwID && r.gjID == model.gjID && r.wlID == model.wlID).ToDelete().ExecuteAffrows();
                }
            }

        }
    }
}
