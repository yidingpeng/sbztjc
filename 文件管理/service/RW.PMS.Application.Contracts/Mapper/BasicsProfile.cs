using AutoMapper;
using RW.PMS.Application.Contracts.DTO.Basics;
using RW.PMS.Application.Contracts.DTO.Module;
using RW.PMS.Domain.Entities.Basics;
using RW.PMS.Domain.Entities.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Mapper
{
    public class BasicsProfile : Profile
    {
        public  BasicsProfile ()
        {
            CreateMap<ProductionDto, ProductionEntity>();
            CreateMap<ProductionEntity, ProductionDto>();

            CreateMap<WorkCenterDto, WorkCenterEntity>();
            CreateMap<WorkCenterEntity, WorkCenterDto>();

            CreateMap<DeviceDto, DeviceEntity>();
            CreateMap<DeviceEntity, DeviceDto>();

            CreateMap<ToolDto, ToolEntity>();
            CreateMap<ToolEntity, ToolDto>();

            CreateMap<ToolDetailDto, ToolDetailEntity>();
            CreateMap<ToolDetailEntity, ToolDetailDto>();

            CreateMap<MaterialDto, MaterialEntity>();
            CreateMap<MaterialEntity, MaterialDto>();

            CreateMap<MaterialDetailDto, MaterialDetailEntity>();
            CreateMap<MaterialDetailEntity, MaterialDetailDto>();

            CreateMap<ProductionModelDto, ProductionModelEntity>();
            CreateMap<ProductionModelEntity, ProductionModelDto>();

            CreateMap<GjwlOpcPointDto, GjwlOpcPointEntity>();
            CreateMap<GjwlOpcPointEntity, GjwlOpcPointDto>();

            CreateMap<ProcessInfoDto, ProcessInfoEntity>();
            CreateMap<ProcessInfoEntity, ProcessInfoDto>();

            CreateMap<ProductExtendDto, ProductExtendEntity>();
            CreateMap<ProductExtendEntity, ProductExtendDto>();

            CreateMap<DevicestatusDto, DevicestatusEntity>();
            CreateMap<DevicestatusEntity, DevicestatusDto>();
            
            CreateMap<LogdetailsDto, LogdetailsEntity>();
            CreateMap<LogdetailsEntity, LogdetailsDto>();

            CreateMap<FileInformationDto, FileInformationEntity>();
            CreateMap<FileInformationEntity, FileInformationDto>();
        }
    }
}
