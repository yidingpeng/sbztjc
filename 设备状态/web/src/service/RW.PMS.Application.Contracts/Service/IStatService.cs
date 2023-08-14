using RW.PMS.Application.Contracts.DTO.Stat;
using RW.PMS.Domain.Entities.Stat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Service
{
    public interface IStatService : ICrudApplicationService<StatDataEntity, int>
    {
        Dictionary<string, int> GetStats(DateTime date, string type);

        int GetStat(DateTime now, string type, string key);

        bool SetStat(DateTime date, string type, string key, int value = 1);
        UserStatDto GetUserStat();
        WorkflowStatDto GetWorkflowStat();
    }
}
