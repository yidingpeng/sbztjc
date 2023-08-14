using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.Basics;
using RW.PMS.Application.Contracts.DeviceFile;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Basics;
using RW.PMS.Application.Contracts.DTO.DeviceFile;
using RW.PMS.Application.DeviceFile;

namespace RW.PMS.API.Controllers
{
    public class FileInformationController : RWBaseController
    {
        private readonly IFileInformationService _fileInformation;
        private readonly ITestRoomService _testRoomService;
        private readonly ILogDetailsService  _logDetailsService;
        public FileInformationController(IFileInformationService fileInformation, ITestRoomService testRoomService, ILogDetailsService logDetailsService)
        {
            _fileInformation = fileInformation;
            _testRoomService = testRoomService;
            _logDetailsService = logDetailsService;
        }

        [HttpGet]
        public IResponseDto GetList([FromQuery] FileInformationSearchDto input)
        {
            var result = _fileInformation.GetPagedList(input);
            return ResponseDto.Success(result);
        }
        [HttpPost("DoAdd")]
        public IResponseDto DoAdd([FromBody] FileInformationSearchDto input)
        {
            DateTime now= DateTime.Now;
            var logdetal = new LogdetailsDto() { LogDate = now, LogThread = "11",LogLevel="DownLoad",LogLogger= "loginfo",LogMessage="download下载文件："+input.FileName,RequestType="文件下载",Path=input.FilePath };
            var result = _logDetailsService.addlog(logdetal);
            return ResponseDto.Success("添加成功！");
        }


    }
}
