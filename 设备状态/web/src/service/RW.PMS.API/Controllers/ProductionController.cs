using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Basics;
using RW.PMS.Application.Contracts.Basics;
using RW.PMS.Application.Contracts.System;
using RW.PMS.Application.System;
using RW.PMS.Application.Contracts.DTO.Configuration;
using RW.PMS.Application.Basics;

namespace RW.PMS.API.Controllers
{
    public class ProductionController : RWBaseController
    {
        private readonly IProductionService _ProductionServic;

        public ProductionController(IProductionService roleService)
        {
            _ProductionServic = roleService;
        }

        [HttpGet]
        public IResponseDto GetList([FromQuery] ProductionSearchDto input)
        {
            var result = _ProductionServic.GetPagedList(input);
            return ResponseDto.Success(result);
        }

        [HttpGet("GetRepeat")]
        public IResponseDto GetRepeat([FromQuery] ProductionSearchDto input)
        {
            bool model = _ProductionServic.Repeatjudgment(input);

            return ResponseDto.Success(model);
        }

        [HttpPost]
        public IResponseDto DoAdd([FromBody] ProductionDto input)
        {
            var result = _ProductionServic.Insert(input);
            return ResponseDto.Success("添加成功！");
        }

        [HttpPut]
        public IResponseDto DoEdit([FromBody] ProductionDto input)
        {
            if (input.Id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            _ProductionServic.Update(input.Id, input);
            return ResponseDto.Success("修改成功！");
        }

        [HttpDelete]
        public IResponseDto DoDelete([FromBody] DeleteRequesDto model)
        {
            var ids = model.GetIds();
            if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
            int count = _ProductionServic.Delete(ids);
            return ResponseDto.Success($"成功删除了{count}条数据。");
        }
    }
}
