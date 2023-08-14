using Microsoft.AspNetCore.Mvc;
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
    }
}
