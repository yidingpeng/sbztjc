using Hangfire;
using Hangfire.Common;
using Hangfire.Server;
using Microsoft.Extensions.Logging;
using RW.PMS.CrossCutting.BackgroundJobs.Hangfire;
using System;
using System.Globalization;
using Hangfire.Storage;

namespace RW.PMS.Infrastructure.BackgroundJobs
{
    //[DisableConcurrentExecution(10)]
    //[AutomaticRetry(Attempts = 0, LogEvents = false, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
    public class TestJob : IHangfireRecurringJob
    {
        private readonly ILogger<TestJob> _logger;

        public TestJob(ILogger<TestJob> logger)
        {
            _logger = logger;
        }

        public void Execute(PerformContext context)
        {
            _logger.LogInformation($"{DateTime.Now.ToString(CultureInfo.InvariantCulture)}-param:{context.GetJobData<string>("param")}");
        }
    }
}
