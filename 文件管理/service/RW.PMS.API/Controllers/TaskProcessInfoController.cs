using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.Application.Contracts.System;

namespace RW.PMS.API.Controllers
{
    public class TaskProcessInfoController : RWBaseController
    {
        ITaskProcessInfoService _taskprocessinfoService;
        IDictionaryService _dictionaryService;
        public TaskProcessInfoController(ITaskProcessInfoService taskprocessinfoService, IDictionaryService dictionaryService)
        {
            _taskprocessinfoService = taskprocessinfoService;
            _dictionaryService = dictionaryService;
        }

        /// <summary>
        /// 任务基础信息分页
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("PageList")]
        public IResponseDto GetList([FromQuery] TaskProcessInfoSearchDto search)
        {
            var result = _taskprocessinfoService.PagedList(search);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 根据父级ID查询任务信息
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("GetByParentList")]
        public IResponseDto GetByProIDList(int Id)
        {
            var result = _taskprocessinfoService.GetByProIDList(Id);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("doAdd")]
        public IResponseDto DoAdd([FromBody] TaskProcessInfoView input)
        {
            bool counts = _taskprocessinfoService.ProOnly(input.TaskCode, input.Id);//验重
            if (counts) return ResponseDto.Error(-1, "存在重复的任务编号！");
            int maxNo= _taskprocessinfoService.MaxSeqNo(input.ParentId);
            input.SeqNo = maxNo + 1;
            _taskprocessinfoService.Insert(input);
            return ResponseDto.Success("添加成功！");
        }

        /// <summary>
        /// 上移下移
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("UpdateSeqNo")]
        public IResponseDto UpdateSeqNo(int Id, int ParentId, string Type)
        {
            bool counts = _taskprocessinfoService.UpdateSeqNo(Id,ParentId,Type);//修改
            if (!counts) return ResponseDto.Error(-1, "移动异常");
            return ResponseDto.Success("移动成功");
        }
        /// <summary>
        /// 最大排序号
        /// </summary>
        /// <returns></returns>
        [HttpGet("MaxSeqNo")]
        public IResponseDto MaxSeqNo(int ParentId)
        {
            var result = _taskprocessinfoService.MaxSeqNo(ParentId);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 最小排序号
        /// </summary>
        /// <returns></returns>
        [HttpGet("MinSeqNo")]
        public IResponseDto MinSeqNo(int ParentId)
        {
            var result = _taskprocessinfoService.MinSeqNo(ParentId);
            return ResponseDto.Success(result);
        }
        //UpdateSeqNo
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public IResponseDto DoEdit([FromBody] TaskProcessInfoView input)
        {
            if (input.Id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            bool counts = _taskprocessinfoService.ProOnly(input.TaskCode, input.Id);//验重
            if (counts) return ResponseDto.Error(-1, "存在重复的任务编号！");
            _taskprocessinfoService.Update(input);
            return ResponseDto.Success("修改成功！");
        }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete]
        public IResponseDto DoDelete([FromBody] DeleteRequesDto model)
        {
            var ids = model.GetIds();
            if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
            int count = _taskprocessinfoService.Delete(ids);
            foreach (var item in ids)
            {
                DeleteChiledren(item);
            }
            return ResponseDto.Success($"成功删除{count}条数据。");
        }

        /// <summary>
        /// 删除父项目下的所有子项目
        /// </summary>
        /// <param name="Id"></param>
        [HttpGet("DeleteChiledren")]
        public void DeleteChiledren(int Id)
        {
            var childrenId = _taskprocessinfoService.GetByProIDList(Id);
            if (childrenId.Any())
            {
                foreach (var CItem in childrenId)
                {
                    if (CItem.HasChildren == true)
                    {
                        DeleteChiledren(CItem.Id);
                    }
                    _taskprocessinfoService.Delete(CItem.Id);
                }
            }
        }

        /// <summary>
        /// 父级子集list
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("ParentList")]
        public IResponseDto GetParentList()
        {
            var result = _taskprocessinfoService.GetTreeList();
            return ResponseDto.Success(result);
        }
    }
}
