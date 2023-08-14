using System;
using AutoMapper;
using RW.PMS.CrossCutting.Localization.AutoMapper;

namespace RW.PMS.Application.Contracts.Mapper
{
    public class TypeProfile : Profile
    {
        public TypeProfile ()
        {
            CreateMap<Enum, string>().ConvertUsing<StringTypeConverter>();
        }
    }
}