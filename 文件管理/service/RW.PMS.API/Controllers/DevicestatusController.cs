using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.Basics;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Basics;

namespace RW.PMS.API.Controllers
{
    public class DevicestatusController : RWBaseController
    {
        private readonly IDevicestatusService _devicestatus;

        public DevicestatusController(IDevicestatusService devicestatus)
        {
            _devicestatus = devicestatus;
        }

        [HttpGet]
        public IResponseDto GetList([FromQuery] DevicestatusSearchDto input)
        {
            var result = _devicestatus.GetPagedList(input);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 数据是否重复
        /// </summary>
        [HttpGet("GetRepeat")]
        public IResponseDto GetRepeat([FromQuery] DevicestatusSearchDto input)
        {
            bool model = _devicestatus.Repeatjudgment(input);

            return ResponseDto.Success(model);
        }

        [HttpPost]
        public IResponseDto DoAdd([FromBody] DevicestatusDto input)
        {
            var result = _devicestatus.Insert(input);
            return ResponseDto.Success("添加成功！");
        }

        [HttpPut]
        public IResponseDto DoEdit([FromBody] DevicestatusDto input)
        {
            if (input.Id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            _devicestatus.Update(input.Id, input);
            return ResponseDto.Success("修改成功！");
        }

        [HttpDelete]
        public IResponseDto DoDelete([FromBody] DeleteRequesDto model)
        {
            var ids = model.GetIds();
            if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
            int count = _devicestatus.Delete(ids);
            return ResponseDto.Success($"成功删除了{count}条数据。");
        }

    }
}
