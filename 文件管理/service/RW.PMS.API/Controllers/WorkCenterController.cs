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

namespace RW.PMS.API.Controllers
{
    public class WorkCenterController : RWBaseController
    {
        private readonly IWorkCenterService _workCenterService;
        IDictionaryService _dictionaryService;

        public WorkCenterController(IWorkCenterService roleService,IDictionaryService dictionaryService)
        {
            _workCenterService = roleService;
            _dictionaryService = dictionaryService;
        }

        [HttpGet]
        public IResponseDto GetList([FromQuery] WorkCenterSearchDto input)
        {
            var result = _workCenterService.GetPagedList(input);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 数据是否重复
        /// </summary>
        [HttpGet("GetRepeat")]
        public IResponseDto GetRepeat([FromQuery] WorkCenterSearchDto input)
        {
            bool model = _workCenterService.Repeatjudgment(input);

            return ResponseDto.Success(model);
        }

        /// <summary>
        /// 获取字典工作中心类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("WorkCenterType")]
        public IResponseDto GetWorkCenterTypeList()
        {
            var result = _dictionaryService.GetSubItemList("WorkCenterType");
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
        /// 获取字典父级工作中心 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GongWeiArea")]
        public IResponseDto GetGongWeiAreaList()
        {
            var result = _dictionaryService.GetSubItemList("GongWeiArea");
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
        public IResponseDto DoAdd([FromBody] WorkCenterDto input)
        {
            var result = _workCenterService.Insert(input);
            return ResponseDto.Success("添加成功！");
        }

        [HttpPut]
        public IResponseDto DoEdit([FromBody] WorkCenterDto input)
        {
            if (input.Id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            _workCenterService.Update(input.Id, input);
            return ResponseDto.Success("修改成功！");
        }

        [HttpDelete]
        public IResponseDto DoDelete([FromBody] DeleteRequesDto model)
        {
            var ids = model.GetIds();
            if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
            int count = _workCenterService.Delete(ids);
            return ResponseDto.Success($"成功删除了{count}条数据。");
        }
    }
}
