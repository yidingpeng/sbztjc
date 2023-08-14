using RW.PMS.Application.Contracts.DTO.Plan.Log;
using RW.PMS.Domain.Entities.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Plan
{
    public interface IPlanLogService : ICrudApplicationService<PlanLogEntity, int>
    {
        int AddLog(AddPlanLogDto log);
        int ClearLog();

        List<PlanLogListDto> GetLogs(PlanLogSearchDto search);
    }
}
