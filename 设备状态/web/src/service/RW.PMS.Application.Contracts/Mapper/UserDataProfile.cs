using AutoMapper;
using RW.PMS.Application.Contracts.DTO.UserData;
using RW.PMS.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Mapper
{
    public class UserDataProfile : Profile
    {
        public UserDataProfile()
        {
            CreateMap<UserQuickEntity, QuickDto>();
            CreateMap<QuickDto, UserQuickEntity>();
        }
    }
}
