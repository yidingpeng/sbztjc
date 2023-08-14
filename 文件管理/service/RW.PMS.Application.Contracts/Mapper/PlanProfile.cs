using AutoMapper;
using RW.PMS.Application.Contracts.DTO.Plan;
using RW.PMS.Application.Contracts.DTO.Plan.Log;
using RW.PMS.Domain.Entities.Plan;
using RW.PMS.Domain.Entities.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Mapper
{
    public class PlanProfile : Profile
    {
        public PlanProfile()
        {
            CreateMap<WbsPlanEntity, TaskEntity>()
                .ForMember(t => t.Name, x => x.MapFrom(a => a.WbsName))
                .ForMember(t => t.IsMilestone, x => x.MapFrom(a => a.Milestone == 1))
                //.ForMember(t=>t.)
                ;
            CreateMap<TaskEntity, TaskDto>()
                .ForMember(t => t.Order, x => x.MapFrom(a => a.OrderIndex))
                ;
            CreateMap<TaskDto, TaskEntity>()
                .ForMember(t => t.OrderIndex, x => x.MapFrom(a => a.Order))
                ;

            CreateMap<AddPlanLogDto, PlanLogEntity>()
                ;
            CreateMap<PlanLogEntity, PlanLogListDto>()
                .ForMember(x => x.LogTime, x => x.MapFrom(a => a.CreatedAt))
                ;
            CreateMap<PlanLogEntity, PlanLogDetailDto>()
                .ForMember(x => x.LogTime, x => x.MapFrom(a => a.CreatedAt))
                ;

        }
    }
}
