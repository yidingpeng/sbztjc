using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.BOM;
using RW.PMS.Application.Contracts.BOM.DTO;
using RW.PMS.Application.Contracts.DTO;

namespace RW.PMS.API.Controllers.BOM
{
    public class BOMController : RWBaseController
    {
        IBOMService bomService;
        public BOMController(IBOMService bomService)
        {
            this.bomService = bomService;
        }

        [HttpGet]
        public IResponseDto GetList([FromQuery] BOMTableSearchDto search)
        {
            //List<object> lst = new List<object>();
            //lst.Add(new { Id = "123", BomName = "TestName", BomCode = "NO1231", ProjectCode = "GJCS123456", ProjectName = "项目名称1", Version = "VA.0" });
            //lst.Add(new { Id = "123", BomName = "TestName", BomCode = "NO1232", ProjectCode = "GJCS123456", ProjectName = "项目名称2", Version = "VB.0" });
            //return ResponseDto.Success(new PagedResult<object>(lst));

            var list = bomService.GetBOMList(search);
            return ResponseDto.Success(list);
        }

        [HttpPost("approve")]
        public IResponseDto Approve([FromBody] CommentResult comment)
        {
            var result = bomService.Approve(comment);
            return ResponseDto.GetResult(result, "审批成功！", "审批失败！");
        }

        [HttpGet("{id}")]
        public IResponseDto GetDetail(int id)
        {
            var detail = bomService.GetBOM(id);
            return ResponseDto.Success(detail);
        }

        [HttpPost("undo/{id}")]
        public IResponseDto UndoBOM(int id)
        {
            var result = bomService.UndoBOM(id);
            return ResponseDto.GetResult(result, "撤销成功！", "撤销失败！");
        }

        [HttpGet("my")]
        public IResponseDto MyBOM([FromQuery] MyBomTableSearchDto dto)
        {
            var result = bomService.GetMyBOM(dto);
            return ResponseDto.Success(result);
        }
    }
}
