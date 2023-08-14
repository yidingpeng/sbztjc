using System.Collections.Generic;
using Hangfire.Server;

namespace RW.PMS.CrossCutting.BackgroundJobs.Hangfire
{
    public interface IHangfireRecurringJob : IRecurringJob
    {
        void Execute(PerformContext context);
    }
}