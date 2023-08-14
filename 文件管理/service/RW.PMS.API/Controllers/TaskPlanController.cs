using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.Exceptions;

namespace RW.PMS.API.Controllers
{
    public class TaskPlanController : RWBaseController
    {
        ITaskPlanService _taskplanService;
        IProTemplateService _protempldateService;
        IStageService _stageService;
        public TaskPlanController(ITaskPlanService taskplanService,
            IProTemplateService protempldateService, IStageService stageService)
        {
            _taskplanService = taskplanService;
            _protempldateService = protempldateService;
            _stageService = stageService;
        }

        /// <summary>
        /// 项目模板信息分页
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("ProTemplatePagedList")]
        public IResponseDto GetList([FromQuery] projectTemplateSearchDto search)
        {
            var result = _protempldateService.ProTemplatePagedList(search);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 项目模板新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("MBdoAdd")]
        public IResponseDto DoAdd([FromBody] projectTemplateView input)
        {
            bool counts = _protempldateService.ProOnly(input.Name, input.Id);//验重
            if (counts) return ResponseDto.Error(-1, "存在重复模板名称！");
            var entity = _protempldateService.Insert(input);
            if (entity.Id > 0)
            {
                _taskplanService.WBSTabsInsertOrUpdate(new WbsTabsDto
                {
                    TemplateId = entity.Id,
                    PlanName = "主计划",
                    State = 0,
                });
            }
            return ResponseDto.Success("添加成功！");
        }

