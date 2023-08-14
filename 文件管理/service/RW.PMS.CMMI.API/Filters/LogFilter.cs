using Microsoft.AspNetCore.Mvc.Filters;
using RW.PMS.Application.Contracts.System;

namespace RW.PMS.API.Filters
{
    public class LogFilter : ActionFilterAttribute
    {
        ILogService log;
        public LogFilter(ILogService log)
        {
            this.log = log;
        }

        public string PageName { get; set; }
        public string ActionName { get; set; }

        public string Format { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            base.OnResultExecuting(context);
        }


        public override void OnResultExecuted(ResultExecutedContext context)
        {
            //log.AddLog(new Application.Contracts.DTO.Log.LogOutputDto
            //{

            //});
            base.OnResultExecuted(context);
        }
    }
}
