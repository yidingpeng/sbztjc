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
using RW.PMS.Domain.Entities.Basics;
using MySqlX.XDevAPI.Common;

namespace RW.PMS.API.Controllers
{
    public class GjwlOpcPointController : RWBaseController
    {
        private readonly IGjwlOpcPointService _gjwlOpcPointService;
        private readonly IWorkCenterService _workCenterService;
        private readonly IToolService _toolService;
        private readonly IMaterialService _materialService;
        private readonly IDictionaryService _dictionaryService;

        public GjwlOpcPointController(IGjwlOpcPointService roleService, IDictionaryService dictionaryService, IWorkCenterService workCenterService, IToolService toolService, IMaterialService materialService)
        {
            _gjwlOpcPointService = roleService;
            _dictionaryService = dictionaryService;
            _toolService = toolService;
            _workCenterService = workCenterService;
            _materialService = materialService;
        }

        [HttpGet]
        public IResponseDto GetList([FromQuery] GjwlOpcPointSearchDto input)
        {
            var result = _gjwlOpcPointService.GetPagedList(input);
            return ResponseDto.Success(result);
        }

        [HttpGet("GetRepeat")]
        public IResponseDto GetRepeat([FromQuery] GjwlOpcPointSearchDto input) {
            bool model = _gjwlOpcPointService.Repeatjudgment(input);
            return ResponseDto.Success(model);
        }

        /// <summary>
        /// 获取字典Opc点位类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GwOPCType")]
        public IResponseDto GetProductionTypeList()
        {
            var result = _dictionaryService.GetSubItemList("GwOPCType");
            List<BaseCommonOutput> list = new List<BaseCommonOutput>();
            for (int i = 0; i < result.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    label = result[i].DictionaryText,
                    value = result[i].Id,
                    name = result[i].DictionaryValue
                };
                list.Add(baseCommonOutput);
            }

            return ResponseDto.Success(list);
        }

        /// <summary>
        /// 获取工作中心信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("WorkCenter")]
        public IResponseDto GetWorkCenterList()
        {
            var result = _workCenterService.GetList();
            List<BaseCommonOutput> list = new List<BaseCommonOutput>();
            for (int i = 0; i < result.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    label = result[i].workName,
                    value = result[i].Id
                };
                list.Add(baseCommonOutput);
            }

            return ResponseDto.Success(list);
        }

        /// <summary>
        /// 获取工具信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("ToolList")]
        public IResponseDto GetToolList()
        {
            var result = _toolService.GetList();
            List<BaseCommonOutput> list = new List<BaseCommonOutput>();
            for (int i = 0; i < result.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    label = result[i].toolName,
                    value = result[i].Id
                };
                list.Add(baseCommonOutput);
            }

            return ResponseDto.Success(list);
        }


        /// <summary>
        /// 获取物料信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("MaterialList")]
        public IResponseDto GetMaterialList()
        {
            var result = _materialService.GetList();
            List<BaseCommonOutput> list = new List<BaseCommonOutput>();
            for (int i = 0; i < result.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    label = result[i].MtlName,
                    value = result[i].Id
                };
                list.Add(baseCommonOutput);
            }

            return ResponseDto.Success(list);
        }


        [HttpPost]
        public IResponseDto DoAdd([FromBody] GjwlOpcPointDto input)
        {
            foreach (var item in input.opclist)
            {
                input.opcTypeID = item.Id;
                input.opcTypeCode = item.Code;
                input.opcValue = item.Value;

                _gjwlOpcPointService.Insert(input);
            }

            return ResponseDto.Success("添加成功！");
        }

        [HttpPut]
        public IResponseDto DoEdit([FromBody] GjwlOpcPointDto input)
        {
            if (input.Id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            _gjwlOpcPointService.EditGjWlOpc(input);

            return ResponseDto.Success("修改成功！");
        }

        [HttpDelete]
        public IResponseDto DoDelete([FromBody] DeleteRequesDto model)
        {
            var ids = model.GetIds();
            if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
            _gjwlOpcPointService.DeleteGjWlOpc(ids);
            return ResponseDto.Success($"成功删除了{ids.Length}条数据。");
        }
    }
}
