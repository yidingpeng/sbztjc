using AutoMapper;
using RW.PMS.Application.Contracts.DTO.Material;
using RW.PMS.Domain.Entities.Material;

namespace RW.PMS.Application.Contracts.Mapper
{
    public class MaterialProfile : Profile
    {
        public MaterialProfile()
        {
            CreateMap<MaterialEntity, MaterialDto>();
            CreateMap<MaterialDto, MaterialEntity>();

            CreateMap<BOMListEntity, BOMListSearchDto>();
            CreateMap<BOMListSearchDto, BOMListEntity>();

            CreateMap<pdm_material, MaterialDto>();

            CreateMap<MaterialConversionEntity, MaterialConversionDto>();
            CreateMap<MaterialConversionDto, MaterialConversionEntity>();
        }

    }
}
