using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.File;
using RW.PMS.Application.Contracts.DTO.Workflow;
using RW.PMS.Application.Contracts.Service;

namespace RW.PMS.API.Controllers
{
    public class WorkflowController : RWBaseController
    {
        IWorkflowService workflowService;
        IUserWorkflowService userflowService;
        public WorkflowController(
            IWorkflowService workflowService,
        IUserWorkflowService userflowService
            )
        {
            this.workflowService = workflowService;
            this.userflowService = userflowService;
        }

        [HttpGet]
        public IResponseDto GetList([FromQuery] WorkflowQueryDto input)
        {
            var list = workflowService.GetPagedList(input);
            return ResponseDto.Success(list);
        }

        [HttpGet("{id}")]
        public IResponseDto GetOne(int id)
        {
            //var model = mapper.Map<WorkflowListDto>(workflowService.Get(id));
            var model = workflowService.GetFlow(id);

            return ResponseDto.Success(model);
        }

        [HttpDelete("{id}")]
        public IResponseDto DeleteOne(int id)
        {
            var c = workflowService.Remove(id);

            return ResponseDto.GetResult(c, "删除成功！");
        }

        [HttpPut("{id}")]
        public IResponseDto Update(int id, [FromBody] EditWorkflowDto model)
        {
            var result = workflowService.Modify(id, model);
            return ResponseDto.UpdateResult(result);
        }

        [HttpPost]
        public IResponseDto Add(AddWorkflowDto model)
        {
            var result = workflowService.Add(model);
            return ResponseDto.InsertResult(result);
        }

        [HttpPost("my")]
        public IResponseDto AddMine(AddUserFlowDto input)
        {
            var result = userflowService.AddMine(input);
            return ResponseDto.InsertResult(result);
        }

        [HttpPut("my/{id}")]
        public IResponseDto ModifyMine(int id, EditUserFlowDto input)
        {
            var result = userflowService.ModifyMine(id, input);
            return ResponseDto.UpdateResult(result);
        }

        [HttpDelete("my/{id}")]
        public IResponseDto DeleteMine(int id)
        {
            var result = userflowService.RemoveMine(id);
            return ResponseDto.DeleteResult(result);
        }

        [HttpPut("cancel/{id}")]
        public IResponseDto CancelMine(int id)
        {
            var result = userflowService.CancelMine(id);
            return ResponseDto.UpdateResult(result, "撤销流程成功", "撤销流程失败");
        }

        [HttpGet("my")]
        public IResponseDto MyList([FromQuery] UserFlowQueryDto input)
        {
            var lst = userflowService.GetUserFlowPagedList(input);
            return ResponseDto.Success(lst);
        }

        [HttpGet("top")]
        public IResponseDto MyTop(int count)
        {
            var lst = userflowService.GetUserFlowPagedList(new UserFlowQueryDto { PageSize = count, Status = Domain.Entities.Workflow.UserFlowStatus.Approving });
            return ResponseDto.Success(lst);
        }

        [HttpGet("my/{id}")]
        public IResponseDto MyOne(int id)
        {
            var item = userflowService.GetUserFlow(id);
            return ResponseDto.Success(item);
        }

        [HttpPost("audit")]
        public IResponseDto Audit(AuditSubmitDto dto)
        {
            var result = userflowService.AuditFlow(dto);
            return ResponseDto.Success("提交完成");
        }

        [HttpGet("files")]
        public IResponseDto GetFiles([FromQuery] FileQueryDto query)
        {
            var files = userflowService.GetMyFiles(query);
            return ResponseDto.Success(files);
        }

        [HttpPost("urging/{id}")]
        public IResponseDto Urging(int id)
        {
            bool result = userflowService.Urging(id);
            return ResponseDto.GetResult(result, "催办成功！", "催办失败");
        }

        //[HttpGet("no")]
        //public IResponseDto No()
        //{
        //    var data = workflowService.GenerateNo();
        //    return ResponseDto.Success<string>(data);

        //}

    }
}
