using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DeviceFile;
using RW.PMS.Application.Contracts.DTO;

namespace RW.PMS.API.Controllers.DeviceFile
{
    public class DeviceController : RWBaseController
    {
       
        private readonly IDeviceService _devicweService;
        public DeviceController( IDeviceService devicweService)
        {
            _devicweService = devicweService;
        }
        [HttpGet("GetList")]
        public IResponseDto GetList()
        {
            var result = _devicweService.getdeviceAlllist();
            return ResponseDto.Success(result);
        }
    }
}
