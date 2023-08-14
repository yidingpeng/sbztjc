using AutoMapper;
using RW.PMS.Application.Contracts.DTO.Stat;
using RW.PMS.Domain.Entities.Stat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Mapper
{
    public class StatDataProfile : Profile
    {
        public StatDataProfile()
        {
            CreateMap<StatDataDto, StatDataEntity>();
            CreateMap<StatDataEntity, StatDataDto>();
        }
    }
}
