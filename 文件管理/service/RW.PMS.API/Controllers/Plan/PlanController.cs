using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Plan;
using RW.PMS.Application.Contracts.Plan;
using RW.PMS.CrossCutting.Exceptions;

namespace RW.PMS.API.Controllers.Plan
{
    public class PlanController : RWBaseController
    {
        private readonly IPlanService _planService;
        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        /// <summary>
        /// 计划集
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("GetPlanList")]
        public IResponseDto GetPlanList(string ProjectCode)
        {
            var result = _planService.GetPlans(ProjectCode);
            if (result.Count == 0)
            {
                result.Add(new PlanDto { Id = 1, IsMainPlan = 1, OrderIndex = 1, PlanName = "主计划", Status = "变更中" });
                result.Add(new PlanDto { Id = 2, IsMainPlan = 0, OrderIndex = 2, PlanName = "子计划1", Status = "已发布" });
                result.Add(new PlanDto { Id = 3, IsMainPlan = 0, OrderIndex = 3, PlanName = "子计划2", Status = "已发布" });
            }
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 计划集新增或修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("PlanAddOrEdit")]
        public IResponseDto PlanAddOrEdit([FromBody] PlanDto input)
        {
            string msg;
            if (input.Id > 0)
            {
                int count = _planService.UpdatePlan(input.Id, input);
                msg = count > 0 ? "修改成功" : "修改失败";
            }
            else
            {
                var entity = _planService.AddPlan(input);
                msg = entity.Id > 0 ? "新增成功" : "新增失败";
            }
            return ResponseDto.Success(msg);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete("PlanDelete")]
        public IResponseDto PlanDelete([FromBody] DeleteRequesDto input)
        {
            var ids = input.GetIds();
            var result = _planService.DeletePlan(ids);
            return result > 0 ? ResponseDto.Success("删除成功") : ResponseDto.Error(0, ExceptionCode.DeleteFailed.ToString());
        }

        /// <summary>
        /// 获取计划任务
        /// </summary>
        [HttpGet("GetTaskList")]
        public IResponseDto GetTaskList(int planId)
        {
            var tasks = _planService.GetTasks(planId);
            //if (tasks.Count == 0)
            //{
            //    tasks.Add(new TaskDto { Id = 1, Name = "任务1", PlanId = planId, Owner = "袁勇", Duration = 1, WorkingHours = 8, Progress = 30, PlanStartDate = "2023-02-05", PlanEndDate = "2023-02-06" });
            //    tasks.Add(new TaskDto { Id = 2, Name = "任务2", PlanId = planId, Owner = "管理员", Duration = 2, WorkingHours = 8, IsMilistone = true, Progress = 50, PlanStartDate = "2023-02-02", PlanEndDate = "2023-02-03" });
            //    tasks.Add(new TaskDto { Id = 3, Name = "任务3", PlanId = planId, Owner = "仇文佳", Duration = 3, WorkingHours = 8, Progress = 100, PlanStartDate = "2023-02-03", PlanEndDate = "2023-02-04" });
            //}
            return ResponseDto.Success(tasks);
        }

        [HttpPut("MoveTask")]
        public IResponseDto MoveTaskTo(MoveTaskDto moveDto)
        {
            var result = _planService.MoveTask(moveDto);
            if (result > 0)
                return ResponseDto.Success("移动成功！");
            else
                return ResponseDto.Error(500, "移动失败！");
        }

        [HttpPost("importPlanTask")]
        public IResponseDto ImportPlanTask(ImportDto import)
        {
            var result = _planService.ImportPlanTask(import);

            if (result > 0)
                return ResponseDto.Success($"导入成功，导入{result}条任务。");
            else
                return ResponseDto.Error(500, "导入失败，未导入任务数据。");
        }

        [HttpPost("AddPlanTask")]
        public IResponseDto AddPlanTask(AddTaskDto task)
        {
            var result = _planService.AddTask(task);

            if (result > 0)
                return ResponseDto.Success("添加任务成功！");
            else
                return ResponseDto.Error(500, "添加任务失败！");
        }

        [HttpDelete("DeletePlanTask")]
        public IResponseDto DeletePlanTask(DeleteTaskDto task)
        {
            var result = _planService.DeleteTask(task);

            if (result > 0)
                return ResponseDto.Success("删除任务成功！");
            else
                return ResponseDto.Error(500, "删除任务失败！");
        }

        [HttpDelete("ClearPlanTask")]
        public IResponseDto ClearPlanTask(ClearTaskDto clear)
        {
            var result = _planService.ClearTask(clear);

            if (result > 0)
                return ResponseDto.Success("删除所有任务成功！");
            else
                return ResponseDto.Error(500, "删除所有任务失败！");
        }

        /// <summary>
        /// 修改任务字段属性
        /// </summary>
        /// <returns></returns>
        [HttpPut("EditTaskFiled")]
        public IResponseDto EditTaskFiled(EditTaskFiledDto edit)
        {
            var result = _planService.EditTaskFiled(edit);

            if (result > 0)
                return ResponseDto.Success("编辑成功！");
            else
                return ResponseDto.Error(500, "编辑属性失败！");
        }

    }
}
