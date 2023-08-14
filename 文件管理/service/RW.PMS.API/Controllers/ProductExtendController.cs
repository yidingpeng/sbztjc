using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Basics;
using RW.PMS.Application.Contracts.Basics;
using RW.PMS.Application.Contracts.System;
using RW.PMS.Application.System;
using RW.PMS.Application.Contracts.DTO.Configuration;
using RW.PMS.Application.Basics;
using RW.PMS.Application.Contracts.DTO.Project;
namespace RW.PMS.API.Controllers
{
    public class ProductExtendController : RWBaseController
    {
        private readonly IProductExtendService _productExtendService;
        private readonly IProductionModelService _productionModelService;
        public ProductExtendController(IProductExtendService roleService, IProductionModelService productionModelService)
        {
            _productExtendService = roleService;
            _productionModelService = productionModelService;
        }

        [HttpGet]
        public IResponseDto GetList([FromQuery] ProductExtendSearchDto input)
        {
            var result = _productExtendService.GetPagedList(input);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 数据是否重复
        /// </summary>
        [HttpGet("GetRepeat")]
        public IResponseDto GetRepeat([FromQuery] ProductExtendSearchDto input)
        {
            bool model = _productExtendService.Repeatjudgment(input);

            return ResponseDto.Success(model);
        }

        /// <summary>
        /// 获取产品型号
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetProductionModelList")]
        public IResponseDto GetProductionModelList([FromQuery] ProductExtendSearchDto input)
        {
            var result = _productionModelService.GetList();

            List<BaseCommonOutput> list = new List<BaseCommonOutput>();
            for (int i = 0; i < result.Count; i++)
            {
                if (input.Id.HasValue)
                {
                    if (input.Id.Value == result[i].Id) { continue; }
                }
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    label = result[i].Pname,
                    value = result[i].Id
                };
                list.Add(baseCommonOutput);
            }
            return ResponseDto.Success(list);
        }

        [HttpPost]
        public IResponseDto DoAdd([FromBody] ProductExtendDto input)
        {
            var result = _productExtendService.Insert(input);
            return ResponseDto.Success("添加成功！");
        }

        [HttpPut]
        public IResponseDto DoEdit([FromBody] ProductExtendDto input)
        {
            if (input.Id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            _productExtendService.Update(input.Id, input);
            return ResponseDto.Success("修改成功！");
        }

        [HttpDelete]
        public IResponseDto DoDelete([FromBody] DeleteRequesDto model)
        {
            var ids = model.GetIds();
            if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
            int count = _productExtendService.Delete(ids);
            return ResponseDto.Success($"成功删除了{count}条数据。");
        }
    }
}
