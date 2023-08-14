using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DeviceFile;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.DeviceFile;

namespace RW.PMS.API.Controllers.DeviceFile
{
    public class RoomController : RWBaseController
    
    {
     
        private readonly ITestRoomService _testRoomService;
        public RoomController(ITestRoomService testRoomService)
        {
            _testRoomService = testRoomService;
        }
        [HttpGet("GetList")]
        public IResponseDto GetList()
        {
            var result = _testRoomService.getroomAlllist();
            return ResponseDto.Success(result);
        }
    }
}
