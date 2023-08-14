using AutoMapper;
using RW.PMS.Application.Contracts.DTO.DeviceStatus;
using RW.PMS.Domain.Entities.DeviceStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Mapper
{
    public class DeviceStatusProfile : Profile
    {
        public DeviceStatusProfile()
        {
            CreateMap<DeviceStatusTagNameEntity, DeviceStatusTagNameDto>();
            CreateMap<DeviceStatusTagNameDto, DeviceStatusTagNameEntity>();
            CreateMap<DeviceStatusEntity, DeviceStatusDto>();
            CreateMap<DeviceStatusDto, DeviceStatusEntity>();
            //        CreateMap<DeviceTestRoomEntity, DeviceTestRoomDto>() .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
            //.ForMember(dest => dest.roomName, opt => opt.MapFrom(src => src.roomName));
            //        CreateMap<DeviceNameEntity, DeviceTestRoomDto>().ForMember(dest => dest.deviceName, opt => opt.MapFrom(src => src.deviceName));
            CreateMap<DRoomTestRoomStatusEntity, DRoomTestRoomStatusDto>();
            CreateMap<DRoomTestRoomStatusDto, DRoomTestRoomStatusEntity>();
            CreateMap<DeviceTestSheetEntity, DeviceTesSheetDto>();
            CreateMap<DeviceTesSheetDto, DeviceTestSheetEntity>();
            CreateMap<DeviceTesSheetDto, DeviceTestSheetEntity>();

            CreateMap<FaultReportingDto, FaultReportingEntity>();
            CreateMap<FaultReportingEntity, FaultReportingDto>();

            CreateMap<OPCpointDto, OPCpointEntity>();
            CreateMap<OPCpointEntity, OPCpointDto>();


            CreateMap<TestRoomTimesDto, TestRoomzhiduTimeEntity>();
            CreateMap<TestRoomzhiduTimeEntity, TestRoomTimesDto>();

            CreateMap<TestRoomTimesDto, TestRoomweibaoTimeEntity>();
            CreateMap<TestRoomweibaoTimeEntity, TestRoomTimesDto>();

            CreateMap<TestRoomTimesDto, TestRoomHolidayTimeEntity>();
            CreateMap<TestRoomHolidayTimeEntity, TestRoomTimesDto>();


            CreateMap<DeviceFaultDownTimesDto, DeviceFaultDownTimesEntity>();
            CreateMap<DeviceFaultDownTimesEntity, DeviceFaultDownTimesDto>();
            CreateMap<DeviceFaultNumberTimeEntitity, DeviceFaultNumberTimeDto>();
            CreateMap<DeviceFaultNumberTimeDto, DeviceFaultNumberTimeEntitity>();
            //台架
            CreateMap<DRoomEffectiveRunningTimeEntity, DRoomEffectiveRunningTimeDto>();
            CreateMap<DRoomEffectiveRunningTimeDto, DRoomEffectiveRunningTimeEntity>();

            CreateMap<DRoomFaultTimeEntity, DRoomFaultTimeDto>();
            CreateMap<DRoomFaultTimeDto, DRoomFaultTimeEntity>();

            CreateMap<DRoomStandbyTimeEntity, DRoomStandbyTimeDto>();
            CreateMap<DRoomStandbyTimeDto, DRoomStandbyTimeEntity>();

            CreateMap<DRoomTestStopTimeEntity, DRoomTestStopTimeDto>();
            CreateMap<DRoomTestStopTimeDto, DRoomTestStopTimeEntity>();

            CreateMap<DRoomweibaoTimeEntity, DRoomweibaoTimeDto>();
            CreateMap<DRoomweibaoTimeDto, DRoomweibaoTimeEntity>();

            CreateMap<DRoomzhiduTimeEntity, DRoomzhiduTimeDto>();
            CreateMap<DRoomzhiduTimeDto, DRoomzhiduTimeEntity>();

            CreateMap<DRoomJiaoZhunTimeEntity, DRoomJiaoZhunTimeDto>();
            CreateMap<DRoomJiaoZhunTimeDto, DRoomJiaoZhunTimeEntity>();

            CreateMap<DRoomHolidayTimeEntity, DRoomHolidayTimeDto>();
            CreateMap<DRoomHolidayTimeDto, DRoomHolidayTimeEntity>();

            //设备
            CreateMap<DeviceRunTimeEntity, DeviceRunTimeDto>();
            CreateMap<DeviceRunTimeDto, DeviceRunTimeEntity>();

            CreateMap<DeviceStopTimeEntity, DeviceStopTimeDto>();
            CreateMap<DeviceStopTimeDto, DeviceStopTimeEntity>();

            CreateMap<DeviceweibaoTimeEntity, DeviceweibaoTimeDto>();
            CreateMap<DeviceweibaoTimeDto, DeviceweibaoTimeEntity>();

            CreateMap<DRoomTestStopTimeEntity, DRoomTestStopTimeDto>();
            CreateMap<DRoomTestStopTimeDto, DRoomTestStopTimeEntity>();

            CreateMap<DRoomweibaoTimeEntity, DRoomweibaoTimeDto>();
            CreateMap<DRoomweibaoTimeDto, DRoomweibaoTimeEntity>();

            CreateMap<DeviceHolidayTimeEntity, DeviceHolidayTimeDto>();
            CreateMap<DeviceHolidayTimeDto, DeviceHolidayTimeEntity>();

            CreateMap<DeviceTrainningTimeEntity, DeviceTrainningTimeDto>();
            CreateMap<DeviceTrainningTimeDto, DeviceTrainningTimeEntity>();


            CreateMap<DeviceWeiBaoTimeOnOffEntity, DeviceWeiBaoTimeOnOffDto>();
            CreateMap<DeviceWeiBaoTimeOnOffDto, DeviceWeiBaoTimeOnOffEntity>();

            CreateMap<DRoomWeiBaoTimeOnOffEntity, DRoomWeiBaoTimeOnOffDto>();
            CreateMap<DRoomWeiBaoTimeOnOffDto, DRoomWeiBaoTimeOnOffEntity>();
            //调试时间
            CreateMap<DRoomDeviceDebugRunTimeEntity, DRoomDeviceDebugRunTimeDto>();
            CreateMap<DRoomDeviceDebugRunTimeDto, DRoomDeviceDebugRunTimeEntity>();
            //设备统计
            CreateMap<DRoomWeekCountEntity, DRoomWeekCountDto>();
            CreateMap<DRoomWeekCountDto, DRoomWeekCountEntity>();

            CreateMap<DRoomMonthCountEntity, DRoomMonthCountDto>();
            CreateMap<DRoomMonthCountDto, DRoomMonthCountEntity>();

            CreateMap<DRoomYearCountEntity, DRoomYearCountDto>();
            CreateMap<DRoomYearCountDto, DRoomYearCountEntity>();
            //台架统计
            CreateMap<DeviceWeekCountEntity, DeviceWeekCountDto>();
            CreateMap<DeviceWeekCountDto, DeviceWeekCountEntity>();

            CreateMap<DeviceMonthCountEntity, DeviceMonthCountDto>();
            CreateMap<DeviceMonthCountDto, DeviceMonthCountEntity>();

            CreateMap<DeviceYearCountEntity, DeviceYearCountDto>();
            CreateMap<DeviceYearCountDto, DeviceYearCountEntity>();
        }
    }
}
