using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.Input.Message;
using RW.PMS.Application.Contracts.Message;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.CrossCutting.Extensions;
using System.Collections;
using System.Net;
using System.Text;

namespace RW.PMS.API.Controllers
{
    public class ThirdPartController : Controller
    {
        private readonly IThirdPartService _thirdPartService;
        private readonly IConfiguration _configuration;
        private readonly IProjectBasicsService _basicsService;
        public ThirdPartController(IThirdPartService thirdPartService, IConfiguration configuration, IProjectBasicsService basicsService)
        {
            _thirdPartService = thirdPartService;
            _configuration = configuration;
            _basicsService = basicsService;
        }
        /// <summary>
        /// ESB SendMessage
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("ESBPostSendMessage")]
        public IResponseDto PostSendMessage()
        {
            string esburl = _configuration["ESBUrl:key"] + "/sendMessage";
            string plmurl = _configuration["PLMUrl:key"];

            //var result = _thirdPartService.PostRequestAsync(esburl, datas);
            var result = _basicsService.MessagePush(plmurl, esburl);
            return ResponseDto.Success(result);
        }
        [HttpPost("ESBSendMessageTest")]
        public IResponseDto PostSendMessageTest()
        {
            string esburl = _configuration["ESBUrl:key"] + "/sendMessage";
            string plmurl = _configuration["PLMUrl:key"];

            //var result = _thirdPartService.PostRequestAsync(esburl, datas);
            ArrayList myArrayList = new ArrayList();
            myArrayList.Add(new { type = "user", id = "13166107390" });
            var data = new ESBSendMessageInput
            {
                Title = "测试的消息提醒",
                Type = 1,
                Desc = "",
                Link = plmurl,
                From = "PLM",
                TargetPlantform = "OA",
                Targets = myArrayList.ToArray(),
                SendMode = 0,
                SendModeValue = ""
            };
            var result = _thirdPartService.PostRequestAsync(esburl, data);
            return ResponseDto.Success(result);
        }
        /// <summary>
        /// 项目团队消息推送
        /// </summary>
        /// <returns></returns>
        [HttpPost("TeamNewsFeed")]
        public IResponseDto TeamNewsFeed()
        {
            string esburl = _configuration["ESBUrl:key"] + "/sendMessage";
            string plmurl = _configuration["PLMUrl:key"];
            var result = _basicsService.MessagePush(plmurl, esburl);
            return ResponseDto.Success(result);
        }
    }
}
