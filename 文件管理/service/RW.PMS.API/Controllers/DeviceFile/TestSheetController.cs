using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DeviceFile;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.DeviceFile;

namespace RW.PMS.API.Controllers.DeviceFile
{
    public class TestSheetController : RWBaseController
    {
        private readonly ITestSheetService _testSheetService;
        private readonly ITestSheetTimeCountService _testSheetTimeCountService;
     public TestSheetController( ITestSheetService testSheetService, ITestSheetTimeCountService testSheetTimeCountService)
        {
            _testSheetService = testSheetService;
            _testSheetTimeCountService= testSheetTimeCountService;
        }

        [HttpPost("DoAdd")]
        public IResponseDto DoAdd([FromBody] TestSheetDto input)
        {
            var result = _testSheetService.insertTestSheet(input);
            return ResponseDto.Success("添加成功！");
        }
        [HttpGet("getTestSheetAllList")]
        public IResponseDto getTestSheetAllList([FromQuery] TestSheetDto input)
        {
            var result = _testSheetService.getTestSheetAllList(input);
            return ResponseDto.Success(result);
        }
        [HttpPost("DoEdit")]
        public IResponseDto DoEdit([FromBody] TestSheetDto input)
        {
            //  var IsExist = _deviceStatusService.AccountExist(input);
            //if (IsExist) return ResponseDto.Error(500, "该用户名已存在");
            // input.createTime = DateTime.Now;
            _testSheetService.Update(input);
            return ResponseDto.Success("修改成功！");
        }
        [HttpGet("GetRepeat")]
        public IResponseDto GetRepeat([FromQuery] TestSheetDto input)
        {
            bool model = _testSheetService.Repeatjudgment(input);

            return ResponseDto.Success(model);
        }
        [HttpPost("Docountdataone")]
        public IResponseDto Docountdataone([FromBody] DeviceTestSheetTimeCountDto input)
        {
            //  var IsExist = _deviceStatusService.AccountExist(input);
            //if (IsExist) return ResponseDto.Error(500, "该用户名已存在");
            // input.createTime = DateTime.Now;
            input.alarmTime = DateTime.Now;
            _testSheetTimeCountService.insertTestSheet(input);
            return ResponseDto.Success("添加成功！");
        }
    }
}
