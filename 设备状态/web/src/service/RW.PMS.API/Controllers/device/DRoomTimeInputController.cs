using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DeviceStatus;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.DeviceStatus;

namespace RW.PMS.API.Controllers.device
{
    public class DRoomTimeInputController : RWBaseController
    {
        private readonly IDRoomTimeInputService _droomTimeInputService;
        private readonly IDroomGetTimesService _droomgetTimeService;
        public DRoomTimeInputController(IDRoomTimeInputService droomTimeInputService,IDroomGetTimesService droomGetTimesService)
        {
            _droomTimeInputService = droomTimeInputService;
            _droomgetTimeService = droomGetTimesService;
        }
        [HttpPost("DoAdd")]
        public IResponseDto DoAdd([FromBody] DRoomTimeInputDto input)
        {
            //  var IsExist = _deviceStatusService.AccountExist(input);
            //if (IsExist) return ResponseDto.Error(500, "该用户名已存在");
            // input.createTime = DateTime.Now;
            bool isadd = true;
            var zhidulsit= _droomgetTimeService.getzhiduTimeList().List.ToList();
            zhidulsit.ForEach(t=>{ if (t.roomName.Equals(input.roomName)) { isadd = false; } });
            DateTime now = DateTime.Now;
            _droomTimeInputService.InsertDroomHoliday(new DRoomHolidayTimeDto { deviceName = "all",roomName=input.roomName,createTime=now,alarmTime=input.holidayTime});
            _droomTimeInputService.InsertDroomjioazhun(new DRoomJiaoZhunTimeDto { deviceName = "all",roomName=input.roomName,createTime=now,alarmTime=input.jiaozhunTime});
            _droomTimeInputService.InsertDroomweibao(new DRoomweibaoTimeDto { deviceName = "all",roomName=input.roomName,createTime=now,alarmTime=input.weibaoTime});
            if (isadd)
            {
                _droomTimeInputService.InsertDroomzhidu(new DRoomzhiduTimeDto { deviceName = "all", roomName = input.roomName, createTime = now, alarmTime = input.zhiduTime });
            }
            else
            {
                _droomTimeInputService.updateDroomzhidu(new DRoomzhiduTimeDto { roomName = input.roomName, createTime = now,    alarmTime = input.zhiduTime });
            }
                return ResponseDto.Success("添加成功！");
        }
    }
}
