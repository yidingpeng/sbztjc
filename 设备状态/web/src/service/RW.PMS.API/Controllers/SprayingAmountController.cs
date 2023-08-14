using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Orders;
using RW.PMS.Application.Contracts.Orders;

namespace RW.PMS.API.Controllers
{
    public class SprayingAmountController : RWBaseController
    {
        private readonly ISprayingAmountService _sprayingAmountService;
        public SprayingAmountController(ISprayingAmountService ordersService)
        {

            _sprayingAmountService = ordersService;
        }
        [HttpGet("GetAllList")]
        public IResponseDto GetAllList([FromQuery] SprayingAmountSearchDto input)
        {
            var result = _sprayingAmountService.GetAllList(input);
            return ResponseDto.Success(result);
        }
        [HttpPut]
        public IResponseDto DoEdit([FromBody] SprayingAmountDto input)
        {
            if (input.id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            var list = _sprayingAmountService.GetList().Where(s => s.oAxleModel.Equals(input.oAxleModel) && s.Id == input.id && s.sprayingParameter.Equals(input.sprayingParameter)).Count();
            if (list > 0) return ResponseDto.Error(500, "此车轴型号参数值已存在！");
            _sprayingAmountService.Update(input);
            return ResponseDto.Success("修改成功！");
        }

        [HttpPost]
        public IResponseDto DoAdd([FromBody] SprayingAmountDto input)
        {
            var list = _sprayingAmountService.GetList().Where(s => s.oAxleModel.Equals(input.oAxleModel) && s.Id != input.id ).Count();
            if (list > 0) return ResponseDto.Error(500, "此车轴型号已存在！");
            var result = _sprayingAmountService.Insert(input);
            return ResponseDto.Success("添加成功！");
        }
        [HttpDelete]
        public IResponseDto DoDelete([FromBody] DeleteRequesDto model)
        {
            var ids = model.GetIds();
            if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
            int count = _sprayingAmountService.Delete(ids);
            return ResponseDto.Success($"成功删除了{count}条数据。");
        }
    }
}
