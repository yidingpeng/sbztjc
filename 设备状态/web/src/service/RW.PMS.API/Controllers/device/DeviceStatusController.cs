using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DeviceStatus;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.DeviceStatus;
using RW.PMS.Application.DeviceStatus;

namespace RW.PMS.API.Controllers
{
    public class DeviceStatusController : RWBaseController
    {
        private readonly IDeviceStatusService _deviceStatusService;
        public DeviceStatusController(IDeviceStatusService deviceStatusService)
        {

            _deviceStatusService = deviceStatusService;
        }
        [HttpGet("GetDeviceStatusNameAllList")]
        public IResponseDto GetDeviceStatusNameAllList()
        {
            var result = _deviceStatusService.GetDeviceStatusNameAllList();
            return ResponseDto.Success(result);
        }
        [HttpGet("GetDeviceNameAllList")]
        public IResponseDto GetDeviceNameAllList()
        {
            var result = _deviceStatusService.GetDeviceNameAllList();
            result.List.ToList().ForEach(t => {
                if (t.isClear == 1)
                {
                    t.totalRunTime = 0; t.totalStopTime = 0; t.totalFaultDownTime = 0; t.toalFaultNumber = 0; t.totalFreeTime = 0; t.weibaoTime = 0; 
                }
            });
            return ResponseDto.Success(result);
        }
        [HttpGet("GetDeviceTestRoomAllList")]
        public IResponseDto GetDeviceTestRoomAllList()
        {
            var result = _deviceStatusService.GetDeviceTestRoomAllList();
            return ResponseDto.Success(result);
        }
        [HttpGet("GetDeviceNameConditionList")]
        public IResponseDto GetDeviceNameConditionList( string roomName)
        {

            var result = _deviceStatusService.GetDeviceNameConditionList(roomName);
            return ResponseDto.Success(result);
        
        }
        [HttpPost("DoAdd")]
        public IResponseDto DoAdd([FromBody] DeviceTesSheetDto input)
        {
            //  var IsExist = _deviceStatusService.AccountExist(input);
            //if (IsExist) return ResponseDto.Error(500, "该用户名已存在");
           // input.createTime = DateTime.Now;
           
            var result = _deviceStatusService.Insert(input);
            return ResponseDto.Success("添加成功！");
        }
        [HttpGet("getTestSheetAllList")]
        public IResponseDto getTestSheetAllList([FromQuery] DeviceTesSheetDto  input)
        {
            var result = _deviceStatusService.getTestSheetAllList(input);
            return ResponseDto.Success(result);
        }
        [HttpGet("GetRepeat")]
        public IResponseDto GetRepeat([FromQuery] DeviceTesSheetDto input)
        {
            bool model = _deviceStatusService.Repeatjudgment(input);

            return ResponseDto.Success(model);
        }
        [HttpPost("DoEdit")]
        public IResponseDto DoEdit([FromBody] DeviceTesSheetDto input)
        {
            //  var IsExist = _deviceStatusService.AccountExist(input);
            //if (IsExist) return ResponseDto.Error(500, "该用户名已存在");
            // input.createTime = DateTime.Now;
            _deviceStatusService.Update(input);
            return ResponseDto.Success("修改成功！");
        }
        [HttpPost("updateIsClear")]
        public IResponseDto updateIsClear([FromBody] int id)
        {
            //  var IsExist = _deviceStatusService.AccountExist(input);
            //if (IsExist) return ResponseDto.Error(500, "该用户名已存在");
            // input.createTime = DateTime.Now;
            _deviceStatusService.updateIsClear(id,1);
            return ResponseDto.Success("数据清零成功！");
        }

        [HttpGet("devicestauscount")]
        public DeviceOrDRoomStatusCountDto devicestauscount()
        {
            var faultlist = _deviceStatusService.getstatusList("故障");
            var runlist = _deviceStatusService.getstatusList("运行");
            var stoplist = _deviceStatusService.getstatusList("停机");
           
            return new DeviceOrDRoomStatusCountDto()
            {
                faultNumber = faultlist.Count,
                runNumber = runlist.Count,
                stopNumber = stoplist.Count,
               
            };
        }
    }
}
