using AutoMapper;
using RW.PMS.Application.Contracts.DTO.Plan;
using RW.PMS.Application.Contracts.DTO.Plan.Log;
using RW.PMS.Application.Contracts.Plan;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Plan;
using RW.PMS.Domain.Entities.Project;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace RW.PMS.Application.Plan
{
    public class PlanService : CrudApplicationService<PlanEntity, int>, IPlanService
    {
        PlanLogService logService;
        public PlanService(PlanLogService logService, IDataValidatorProvider dataValidator,
          IRepository<PlanEntity, int> repository, IMapper mapper, Lazy<ICurrentUser> currentUser) : base(
          dataValidator, repository, mapper, currentUser)
        {
            this.logService = logService;
        }

        public PlanEntity AddPlan(PlanDto input)
        {
            var model = this.Insert(input);

            var logModel = new PlanLogEntity();
            logModel.SetPlan(model);
            logModel.AddPlan(CurrentUser.Value);
            logService.Insert(logModel);

            return model;
        }

        static object locker = new object();
        public int AddTask(AddTaskDto task)
        {
            //为了防止排序号错乱的问题，加锁防止多线程同时访问
            lock (locker)
            {
                var plan = Repository.Where(x => x.Id == task.PlanId).First();
                var entity = new TaskEntity();
                entity.Name = task.TaskName;
                entity.PlanId = task.PlanId;
                entity.PlanStartDate = plan.PlanDate;
                entity.PlanEndDate = plan.PlanDate;
                entity.Duration = 0;
                entity.ParentId = task.ParentId;
                entity.WorkingHours = 8;
                entity.OrderIndex = Repository.Orm.Select<TaskEntity>()
                    .Where(x => x.PlanId == task.PlanId)
                    .Max(x => x.OrderIndex) + 1;

                var num = Repository.Orm.Insert(entity).ExecuteAffrows();

                var logModel = new PlanLogEntity();
                logModel.SetTask(entity);
                logModel.AddTask(CurrentUser.Value);
                logService.Insert(logModel);
                return num;
            }
        }

        public int ClearTask(ClearTaskDto clear)
        {
            var plan = Repository.Where(x => x.Id == clear.PlanId).First();
            int num = Repository.Orm.Update<TaskEntity>()
                .Set(x => x.IsDeleted, true)
                .Where(x => x.PlanId == clear.PlanId)
                .ExecuteAffrows();
            var logModel = new PlanLogEntity();
            logModel.SetPlan(plan);
            logModel.ClearTask(CurrentUser.Value, num);
            logService.Insert(logModel);

            return num;
        }

        /// <summary>
        /// 删除计划
        /// </summary>
        public int DeletePlan(int[] ids)
        {
            var names = Repository.Where(x => ids.Contains(x.Id)).ToList(x => x.PlanName).ToArray();

            int i = this.Delete(ids);

            var logModel = new PlanLogEntity();
            logModel.DeletePlans(CurrentUser.Value, ids, names);
            logService.Insert(logModel);

            return i;
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        public int DeleteTask(DeleteTaskDto task)
        {
            var result = Repository.Orm.Select<TaskEntity>().Where(x => x.Id == task.TaskId).First();
            var plan = Repository.Where(x => x.Id == result.PlanId).First();
            result.IsDeleted = true;
            if (result == null) throw new NullReferenceException($"指定的任务ID不存在。ID={task.TaskId}");
            int num = Repository.Orm.Update<TaskEntity>().SetSource(result).UpdateColumns(x => x.IsDeleted).ExecuteAffrows();
            //更新排序之后的排序索引号
            Repository.Orm.Update<TaskEntity>().Set(x => x.OrderIndex - 1).Where(x => x.PlanId == result.PlanId && x.OrderIndex > result.OrderIndex).ExecuteAffrows();

            var logModel = new PlanLogEntity();
            logModel.SetPlan(plan);
            logModel.SetTask(result);
            logModel.DeleteTask(CurrentUser.Value);
            logService.Insert(logModel);

            return num;
        }

        public int EditTaskFiled(EditTaskFiledDto edit)
        {
            if (edit.Value != null)
            {
                JsonElement element = (JsonElement)edit.Value;
                switch (element.ValueKind)
                {
                    case JsonValueKind.Number:
                        edit.Value = element.GetDecimal();
                        break;
                    case JsonValueKind.True:
                    case JsonValueKind.False:
                        edit.Value = element.GetBoolean();
                        break;
                    case JsonValueKind.Null:
                    case JsonValueKind.Undefined:
                    case JsonValueKind.Object:
                    case JsonValueKind.Array:
                    case JsonValueKind.String:
                    default:
                        edit.Value = element.GetString();
                        break;
                }
            }

            var task = Repository.Orm.Select<TaskEntity>().Where(x => x.Id == edit.Id).First();
            var plan = Repository.Where(x => x.Id == task.PlanId).First();
            task.OffsetDuration += value =>
            {
                //如果自动排程的话，则需要更新后面所有的计划日期
                if (!plan.IsAutoAps) return;

                var date = task.PlanStartDate.AddDays((int)task.Duration);
                var afterTasks = Repository.Orm.Select<TaskEntity>()
                    .Where(x => x.PlanId == task.PlanId && x.OrderIndex > task.OrderIndex)
                    .OrderBy(x => x.OrderIndex).ToList();
                foreach (var item in afterTasks)
                {
                    item.PlanStartDate = date;
                    item.PlanEndDate = date.AddDays((int)item.Duration);
                    date = item.PlanEndDate.Value;
                }
                //只更新特定字段，减少数据库语句
                Repository.Orm.Update<TaskEntity>()
                .SetSource(afterTasks)
                .UpdateColumns(x => new { x.PlanStartDate, x.PlanEndDate })
                .ExecuteAffrows();

            };

            //修改指定字段
            var oldValue = task.UpdateColumn(edit.FiledName, edit.Value);
            int num = Repository.Orm.Update<TaskEntity>()
                .SetSource(task)
                //.UpdateColumns(new string[] { edit.FiledName })
                .ExecuteAffrows();

            var logModel = new PlanLogEntity();
            logModel.SetPlan(plan);
            logModel.SetTask(task);
            logModel.UpdateTask(CurrentUser.Value, edit.FiledName, oldValue, edit.Value);
            logService.Insert(logModel);

            return num;
        }

        /// <summary>
        /// 根据项目编码查询计划名称列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<PlanDto> GetPlans(string projectCode)
        {
            if (!string.IsNullOrWhiteSpace(projectCode))
            {
                //var planList = GetPlans(projectCode);
                var list = Repository.Where(o => o.ProjectCode == projectCode).OrderBy(x => x.OrderIndex).ToList()
                    .Select(t =>
                  {
                      var Output = Mapper.Map<PlanDto>(t);
                      Output.Status = t.StatusText();
                      //Output.Tasks = Repository.Orm.Select<TaskEntity>().Where(o => o.PlanId == Output.Id).ToList().Select(x => Mapper.Map<TaskDto>(x)).ToList();
                      return Output;
                  }).ToList();
                return list;
            }
            else
            {
                return null;
            }
        }

        public List<TaskDto> GetTasks(int planId)
        {
            var list = Repository.Orm
                .Select<TaskEntity>()
                .Where(o => o.PlanId == planId)
                .OrderBy(x => x.OrderIndex)
                .ToList()
                .Select(t =>
                {
                    var Output = Mapper.Map<TaskDto>(t);
                    //Output.Tasks = Repository.Orm.Select<TaskEntity>().Where(o => o.PlanId == Output.Id).ToList().Select(x => Mapper.Map<TaskDto>(x)).ToList();
                    return Output;
                }).ToList();
            return list;
        }

        /// <summary>
        /// 导入模板中的计划和任务
        /// </summary>
        public int ImportPlanTask(ImportDto import)
        {
            //计划模板
            var templatePlan = Repository.Orm.Select<WbsTabsEntity>().Where(x => x.Id == import.TemplatePlanId).First();
            if (templatePlan == null)
                throw new NullReferenceException("指定的模版任务不存在！");
            var plan = Repository.Orm.Select<PlanEntity>().Where(x => x.Id == import.CurrentPlanId).First();
            if (plan == null)
                throw new NullReferenceException("当前任务不存在！");
            //任务模板
            var templateTasks = Repository.Orm.Select<WbsPlanEntity>().Where(x => x.TemplateId == import.TemplatePlanId).ToList();

            var maxOrder = Repository.Orm.Select<TaskEntity>().Where(x => x.PlanId == plan.Id).Max(x => x.OrderIndex);

            DateTime startTime = plan.PlanDate.Date;
            //转成当前任务
            var tasks = templateTasks.Select(x =>
            {
                var task = Mapper.Map<TaskEntity>(x);
                //TODO:ParnetId怎么导，ID是自动生成的。
                task.PlanId = import.CurrentPlanId;
                task.PlanStartDate = plan.NextWorkDay(startTime.AddDays(-1));
                task.PlanEndDate = plan.WorkEnd(task.PlanStartDate, (int)task.Duration);
                task.OrderIndex += ++maxOrder;

                return task;
            }).ToList();

            //批量插入到当前计划下的所有任务（建议要手动清楚掉之前的任务）
            int num = Repository.Orm.Insert<TaskEntity>().AppendData(tasks).ExecuteAffrows();
            return num;
        }

        /// <summary>
        /// 交换2个任务的位置
        /// </summary>
        public int MoveTask(MoveTaskDto move)
        {
            var fromEntity = Repository.Orm.Select<TaskEntity>().Where(x => x.Id == move.From).First();
            var destEntity = Repository.Orm.Select<TaskEntity>().Where(x => x.Id == move.Dest).First();

            if (fromEntity == null || destEntity == null)
                return -1;

            var plan = Repository.Where(x => x.Id == fromEntity.PlanId).First();

            var old = destEntity.OrderIndex;
            destEntity.OrderIndex = fromEntity.OrderIndex;
            fromEntity.OrderIndex = old;

            var updateRepository = Repository.Orm.Update<TaskEntity>();
            updateRepository.SetSource(fromEntity).SetSource(destEntity).UpdateColumns(x => x.OrderIndex).ExecuteAffrows();

            var logModel = new PlanLogEntity();
            logModel.SetPlan(plan);
            logModel.SetTask(fromEntity);
            logModel.MoveTask(CurrentUser.Value, destEntity);
            logService.Insert(logModel);

            return 1;
        }

        public int UpdatePlan(int id, PlanDto input)
        {
            return this.Update(id, input);
        }
    }
}
