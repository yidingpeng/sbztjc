using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DeviceStatus;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.DeviceStatus;

namespace RW.PMS.API.Controllers.device
{
    public class OPCpointController : RWBaseController
    {
        private readonly IOPCpointService _oPCpointService;
        public OPCpointController(IOPCpointService oPCpointService)
        {

            _oPCpointService = oPCpointService;
        }
        [HttpGet("GetList")]
        public IResponseDto GetList([FromQuery] OPCpointDto input)
        {
            var result = _oPCpointService.getList(input);
            return ResponseDto.Success(result);
        }
        [HttpPut]
        public IResponseDto DoEdit([FromBody] OPCpointDto input)
        {
            if (input.id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            var list = _oPCpointService.GetList().Where(s => s.Address.Equals(input.Address)&&s.TagName.Equals(input.TagName)&&s.ExplainInfo.Equals(input.ExplainInfo)).Count();
            if (list > 0) return ResponseDto.Error(500, "此地址已存在！");
            _oPCpointService.Update(input);
            return ResponseDto.Success("修改成功！");
        }

        [HttpPost]
        public IResponseDto DoAdd([FromBody] OPCpointDto input)
        {
            var list = _oPCpointService.GetList().Where(s => s.Address.Equals(input.Address) && s.Id != input.id).Count();
            if (list > 0) return ResponseDto.Error(500, "此地址已存在！");
            var result = _oPCpointService.Insert(input);
            return ResponseDto.Success("添加成功！");
        }
        [HttpDelete]
        public IResponseDto DoDelete([FromBody] DeleteRequesDto model)
        {
            var ids = model.GetIds();
            if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
            int count = _oPCpointService.Delete(ids);
            return ResponseDto.Success($"成功删除了{count}条数据。");
        }
    }
}
