using System.Text.Json;
using Microsoft.Extensions.Localization;
using RW.PMS.Application.Contracts.DTO.Log;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.Exceptions;
using RW.PMS.CrossCutting.Extensions;

namespace RW.PMS.API.Middleware
{
    public class ExceptionMiddleWare : IMiddleware
    {
        ILogService log;
        public ExceptionMiddleWare(ILogService log)
        {
            this.log = log;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                if (e is LogicException logicException)
                    await HandleLogicExceptionAsync(context, logicException);
                else if (e is DataValidatorException dataValidatorException)
                    await HandleDataValidatorException(context, dataValidatorException);
                else
                    await HandleCommonException(context, e);
            }
        }

        private async Task HandleLogicExceptionAsync(HttpContext context, LogicException e)
        {
            context.Response.ContentType = "application/json";
            var message = e.Message;
            if (message.IsNullOrWhiteSpace())
            {
                var stringLocalizer =
                    context.RequestServices.GetRequiredService<IStringLocalizer<ExceptionCode>>();
                message = stringLocalizer[e.Code.ToString()].Value;
            }
            log.AddErrorLog("操作失败", message);

            await context.Response.WriteAsync(JsonSerializer.Serialize(new { e.Code, Msg = message },
                typeof(object), new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }

        private async Task HandleDataValidatorException(HttpContext context, DataValidatorException e)
        {
            context.Response.ContentType = "application/json";
            var stringLocalizer = context.RequestServices.GetRequiredService<IStringLocalizer<ExceptionCode>>();
            var data = e.ValidationResults.GroupBy(v => v.ErrorField).Select(v => new { Field = v.Key, Error = v.Select(t => t.ErrorMessage) });
            await context.Response.WriteAsync(JsonSerializer.Serialize(
                new { Code = ExceptionCode.ParamError, Msg = stringLocalizer[ExceptionCode.ParamError.ToString()].Value, Data = data },
                typeof(object), new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
        }

        private async Task HandleCommonException(HttpContext context, Exception e)
        {
            log.AddLog(new LogInputDto
            {
                Message = e.Message,
                Desc = e.ToString(),
                Result = false,
                Type = LogTypeEnum.Error,
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { Code = 500, Msg = e.Message, Desc = e.ToString() }));

        }
    }

    public static class ExceptionMiddleWareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleWare(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleWare>();
        }
    }
}