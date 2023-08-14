using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.Basics;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Basics;
using RW.PMS.Application.NpoiHelper;
using RW.PMS.CrossCutting.Extensions;

namespace RW.PMS.API.Controllers
{
    public class LogDetailsController : RWBaseController
    {
        private readonly ILogDetailsService _logDetailsService;
        public LogDetailsController(ILogDetailsService logDetailsService)
        {
            _logDetailsService = logDetailsService;
        }

        [HttpGet]
        public IResponseDto GetList([FromQuery] LogdetailsSearchDto input)
        {
            if (input.startTime.NotNullOrWhiteSpace())
            {
                input.startTime = Convert.ToDateTime(input.startTime).ToShortDateString();
                input.endTime = Convert.ToDateTime(input.endTime).ToShortDateString();
            }
            var result = _logDetailsService.GetPagedList(input);
            return ResponseDto.Success(result);
        }
    }
}
