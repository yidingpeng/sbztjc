using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DeviceStatus;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.DeviceStatus;

namespace RW.PMS.API.Controllers.device
{
    public class DeviceTimeInputController : RWBaseController
    {
        private readonly IDeviceTimeInputService _deviceTimeInputService;

        public DeviceTimeInputController(IDeviceTimeInputService deviceTimeInputService)
        {
            _deviceTimeInputService = deviceTimeInputService;
        }
        [HttpPost("DoAdd")]
        public IResponseDto DoAdd([FromBody] DeviceTimeInputDto input)
        {
            //  var IsExist = _deviceStatusService.AccountExist(input);
            //if (IsExist) return ResponseDto.Error(500, "该用户名已存在");
            // input.createTime = DateTime.Now;
            DateTime now = DateTime.Now;
            _deviceTimeInputService.InsertDeviceHoliday(new DeviceHolidayTimeDto { deviceName = input.deviceName.Substring(7), roomName = input.deviceName.Substring(0,7), createTime = now, alarmTime = input.holidayTime });
            _deviceTimeInputService.InsertDeviceTranning(new DeviceTrainningTimeDto { deviceName = input.deviceName.Substring(7), roomName = input.deviceName.Substring(0, 7), createTime = now, alarmTime = input.trainningTime });
            _deviceTimeInputService.InsertDeviceweibao(new DeviceweibaoTimeDto { deviceName = input.deviceName.Substring(7), roomName = input.deviceName.Substring(0, 7), createTime = now, alarmTime = input.weibaoTime });
            
            return ResponseDto.Success("添加成功！");
        }
    }
}
