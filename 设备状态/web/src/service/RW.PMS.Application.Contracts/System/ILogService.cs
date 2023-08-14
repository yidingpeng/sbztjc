using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Log;
using RW.PMS.Domain.Entities.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.System
{
    public interface ILogService : ICrudApplicationService<LogEntity, int>
    {
        PagedResult<LogOutputDto> GetPagedList(LogQueryInputDto input);
        PagedResult<LogOutputDto> GetLogsAllList();
        bool AddLog(LogInputDto input);

        bool AddErrorLog(string msg, string desc);

        bool AddOperationLog(bool result, string msg, string desc);

        bool AddLoginLog(bool result, string msg, string desc);
    }
}
