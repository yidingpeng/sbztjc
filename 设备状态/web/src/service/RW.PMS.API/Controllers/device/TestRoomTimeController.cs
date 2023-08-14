using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DeviceStatus;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.DeviceStatus;

namespace RW.PMS.API.Controllers.device
{
    public class TestRoomTimeController : RWBaseController
    {
        private readonly ITestRoomTimeService _testRoomTimeService;
        public TestRoomTimeController(ITestRoomTimeService testRoomTimeService)
        {

            _testRoomTimeService = testRoomTimeService;
        }

        [HttpPost("DoAddweibao")]
        public IResponseDto DoAddweibao([FromBody] TestRoomTimesDto input)
        {
            
            var result = _testRoomTimeService.Insertweibao(input);
            return ResponseDto.Success("添加成功！");
        }
        [HttpPost("DoAddholiday")]
        public IResponseDto DoAddholiday([FromBody] TestRoomTimesDto input)
        {

            var result = _testRoomTimeService.Insertholiday(input);
            return ResponseDto.Success("添加成功！");
        }
        [HttpPost("DoAddzhidu")]
        public IResponseDto DoAddzhidu([FromBody] TestRoomTimesDto input)
        {

            var result = _testRoomTimeService.Insertzhidu(input);
            return ResponseDto.Success("添加成功！");
        }
    }
}
