using Microsoft.AspNetCore.Mvc;
using MiniExcelLibs;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Log;
using RW.PMS.Application.Contracts.System;

namespace RW.PMS.API.Controllers
{
    public class SystemLogController : RWBaseController
    {
        private readonly ILogService logService;
        public SystemLogController(ILogService logService)
        {
            this.logService = logService;
        }

        [HttpGet]
        public IResponseDto GetLogs([FromQuery] LogQueryInputDto input)
        {
            var list = logService.GetPagedList(input);
            return ResponseDto.Success(list);
        }
        [HttpGet("GetLogAllList")]
        public ActionResult GetLogAllList()
        {
            var list = logService.GetLogsAllList();
            //2.将数据源存放到内存里
            //MemoryStream在内存中开辟一块空间
            var memoryStream = new MemoryStream();
            //将list数据原】源存放到内存里
            memoryStream.SaveAs(list.List.ToList());
            //offset偏移量，0开始
            memoryStream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = "用户信息表" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx"
            };
        }
    }
}
