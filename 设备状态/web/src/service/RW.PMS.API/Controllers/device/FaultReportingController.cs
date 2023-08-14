using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Relational;
using NPOI.SS.Formula.Functions;
using RW.PMS.Application.Contracts.DeviceStatus;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.DeviceStatus;
using RW.PMS.Application.DeviceStatus;

namespace RW.PMS.API.Controllers.device
{
    public class FaultReportingController : RWBaseController
    {
        private readonly IFaultReportingService _faultReportingService;
        private readonly IDeviceFaultTimesService _deviceFaultTimesService;
        private readonly IDeviceStatusService _deviceStatusService;
        private readonly IDeviceFaultNumberTimeService _deviceFaultNumberTimeService;
        private readonly IDeviceRefreshTimeService _DeviceRefreshTimeService;
      

        public FaultReportingController(IFaultReportingService faultReportingService,IDeviceFaultTimesService deviceFaultTimesService, IDeviceStatusService deviceStatusService,
            IDeviceFaultNumberTimeService deviceFaultNumberTimeService, IDeviceRefreshTimeService deviceRefreshTimeService)
        {

            _faultReportingService = faultReportingService;
            _deviceFaultTimesService = deviceFaultTimesService;
            _deviceStatusService = deviceStatusService;
            _deviceFaultNumberTimeService = deviceFaultNumberTimeService;
            _DeviceRefreshTimeService = deviceRefreshTimeService;
        }
        [HttpGet("GetList")]
        public IResponseDto GetList([FromQuery] FaultReportingDto input)
        {
            var result = _faultReportingService.getList(input);
            return ResponseDto.Success(result);
        }
        [HttpPut]
        public IResponseDto DoEdit([FromBody] FaultReportingDto input)
        {
            if (input.id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            
            input.reportingStatus = 1;
            _faultReportingService.Update(input);

            _deviceFaultTimesService.Update(new DeviceFaultDownTimesDto { id = input.faultTimeid, deviceName = input.deviceName });
            var list = _deviceFaultTimesService.getFirstList(new DeviceFaultDownTimesDto { roomName = input.testRoom.Substring(4), onOrOff = "on" });
            _deviceFaultTimesService.Update(new DeviceFaultDownTimesDto { id = list.id, deviceName = input.deviceName });
            var device = (_deviceStatusService.GetDeviceNameAllList()).List.Where(t => t.deviceName.Equals(input.deviceName)).First();
            _deviceStatusService.update(new DeviceStatusDto{ id = device.id, toalFaultNumber = device.toalFaultNumber + 1 });
            _deviceFaultNumberTimeService.Insert(new DeviceFaultNumberTimeDto {deviceName=device.deviceName,roomName=list.roomName,alarmTime=list.alarmTime });
            var faultdownlist = _DeviceRefreshTimeService.getFaultDownTimeList( list.roomName, device.deviceName);
            //故障时间
            TimeSpan totalFaultDown = TimeSpan.Zero;
            if (faultdownlist.Count % 2 != 0)
            {
                return ResponseDto.Success("修改失败不是双数！");
            }
            else
                for (int i = 0; i < faultdownlist.Count; i += 2)
                {
                    totalFaultDown += TimeSpan.FromTicks(faultdownlist[i + 1].alarmTime.Ticks - faultdownlist[i].alarmTime.Ticks);
                }
            _deviceStatusService.updateDevicetotalFaultDownTime(device.deviceName, device.roomId, totalFaultDown.TotalSeconds);
            return ResponseDto.Success("修改成功！");
        }
    }
}
