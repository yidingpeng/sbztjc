using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DeviceStatus;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.DeviceStatus;

namespace RW.PMS.API.Controllers.device
{
    public class DeviceBaseDeviceController : RWBaseController
    {

        private readonly IDeviceBaseDeviceService _deviceBaseDeviceService;

        public DeviceBaseDeviceController(IDeviceBaseDeviceService deviceBaseDeviceService)
        {

            _deviceBaseDeviceService = deviceBaseDeviceService;
        }
        [HttpGet("GetList")]
        public IResponseDto GetList([FromQuery] DeviceStatusDto input)
        {
            var result = _deviceBaseDeviceService.getList(input);
            return ResponseDto.Success(result);
        }
        [HttpPut]
        public IResponseDto DoEdit([FromBody] DeviceStatusDto input)
        {
            if (input.id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            //var list = _deviceBaseDeviceService.GetList().Where(s => s.deviceName.Equals(input.deviceName)).Count();
            //if (list > 0) return ResponseDto.Error(500, "此试验间已存在！");
            _deviceBaseDeviceService.Update(input);
            return ResponseDto.Success("修改成功！");
        }

        [HttpPost]
        public IResponseDto DoAdd([FromBody] DeviceStatusDto input)
        {
            //var list = _deviceBaseDeviceService.GetList().Where(s => s.deviceName.Equals(input.deviceName) && s.Id != input.id).Count();
            //if (list > 0) return ResponseDto.Error(500, "此试验间已存在！");
            var result = _deviceBaseDeviceService.Insert(input);
            return ResponseDto.Success("添加成功！");
        }
        [HttpDelete]
        public IResponseDto DoDelete([FromBody] DeleteRequesDto model)
        {
            var ids = model.GetIds();
            if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
            int count = _deviceBaseDeviceService.Delete(ids);
            return ResponseDto.Success($"成功删除了{count}条数据。");
        }
    }
}