        /// <summary>
        /// 项目模板编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("MBdoEdit")]
        public IResponseDto DoEdit([FromBody] projectTemplateView input)
        {
            if (input.Id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            bool counts = _protempldateService.ProOnly(input.Name, input.Id);//验重
            if (counts) return ResponseDto.Error(-1, "存在重复模板名称！");
            _protempldateService.Update(input.Id, input);
            return ResponseDto.Success("修改成功！");
        }
        /// <summary>
        /// 项目模板删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpDelete("MBdoDelete")]
        public IResponseDto doDelete([FromBody] DeleteRequesDto input)
        {
            var ids = input.GetIds();
            var result = _protempldateService.Delete(ids);
            return result > 0 ? ResponseDto.Success("删除成功") : ResponseDto.Error(0, ExceptionCode.DeleteFailed.ToString());
        }

        /// <summary>
        /// 项目模板编辑状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("DoEditState")]
        public IResponseDto DoEditState(int Id)
        {
            if (Id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            bool state = _protempldateService.updateState(Id);
            if (!state) return ResponseDto.Error(-1, "修改失败！");
            return ResponseDto.Success("修改成功！");
        }

        /// <summary>
        /// WBS计划新增或修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("WBSDoAddOrEdit")]
        public IResponseDto WBSDoAddOrEdit([FromBody] WbsPlanDto input)
        {
            bool wbs = _taskplanService.WBSInsertOrUpdate(input);
            string msg;
            if (!wbs)
            {
                msg = "操作失败";
            }
            else
                msg = "操作成功";
            return ResponseDto.Success(msg);
        }

        /// <summary>
        /// WBSTabs计划新增或修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("WBSTabsDoAddOrEdit")]
        public IResponseDto WBSTabsDoAddOrEdit([FromBody] WbsTabsDto input)
        {
            bool wbs = _taskplanService.WBSTabsInsertOrUpdate(input);
            string msg;
            if (!wbs)
            {
                msg = "操作失败";
            }
            else
                msg = "操作成功";
            return ResponseDto.Success(msg);
        }

        /// <summary>
        /// 配置WBS计划列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("GetTempLateList")]
        public IResponseDto GetTempLateList(int TemplateId)
        {
            var result = _taskplanService.GetWBSTabsList(TemplateId);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 配置阶段列表
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("GetStageList")]
        public IResponseDto GetStageList(int TemplateId)
        {
            var result = _stageService.GetList(TemplateId);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 配置阶段新增或修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("StageAddOrEdit")]
        public IResponseDto StageAddOrEdit([FromBody] ConfigureStageDto input)
        {
            try
            {
                  if (input.Id > 0)
                {
                    int up = _stageService.Update(input.Id, input);
                    return up > 0 ? ResponseDto.Success("更新成功") : ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
                }
                else
                {
                    var entity = _stageService.GetList().Where(o => o.TemplateId == input.TemplateId).OrderByDescending(o => o.Sort).FirstOrDefault();
                    input.Sort = entity == null ? 1 : entity.Sort + 1;
                    var stage = _stageService.Insert(input);
                    return stage.Id > 0 ? ResponseDto.Success("新增成功") : ResponseDto.Error(0, ExceptionCode.UpdateFailed.ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 配置阶段更新排序
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("StageSortEdit")]
        public IResponseDto StageSortEdit([FromBody] List<ConfigureStageDto> list)
        {
            try
            {
                if (list.Count == 2)
                {
                    int sort1,
                        sort2;
                    sort1 = list[0].Sort;
                    sort2 = list[1].Sort;
                    list[0].Sort = sort2;
                    list[1].Sort = sort1;
                    int up = _stageService.Update(list[0].Id, list[0]);
                    int up2 = _stageService.Update(list[1].Id, list[1]);
                    return up > 0 && up2 > 0 ? ResponseDto.Success("更新成功") : ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
                }
                else
                {
                    return ResponseDto.Error(0, ExceptionCode.UpdateFailed.ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 配置阶段删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpDelete("StageDelete")]
        public IResponseDto StageDelete([FromBody] DeleteRequesDto input)
        {
            var ids = input.GetIds();
            var result = _stageService.Delete(ids);
            return result > 0 ? ResponseDto.Success("删除成功") : ResponseDto.Error(0, ExceptionCode.DeleteFailed.ToString());
        }

        /// <summary>
        /// 根据类型获取最大或最小的排序号
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSort")]
        public IResponseDto GetSort(int ParentId, string Type)
        {
            try
            {
                int result = 0;
                //同级数据  
                var list = _stageService.GetList().Where(o => o.TemplateId == ParentId).ToList();
                if (list.Any())
                {
                    if (Type == "up")
                    {
                        result = list.Max(o => o.Sort);
                    }
                    else if (Type == "down")
                    {
                        result = list.Min(o => o.Sort);
                    }
                }
                return ResponseDto.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 配置阶段上移下移
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("MoveEdit")]
        public IResponseDto MoveEdit(int Id, int ParentId, string Type)
        {
            try
            {
                bool result = false;
                //同级数据
                var list = _stageService.GetList(ParentId);
                if (list.Any())
                {
                    //当前数据
                    var entity = list.Where(o => o.Id == Id).FirstOrDefault();
                    if (entity != null)
                    {
                        int sort1,
                            sort2;
                        sort1 = entity.Sort;
                        //最大
                        var maxSort = list.Max(o => o.Sort);
                        //最小
                        var minSort = list.Min(o => o.Sort);
                        //下移
                        if (Type == "up")
                        {
                            var first = list.Where(o => o.Sort > entity.Sort).FirstOrDefault();
                            if (first != null)
                            {
                                sort2 = first.Sort;
                                entity.Sort = sort2;
                                var up = _stageService.Update(entity.Id, entity);
                                first.Sort = sort1;
                                var up2 = _stageService.Update(first.Id, first);
                                result = up > 0 && up2 > 0;
                            }
                        }
                        else if (Type == "down") //上移
                        {
                            var last = list.Where(o => o.Sort < entity.Sort).LastOrDefault();
                            if (last != null)
                            {
                                sort2 = last.Sort;
                                entity.Sort = sort2;
                                var up = _stageService.Update(entity.Id, entity);
                                last.Sort = sort1;
                                var up2 = _stageService.Update(last.Id, last);
                                result = up > 0 && up2 > 0;
                            }
                        }
                    }

                }
                return result == true ? ResponseDto.Success("移动成功") : ResponseDto.Success("移动失败");
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// WBS计划列表上移下移
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("WBSMoveEdit")]
        public IResponseDto WBSMoveEdit(int Id, int TemplateId, string Type)
        {
            try
            {
                var result = _taskplanService.WBSMove(Id, TemplateId, Type);
                return result == true ? ResponseDto.Success("移动成功") : ResponseDto.Success("移动失败");
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// WBS计划列表获取最大或最小的排序号
        /// </summary>
        /// <returns></returns>
        [HttpGet("WBSPlanGetSort")]
        public IResponseDto WBSPlanGetSort(int TemplateId, string Type)
        {
            try
            {
                int result = 0;
                var list = _taskplanService.GetWBSPlanList(TemplateId);
                if (list.Any())
                {
                    if (Type == "up")
                    {
                        result = list.Min(o => o.Sort);
                    }
                    else if (Type == "down")
                    {
                        result = list.Max(o => o.Sort);
                    }
                }
                return ResponseDto.Success(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 计划名称删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("WBSTabsDelete")]
        public IResponseDto WBSTabsDelete(int Id)
        {
           
            bool result = _taskplanService.WBSTabsDelete(Id);
            return result ? ResponseDto.Success("删除成功") : ResponseDto.Error(0, ExceptionCode.DeleteFailed.ToString());
        }

        /// <summary>
        /// 计划列表信息删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("WBSPlanDelete")]
        public IResponseDto WBSPlanDelete(int Id)
        {
            bool result = _taskplanService.WBSPlanDelete(Id);
            return result ? ResponseDto.Success("删除成功") : ResponseDto.Error(0, ExceptionCode.DeleteFailed.ToString());
        }
    }
}
