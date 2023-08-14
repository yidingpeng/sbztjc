using System.Collections.Generic;
using Hangfire;
using Hangfire.Common;
using Hangfire.Server;

namespace RW.PMS.CrossCutting.BackgroundJobs.Hangfire
{
    public static class PerformContextExtension
    {
        public static T GetJobData<T>(this PerformContext context, string name)
        {
            using var connection = JobStorage.Current.GetConnection();
            var value = connection.GetJobParameter(context.BackgroundJob.Id, "RecurringJobId");
            var jobId = SerializationHelper.Deserialize<string>(value);
            var hash = connection.GetAllEntriesFromHash($"recurring-job:{jobId}");
            if (!hash.ContainsKey(nameof(RecurringJobInfo.JobArgs))) return default;
            var jobArgs =
                SerializationHelper.Deserialize<Dictionary<string, object>>(hash[nameof(RecurringJobInfo.JobArgs)]);
            if (!jobArgs.ContainsKey(name)) return default;
            var arg = jobArgs[name];
            var json = SerializationHelper.Serialize(arg);
            return SerializationHelper.Deserialize<T>(json);
        }
    }
}