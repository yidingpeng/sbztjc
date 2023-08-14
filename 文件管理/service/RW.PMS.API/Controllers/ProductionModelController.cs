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
using IDictionaryService = RW.PMS.Application.Contracts.System.IDictionaryService;
using System.Linq;

namespace RW.PMS.API.Controllers
{
    public class ProductionModelController : RWBaseController
    {
        private readonly IProductionModelService _productionModelService;
        private readonly IDictionaryService _dictionaryService;
        private readonly IProductionService _productionService;
        private readonly IProductExtendService _productExtendService;

        public ProductionModelController(IProductionModelService roleService, IDictionaryService dictionaryService, IProductionService productionService, IProductExtendService productExtendService)
        {
            _productionModelService = roleService;
            _dictionaryService = dictionaryService;
            _productionService = productionService;
            _productExtendService = productExtendService;
        }

        [HttpGet]
        public IResponseDto GetList([FromQuery] ProductionModelSearchDto input)
        {
            var result = _productionModelService.GetPagedList(input);
            return ResponseDto.Success(result);
        }

        [HttpGet("GetRepeat")]
        public IResponseDto GetRepeat([FromQuery] ProductionModelSearchDto input)
        {
            bool model = _productionModelService.Repeatjudgment(input);

            return ResponseDto.Success(model);
        }

        /// <summary>
        /// 获取额外字段信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("ProductExtendList")]
        public IResponseDto GetProductExtendList([FromQuery] ProductionModelSearchDto input)
        {
            var result = _productExtendService.GetList().Where(r => r.PModelID == input.Id.Value).ToList();
         
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 获取产品信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("ProductionList")]
        public IResponseDto GetProductionList()
        {
            var result = _productionService.GetList();
            List<BaseCommonOutput> list = new List<BaseCommonOutput>();
            for (int i = 0; i < result.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    label = result[i].Pname,
                    value = result[i].Id
                };
                list.Add(baseCommonOutput);
            }

            return ResponseDto.Success(list);
        }


        /// <summary>
        /// 获取字典产品型号类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("ProductionModelType")]
        public IResponseDto GetProductionModelTypeList()
        {
            var result = _dictionaryService.GetSubItemList("ProductionModelType");
            List<BaseCommonOutput> list = new List<BaseCommonOutput>();
            for (int i = 0; i < result.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    label = result[i].DictionaryText,
                    value = result[i].Id
                };
                list.Add(baseCommonOutput);
            }
            return ResponseDto.Success(list);
        }

        /// <summary>
        /// 获取字典产品类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("ProductionType")]
        public IResponseDto GetProductionTypeList()
        {
            var result = _dictionaryService.GetSubItemList("ProductionType");
            List<BaseCommonOutput> list = new List<BaseCommonOutput>();
            for (int i = 0; i < result.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    label = result[i].DictionaryText,
                    value = result[i].Id
                };
                list.Add(baseCommonOutput);
            }
            return ResponseDto.Success(list);
        }

        [HttpPost]
        public IResponseDto DoAdd([FromBody] ProductionModelDto input)
        {
            var result = _productionModelService.Insert(input);
            return ResponseDto.Success("添加成功！");
        }

        [HttpPut]
        public IResponseDto DoEdit([FromBody] ProductionModelDto input)
        {
            if (input.Id <= 0) return ResponseDto.Error(500, "ID不能为空！");

            foreach (var item in input.extendList)
            {
                _productExtendService.Update(item.Id, item);
            }

            _productionModelService.Update(input.Id, input);
            return ResponseDto.Success("修改成功！");
        }

        [HttpDelete]
        public IResponseDto DoDelete([FromBody] DeleteRequesDto model)
        {
            var ids = model.GetIds();
            if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
            int count = _productionModelService.Delete(ids);
            return ResponseDto.Success($"成功删除了{count}条数据。");
        }
    }
}
