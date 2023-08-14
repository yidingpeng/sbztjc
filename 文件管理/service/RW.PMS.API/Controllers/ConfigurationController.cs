using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Configuration;
using RW.PMS.Application.Contracts.System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RW.PMS.API.Controllers
{
    public class ConfigurationController : RWBaseController
    {
        private readonly IConfigService _configService;

        public ConfigurationController(IConfigService roleService)
        {
            _configService = roleService;
        }

        [HttpGet]
        public IResponseDto GetList([FromQuery] ConfigSearchDto input)
        {
            var result = _configService.GetPagedList(input);
            return ResponseDto.Success(result);
        }

        [HttpGet("Types")]
        public IResponseDto GetTypeList()
        {
            var result = _configService.GetConfigTypes();
            return ResponseDto.Success(result);
        }

        [HttpPost]
        public IResponseDto DoAdd([FromBody] ConfigListDto input)
        {
            var result = _configService.Insert(input);
            return ResponseDto.Success("添加成功！");
        }

        [HttpPut]
        public IResponseDto DoEdit([FromBody] ConfigListDto input)
        {
            if (input.Id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            _configService.Update(input.Id, input);
            return ResponseDto.Success("修改成功！");
        }

        [HttpDelete]
        public IResponseDto DoDelete([FromBody] DeleteRequesDto model)
        {
            var ids = model.GetIds();
            if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
            int count = _configService.Delete(ids);
            return ResponseDto.Success($"成功删除了{count}条数据。");
        }
    }
}
