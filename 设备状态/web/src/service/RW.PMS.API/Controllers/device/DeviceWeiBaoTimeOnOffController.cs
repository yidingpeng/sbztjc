using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DeviceStatus;
using RW.PMS.Application.Contracts.DTO.DeviceStatus;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.DeviceStatus;
using NPOI.SS.Formula.Functions;

namespace RW.PMS.API.Controllers.device
{
    public class DeviceWeiBaoTimeOnOffController : RWBaseController
    {
        private readonly IDeviceWeiBaoOnOffService  _deviceWeiBaoOnOffService;
        private readonly IDeviceStatusService _DeviceStatusService;
        private readonly IDRoomStatusService _dRoomStatusService;
        public DeviceWeiBaoTimeOnOffController(IDeviceWeiBaoOnOffService deviceWeiBaoOnOffService, IDeviceStatusService deviceStatusService, IDRoomStatusService dRoomStatusService)
        {
            _deviceWeiBaoOnOffService = deviceWeiBaoOnOffService;
            _DeviceStatusService = deviceStatusService;
            _dRoomStatusService= dRoomStatusService;
        }
        [HttpPost("DoAdd")]
        public IResponseDto DoAdd([FromBody] DeviceWeiBaoTimeOnOffDto input)
        {
           
            DateTime now = DateTime.Now;
            input.alarmTime = now;
            input.roomName = input.deviceName.Substring(4, 3);
            _deviceWeiBaoOnOffService.InsertTime(input);
            if(input.onOrOff.Equals("off"))
            {
                TimeSpan totalweibao = TimeSpan.Zero;
                var  devcieweibaolist= _deviceWeiBaoOnOffService.getweibaoTimeList(input.roomName, input.deviceName);
                for (int i = 0; i < devcieweibaolist.Count; i += 2)
                {
                    totalweibao += TimeSpan.FromTicks(devcieweibaolist[i + 1].alarmTime.Ticks - devcieweibaolist[i].alarmTime.Ticks);
                }
               var roomlist= _dRoomStatusService.getRoomAlllist().Where(t=>t.roomName.Contains(input.roomName)).ToList();
                
                _DeviceStatusService.updateDevicetotalweibaoTime(input.deviceName, roomlist[0].id, totalweibao.TotalSeconds);
            }
            if(input.onOrOff.Equals("on"))
            return ResponseDto.Success("开始计时！");
            else
                return ResponseDto.Success("结束计时！");
        }
    }
}
