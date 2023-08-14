using System;
using System.Collections.Generic;
using System.Dynamic;

namespace RW.PMS.CrossCutting.BackgroundJobs.Hangfire
{
    /// <summary>
    ///     任务信息实体
    /// </summary>
    public class RecurringJobInfo
    {
        public string JobName { get; set; }

        public Type JobType { get; set; }

        public string Cron { get; set; }

        public string TimeZone { get; set; }

        public string Queue { get; set; }

        public IDictionary<string, object> JobArgs { get; set; }
    }
}