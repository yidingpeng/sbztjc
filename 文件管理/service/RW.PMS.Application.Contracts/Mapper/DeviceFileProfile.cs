using AutoMapper;
using RW.PMS.Application.Contracts.DTO.DeviceFile;
using RW.PMS.Domain.Entities.DeviceFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Mapper
{
    public class DeviceFileProfile : Profile
    {
        public DeviceFileProfile()
        {
            

            CreateMap<TestRoomDto, TestRoomEntity>();
            CreateMap<TestRoomEntity, TestRoomDto>();

            CreateMap<DeviceDto, DeviceEntity>();
            CreateMap<DeviceEntity, DeviceDto>();
            CreateMap<TestSheetDto, TestSheetEntity>();
            CreateMap<TestSheetEntity, TestSheetDto>();

            CreateMap<DeviceTestSheetTimeCountDto, DeviceTestSheetTimeCountEntity>();
            CreateMap<DeviceTestSheetTimeCountEntity, DeviceTestSheetTimeCountDto>();



            CreateMap<DeviceTestSheetMonthCountDto, DeviceTestSheetMonthCountEntity>();
            CreateMap<DeviceTestSheetMonthCountEntity, DeviceTestSheetMonthCountDto>();

            CreateMap<DeviceTestSheetWeekCountDto, DeviceTestSheetWeekCountEntity>();
            CreateMap<DeviceTestSheetWeekCountEntity, DeviceTestSheetWeekCountDto>();

            CreateMap<DeviceTestSheetYearCountDto, DeviceTestSheetYearCountEntity>();
            CreateMap<DeviceTestSheetYearCountEntity, DeviceTestSheetYearCountDto>();
        }
    }
}
