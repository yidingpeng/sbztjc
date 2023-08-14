using AutoMapper;
using RW.PMS.Application.Contracts.DTO.WorkingHours;
using RW.PMS.Domain.Entities.WorkingHours;

namespace RW.PMS.Application.Contracts.Mapper
{
    public class WhProfile: Profile
    {
        public WhProfile()
        {
            CreateMap<WHDetailEntity, WHDetailDto>();
            CreateMap<WHDetailDto, WHDetailEntity>();
        }
    }
}
