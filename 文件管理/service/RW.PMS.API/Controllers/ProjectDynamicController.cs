using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.Exceptions;

namespace RW.PMS.API.Controllers
{
    /// <summary>
    /// 项目动态
    /// </summary>
    public class ProjectDynamicController : RWBaseController
    {
        IProjectDynamicService _projectDynamicService;
        IDictionaryService _dictionaryService;
        public ProjectDynamicController(IDictionaryService dictionaryService, IProjectDynamicService projectDynamicService)
        {
            _dictionaryService = dictionaryService;
            _projectDynamicService = projectDynamicService;
        }

        /// <summary>
        /// 项目动态信息分页
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("PagedList")]
        public IResponseDto GetPagedList([FromQuery] ProjectDynamicSearchDto search)
        {
            var result = _projectDynamicService.GetPagedList(search);
            return ResponseDto.Success(result);
        }
        /// <summary>
        /// 项目动态信息List
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("List")]
        public IResponseDto GetList(int Id)
        {
            var result = _projectDynamicService.GetList(Id);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 项目动态新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("doAdd")]
        public IResponseDto DoAdd([FromBody] ProjectDynamicView input)
        {
            _projectDynamicService.Insert(input);
            return ResponseDto.Success("添加成功！");
        }

        /// <summary>
        /// 项目动态编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("doEdit")]
        public IResponseDto DoEdit([FromBody] ProjectDynamicView input)
        {
            if (input.Id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            _projectDynamicService.Update(input);
            return ResponseDto.Success("修改成功！");
        }
        /// <summary>
        /// 项目动态删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpDelete("doDelete")]
        public IResponseDto doDelete([FromBody] DeleteRequesDto input)
        {
            var ids = input.GetIds();
            var result = _projectDynamicService.Delete(ids);
            return result > 0 ? ResponseDto.Success("删除成功") : ResponseDto.Error(0, ExceptionCode.DeleteFailed.ToString());
        }

        /// <summary>
        /// 获取字典动态类型
        /// </summary>
        /// <returns></returns>
        [HttpGet("DyType")]
        public IResponseDto ProjectDyType()
        {
            var list = _dictionaryService.GetSubItemList("DyType");
            List<BaseCommonOutput> result = new List<BaseCommonOutput>();
            for (int i = 0; i < list.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    label = list[i].DictionaryText,
                    value = list[i].Id
                };
                result.Add(baseCommonOutput);
            }
            return ResponseDto.Success(result);
        }
    }
}
