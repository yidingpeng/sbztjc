using Microsoft.AspNetCore.Mvc;
using MiniExcelLibs;
using RW.PMS.Application.Contracts.DeviceStatus;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.DeviceStatus;
using RW.PMS.Application.DeviceStatus;
using RW.PMS.CrossCutting.Extender;

namespace RW.PMS.API.Controllers.device
{
    public class DeviceBaseRoomController : RWBaseController
    {
        private readonly IDeviceBaseRoomService _deviceBaseRoomService;
        private readonly IDRoomStatusService _dRoomStatusService;

        public DeviceBaseRoomController(IDeviceBaseRoomService deviceBaseRoomService, IDRoomStatusService dRoomStatusService)
        {

            _deviceBaseRoomService = deviceBaseRoomService;
            _dRoomStatusService = dRoomStatusService;
        }
        [HttpGet("GetList")]
        public IResponseDto GetList([FromQuery] DRoomTestRoomStatusDto input)
        {
            var result = _deviceBaseRoomService.getList(input);
            return ResponseDto.Success(result);
        }
        [HttpGet("GetAllList")]
        public IResponseDto GetAllList([FromQuery] DRoomTestRoomStatusDto input)
        {
            var result = _deviceBaseRoomService.getAllList(input);
            result.List.ToList().ForEach(t=>{ 
                if (t.isClear == 1) {
                    t.totalEffectiveRunningTime = 0; t.totalFaultTime = 0;t.totalTestStopTime = 0;t.totalStandbyTime = 0; t.totalweibaoTime = 0;t.totalFreeTime = 0;t.totalUtilizationRate = 0;
                } });
            return ResponseDto.Success(result);
        }
        [HttpPut]
        public IResponseDto DoEdit([FromBody] DRoomTestRoomStatusDto input)
        {
            if (input.id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            var list = _deviceBaseRoomService.GetList().Where(s => s.roomName.Equals(input.roomName) ).Count();
            if (list > 0) return ResponseDto.Error(500, "此试验间已存在！");
            _deviceBaseRoomService.Update(input);
            return ResponseDto.Success("修改成功！");
        }

        [HttpPost]
        public IResponseDto DoAdd([FromBody] DRoomTestRoomStatusDto input)
        {
            var list = _deviceBaseRoomService.GetList().Where(s => s.roomName.Equals(input.roomName) && s.Id != input.id).Count();
            if (list > 0) return ResponseDto.Error(500, "此试验间已存在！");
            var result = _deviceBaseRoomService.Insert(input);
            return ResponseDto.Success("添加成功！");
        }
        [HttpDelete]
        public IResponseDto DoDelete([FromBody] DeleteRequesDto model)
        {
            var ids = model.GetIds();
            if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
            int count = _deviceBaseRoomService.Delete(ids);
            return ResponseDto.Success($"成功删除了{count}条数据。");
        }
        [HttpPost("updateIsClear")]
        public IResponseDto updateIsClear([FromBody] int id)
        {
            //  var IsExist = _deviceStatusService.AccountExist(input);
            //if (IsExist) return ResponseDto.Error(500, "该用户名已存在");
            // input.createTime = DateTime.Now;
            _dRoomStatusService.updateDRoomIsClear(id, 1);
            return ResponseDto.Success("数据清零成功！");
        }

        [HttpGet("roomstauscount")]
        public DeviceOrDRoomStatusCountDto roomstauscount()
        {
          var faultlist=  _dRoomStatusService.getstatusList("故障");
        var testrunlist=    _dRoomStatusService.getstatusList("试验中");
        var teststoplist=   _dRoomStatusService.getstatusList("试验暂停中");
          var debugrunlist=  _dRoomStatusService.getstatusList("调试运行");
          var standbylsit=  _dRoomStatusService.getstatusList("待机中");
            return new DeviceOrDRoomStatusCountDto() {faultNumber=faultlist.Count,testrunNumber=testrunlist.Count,
                teststopNumber=teststoplist.Count,debugrunNumber=debugrunlist.Count,standbyNumber=standbylsit.Count };
        }
    }
}
