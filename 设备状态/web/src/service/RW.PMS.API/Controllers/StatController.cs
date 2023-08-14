using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.Probelm;
using RW.PMS.Application.Contracts.Service;

namespace RW.PMS.API.Controllers
{
    public class StatController : RWBaseController
    {
        IStatService statSerivce;
        IProblemFeedbackService problemService;
        public StatController(IStatService statSerivce, IProblemFeedbackService problemService)
        {
            this.statSerivce = statSerivce;
            this.problemService = problemService;
        }

        [HttpGet("user")]
        public IResponseDto GetUserStat()
        {
            var data = statSerivce.GetUserStat();
            return ResponseDto.Success(data);
        }

        [HttpGet("workflow")]
        public IResponseDto GetWorkflowStat()
        {
            var data = statSerivce.GetWorkflowStat();
            return ResponseDto.Success(data);
        }

        [HttpGet("issueTop")]
        public IResponseDto GetIssueTop(int count, string deal)
        {
            var data = problemService.GetPagedList(new ProblemFeedbackSerchDto { PageSize = 10, PageNo = 1, DealWithDynamic = deal });
            return ResponseDto.Success(data.List);
        }
    }
}
