using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using RW.PMS.CrossCutting.Extensions;

namespace RW.PMS.CrossCutting.BackgroundJobs.Hangfire
{
    public static class HangfireExtension
    {
        public static IGlobalConfiguration UseRecurringJob(this IGlobalConfiguration configuration, string jsonFile)
        {
            var configFile = File.Exists(jsonFile) ? jsonFile : Path.Combine(AppContext.BaseDirectory, jsonFile);
            if (!File.Exists(configFile))
                throw new FileNotFoundException($"The json file {configFile} does not exist.");
            using var fileStream = File.OpenRead(configFile);
            using var reader = new StreamReader(fileStream);
            var content = reader.ReadToEnd();
            var jobs = SerializationHelper.Deserialize<List<RecurringJobInfo>>(content);
            if (jobs.Any())
            {
                using var connection = JobStorage.Current.GetConnection();
                foreach (var jobInfo in jobs)
                {
                    var method = jobInfo.JobType.GetMethod(nameof(IHangfireRecurringJob.Execute));
                    if (method != null && method.DeclaringType != null)
                    {
                        //根据方法参数类型生成参数默认值
                        var parameters = method.GetParameters();
                        var args = new Expression[parameters.Length];
                        for (var i = 0; i < parameters.Length; i++)
                            args[i] = Expression.Default(parameters[i].ParameterType);
                        //生成方法调用表达式树
                        var p = Expression.Parameter(method.DeclaringType, "p");
                        var methodCall = method.IsStatic
                            ? Expression.Call(method, args)
                            : Expression.Call(p, method, args);

                        //调用RecurringJob.AddOrUpdate
                        var addOrUpdate = Expression.Call(
                            typeof(RecurringJob),
                            nameof(RecurringJob.AddOrUpdate),
                            new[] {method.DeclaringType},
                            new Expression[]
                            {
                                Expression.Constant(jobInfo.JobName),
                                Expression.Lambda(methodCall, p),
                                Expression.Constant(jobInfo.Cron),
                                Expression.Constant(jobInfo.TimeZone.NotNullOrWhiteSpace()
                                    ? TimeZoneInfo.FindSystemTimeZoneById(jobInfo.TimeZone)
                                    : TimeZoneInfo.Utc),
                                Expression.Constant(jobInfo.Queue.NotNullOrWhiteSpace()
                                    ? jobInfo.Queue
                                    : EnqueuedState.DefaultQueue)
                            });
                        Expression.Lambda(addOrUpdate).Compile().DynamicInvoke();

                        //添加参数
                        using (connection.AcquireDistributedLock($"recurring-job-jobArgs:{jobInfo.JobName}",
                            TimeSpan.FromMinutes(1)))
                        {
                            var changedFields = new Dictionary<string, string>
                            {
                                [nameof(RecurringJobInfo.JobArgs)] = SerializationHelper.Serialize(jobInfo.JobArgs)
                            };
                            connection.SetRangeInHash($"recurring-job:{jobInfo.JobName}", changedFields);
                        }
                    }
                }
            }

            return configuration;
        }
    }
}