using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Basics;
using RW.PMS.Application.Contracts.Basics;
using RW.PMS.Application.Contracts.System;
using RW.PMS.Application.System;
using RW.PMS.Application.Contracts.DTO.Configuration;
using RW.PMS.Application.Basics;
using RW.PMS.Application.Contracts.DTO.Project;
using System.ComponentModel.Design;

namespace RW.PMS.API.Controllers
{
    public class DeviceController : RWBaseController
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService roleService)
        {
            _deviceService = roleService;
        }

        [HttpGet]
        public IResponseDto GetList([FromQuery] DeviceSearchDto input)
        {
            var result = _deviceService.GetPagedList(input);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 数据是否重复
        /// </summary>
        [HttpGet("GetRepeat")]
        public IResponseDto GetRepeat([FromQuery] DeviceSearchDto input)
        {
            bool model = _deviceService.Repeatjudgment(input);

            return ResponseDto.Success(model);
        }

        [HttpPost]
        public IResponseDto DoAdd([FromBody] DeviceDto input)
        {
            var result = _deviceService.Insert(input);
            return ResponseDto.Success("添加成功！");
        }

        [HttpPut]
        public IResponseDto DoEdit([FromBody] DeviceDto input)
        {
            if (input.Id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            _deviceService.Update(input.Id, input);
            return ResponseDto.Success("修改成功！");
        }

        [HttpDelete]
        public IResponseDto DoDelete([FromBody] DeleteRequesDto model)
        {
            var ids = model.GetIds();
            if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
            int count = _deviceService.Delete(ids);
            return ResponseDto.Success($"成功删除了{count}条数据。");
        }
    }
}
