using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DeviceStatus;
using RW.PMS.Application.Contracts.DTO.DeviceStatus;
using RW.PMS.Application.Contracts.DTO;

namespace RW.PMS.API.Controllers.device
{
    public class DRoomWeiBaoTimeOnOffController : RWBaseController
    {
        private readonly IDRoomWeiBaoOnOffService  _dRoomWeiBaoOnOffService;
        private readonly IDeviceStatusService _DeviceStatusService;
        private readonly IDRoomStatusService _dRoomStatusService;
        public DRoomWeiBaoTimeOnOffController(IDRoomWeiBaoOnOffService  dRoomWeiBaoOnOffService, IDeviceStatusService deviceStatusService, IDRoomStatusService dRoomStatusService)
        {
            _dRoomWeiBaoOnOffService = dRoomWeiBaoOnOffService;
            _DeviceStatusService = deviceStatusService;
            _dRoomStatusService = dRoomStatusService;
        }
        [HttpPost("DoAdd")]
        public IResponseDto DoAdd([FromBody] DRoomWeiBaoTimeOnOffDto input)
        {

            DateTime now = DateTime.Now;
            input.alarmTime = now;
            input.roomName = input.roomName.Substring(4);
            input.deviceName = "all";
            _dRoomWeiBaoOnOffService.InsertTime(input);
            if (input.onOrOff.Equals("off"))
            {
                TimeSpan totalweibao = TimeSpan.Zero;
                var devcieweibaolist = _dRoomWeiBaoOnOffService.getweibaoTimeList(input.roomName, input.deviceName);
                for (int i = 0; i < devcieweibaolist.Count; i += 2)
                {
                    totalweibao += TimeSpan.FromTicks(devcieweibaolist[i + 1].alarmTime.Ticks - devcieweibaolist[i].alarmTime.Ticks);
                }
             

                _dRoomStatusService.updateDRoomtotalweibaoTime(input.roomName, totalweibao.TotalSeconds);
            }
            if (input.onOrOff.Equals("on"))
                return ResponseDto.Success("开始计时！");
            else
                return ResponseDto.Success("结束计时！");
        }
    }
}
