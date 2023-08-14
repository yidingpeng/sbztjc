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
using Microsoft.AspNetCore.Authorization;


namespace RW.PMS.API.Controllers
{
    public class ProcessInfoController : RWBaseController
    {
        private readonly IProcessInfoService _processInfoService;

        public ProcessInfoController(IProcessInfoService roleService)
        {
            _processInfoService = roleService;
        }

        [HttpGet]
        public IResponseDto GetList([FromQuery] ProcessInfoSearchDto input)
        {
            var result = _processInfoService.GetTreeList(input);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 数据是否重复
        /// </summary>
        [HttpGet("GetRepeat")]
        public IResponseDto GetRepeat([FromQuery] ProcessInfoSearchDto input)
        {
            bool model = _processInfoService.Repeatjudgment(input);

            return ResponseDto.Success(model);
        }

        /// <summary>
        /// 获取父级工序 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetParentProcessList")]
        public IResponseDto GetParentProcessList([FromQuery] ProcessInfoSearchDto input)
        {
            var result = _processInfoService.GetParentProcessList(input);

            List<BaseCommonOutput> list = new List<BaseCommonOutput>();
            for (int i = 0; i < result.Count; i++)
            {
                if (input.Id.HasValue) {
                    if (input.Id.Value == result[i].Id) { continue; }
                }
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    label = result[i].pcsName,
                    value = result[i].Id
                };
                list.Add(baseCommonOutput);
            }
            return ResponseDto.Success(list);
        }


        [HttpPost]
        public IResponseDto DoAdd([FromBody] ProcessInfoDto input)
        {
            var result = _processInfoService.Insert(input);
            return ResponseDto.Success("添加成功！");
        }

        [HttpPut]
        public IResponseDto DoEdit([FromBody] ProcessInfoDto input)
        {
            if (input.Id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            _processInfoService.Update(input.Id, input);
            return ResponseDto.Success("修改成功！");
        }

        [HttpDelete]
        public IResponseDto DoDelete([FromBody] DeleteRequesDto model)
        {
            var ids = model.GetIds();
            if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
            int count = _processInfoService.Delete(ids);
            return ResponseDto.Success($"成功删除了{count}条数据。");
        }
    }
}
