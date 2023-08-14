using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO.DeviceStatus;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DeviceStatus;
using RW.PMS.Application.DeviceStatus;
using RW.PMS.CrossCutting.Extender;
using MiniExcelLibs;

namespace RW.PMS.API.Controllers.device
{
    public class DRoomDataCountController : RWBaseController
    {
        private readonly IDRoomDataCountService _dRoomDataCountService;
        public DRoomDataCountController(IDRoomDataCountService dRoomDataCountService) {
        _dRoomDataCountService= dRoomDataCountService;
        }
        [HttpGet("getweek")]
        public IResponseDto getweek([FromQuery] DataCountNYRDto input)
        {
            if (input.week == null) {
                input.week = "0";
            }
            if (input.month == null)
            {
                input.month = "0";
            }
            if (input.year == null)
            {
                input.year = "0";
            }
            else
            {
                input.year = input.year.ToDateTime().Year .ToString();
            }
            var result = _dRoomDataCountService.getweekcountvue(input);
            return ResponseDto.Success(result);

        }
        [HttpGet("getmonth")]
        public IResponseDto getmonth([FromQuery] DataCountNYRDto input)
        {
            if (input.month == null)
            {
                input.month = "0";
            }
            if (input.year == null)
            {
                input.year = "0";
            }
            else
            {
                input.year = input.year.ToDateTime().Year .ToString();
            }
            var result = _dRoomDataCountService.GetMonthCountvue(input);
            return ResponseDto.Success(result);

        }
        [HttpGet("getyear")]
        public IResponseDto getyear([FromQuery] DataCountNYRDto input)
        {
            
            if (input.year == null)
            {
                input.year = "0";
            }
            else
            {
                input.year = input.year.ToDateTime().Year.ToString();
            }
            var result = _dRoomDataCountService.GetyearCountvue(input);
            return ResponseDto.Success(result);

        }

        [HttpGet("importweekcount")]
        public ActionResult importweekcount([FromQuery] DataCountNYRDto input)
        {
            if (input.month == null)
            {
                input.month = "0";
            }
            if (input.year == null)
            {
                input.year = "0";
            }
            else
            {
                input.year = input.year.ToDateTime().Year.ToString();
            }
            var list = _dRoomDataCountService.getweekcountvueimport(input);
            //2.将数据源存放到内存里
            //MemoryStream在内存中开辟一块空间
            var memoryStream = new MemoryStream();
            //将list数据原】源存放到内存里
            memoryStream.SaveAs(list.ToList());
            //offset偏移量，0开始
            memoryStream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = "台架周统计表" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx"
            };
        }

        [HttpGet("importmonthcount")]
        public ActionResult importmonthcount([FromQuery] DataCountNYRDto input)
        {
            if (input.month == null)
            {
                input.month = "0";
            }
            if (input.year == null)
            {
                input.year = "0";
            }
            else
            {
                input.year = input.year.ToDateTime().Year.ToString();
            }
            var list = _dRoomDataCountService.GetMonthCountvueimport(input);
            //2.将数据源存放到内存里
            //MemoryStream在内存中开辟一块空间
            var memoryStream = new MemoryStream();
            //将list数据原】源存放到内存里
            memoryStream.SaveAs(list.ToList());
            //offset偏移量，0开始
            memoryStream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = "台架月统计表" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx"
            };
        }
        [HttpGet("importyearcount")]
        public ActionResult importyearcount([FromQuery] DataCountNYRDto input)
        {
            if (input.month == null)
            {
                input.month = "0";
            }
            if (input.year == null)
            {
                input.year = "0";
            }
            else
            {
                input.year = input.year.ToDateTime().Year.ToString();
            }
            var list = _dRoomDataCountService.GetyearCountvueimport(input);
            //2.将数据源存放到内存里
            //MemoryStream在内存中开辟一块空间
            var memoryStream = new MemoryStream();
            //将list数据原】源存放到内存里
            memoryStream.SaveAs(list.ToList());
            //offset偏移量，0开始
            memoryStream.Seek(0, SeekOrigin.Begin);
            return new FileStreamResult(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            {
                FileDownloadName = "台架年统计表" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx"
            };
        }
    }
}
