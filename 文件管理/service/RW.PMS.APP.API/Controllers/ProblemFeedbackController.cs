using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Problem;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Probelm;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.Exceptions;
using RW.PMS.CrossCutting.Security.User;

namespace RW.PMS.API.Controllers
{
    public class ProblemFeedbackController : RWBaseController
    {
        private readonly IProblemFeedbackService _ProblemFeedbackService;
        private readonly IProblemFeedbackdetailService _ProblemFeedbackdetailService;
        private readonly IDictionaryService _dictionaryService;
        private readonly IProjectBasicsService _projectBasicsService;
        private readonly IWebHostEnvironment _evn;
        private readonly IConfiguration _configuration;
        public readonly ICurrentUser CurrentUser;
        private readonly IMsgSendService _send;
        private readonly IConfigService _config;
        private readonly IUserService _user;
        public readonly ILogService _log;
        public ProblemFeedbackController(IProblemFeedbackService problemFeedbackService,
            IWebHostEnvironment evn, IProblemFeedbackdetailService ProblemFeedbackdetailService,
            ICurrentUser currentUser, IProjectBasicsService projectBasicsService,
            IConfiguration configuration, IDictionaryService dictionaryService, IMsgSendService send,
            IConfigService config, IUserService user, ILogService log)
        {
            _ProblemFeedbackService = problemFeedbackService;
            _projectBasicsService = projectBasicsService;
            _ProblemFeedbackdetailService = ProblemFeedbackdetailService;
            _dictionaryService = dictionaryService;
            CurrentUser = currentUser;
            _configuration = configuration;
            _evn = evn;
            _send = send;
            _config = config;
            _user = user;
            _log = log;
        }
        /// <summary>
        /// 问题反馈信息分页
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("GetList")]
        public IResponseDto GetList([FromQuery] ProblemFeedbackSerchDto search)
        {
            var result = _ProblemFeedbackService.GetPagedList(search);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 所有问题反馈信息
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("GetAllList")]
        public IResponseDto GetAllList(string? key, int PageNo, int PageSize)
        {
            var result = _ProblemFeedbackService.GetAllList(key,PageNo,PageSize);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 根据id查询所有问题反馈信息
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("GetAllListById")]
        public IResponseDto GetAllListById(int id)
        {
            var result = _ProblemFeedbackService.GetProblemFeedbackInfo(id);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 根据id获取反馈图片信息id
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        [HttpGet("GetFeedbackIdByPtid")]
        public IActionResult GetFeedbackIdByPtid(int pid)
        {
            var result = _ProblemFeedbackdetailService.GetFeedbackIdByPtid(pid);
            return new JsonResult(new { ids = result });
        }

        /// <summary>
        /// 添加问题反馈
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("DoAdd")]
        public IResponseDto DoAdd([FromBody] ProblemFeedbackDto input)
        {
            input.DealWithDynamic = "ProblemFBStatus1";
            input.IsQualified = 0;
            input.UserId = CurrentUser?.Id ?? 0;
            var entity = _ProblemFeedbackService.Insert(input);
            var id = _ProblemFeedbackService.GetList().OrderByDescending(a => a.Id).FirstOrDefault();
            if (id != null)
            {
                UpdateImgPid(input.imgFilesId, id.Id);
            }
            //发送消息给责任判定通知人
            string esburl = _configuration["ESBUrl:key"] + "/sendMessage"; 
            bool pdr = _ProblemFeedbackService.MessagePushPD(input.pt_ID, esburl);
            _log.AddOperationLog(true, "新增问题反馈", JsonConvert.SerializeObject(input));
            return entity.Id > 0 ? ResponseDto.Success("添加成功") : ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
        }

        /// <summary>
        /// 修改问题反馈
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// 
        [HttpPut("DoEdit")]
        public IResponseDto DoEdit([FromBody] ProblemFeedbackDto input)
        {
            if (input.Id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            //UpdateImgPid(input.imgFilesId, input.Id);
            _ProblemFeedbackService.Update(input.Id.Value, input);
            return ResponseDto.Success("修改成功！");
        }

        /// <summary>
        /// 将上传的图片修改对应父id
        /// </summary>
        /// <param name="imgFilesId"></param>
        /// <param name="pId"></param>
        [HttpGet("UpdateImgPid")]
        public void UpdateImgPid(string imgFilesId, int? pId)
        {
            _ProblemFeedbackdetailService.UpdateImgPid(imgFilesId,pId);
        }

        /// <summary>
        /// 把该所有父id修改成0
        /// </summary>
        /// <param name="Pid"></param>
        /// <returns></returns>
        [HttpGet("UpdateImgPidtoZero")]
        public IResponseDto UpdateImgPidtoZero(int Pid)
        {
            var result = _ProblemFeedbackdetailService.UpdateImgPidtoZero(Pid);
            return result ? ResponseDto.Success("删除成功") : ResponseDto.Error(0, "删除失败");
        }

        /// <summary>
        /// 处理问题反馈
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// 
        [HttpPut("DoDealWith")]
        public IResponseDto DoDealWith([FromBody] ProblemFeedbackDto input)
        {
            if (input.Id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            //获取该数据的创建时间
            var blem = _ProblemFeedbackService.Get(input.Id.Value);
            //处理状态为已处理（2）时，计算影响时间
            if (input.DealWithDynamic == "ProblemFBStatus5")
            {
                input.InfluenceDate = Math.Round((Convert.ToDateTime(input.ActualSettlementDate) - input.FeedbackTime).Value.TotalDays, 2) + "天";
            }
            int upNum = _ProblemFeedbackService.Update(input.Id.Value, input);
            if (upNum > 0)
            {
                var esburl = _configuration["ESBUrl:key"] + "/sendMessage";
                //责任判定的状态编辑成功后发送消息给接收人进行处理
                if (input.DealWithDynamic == "ProblemFBStatus2")
                {
                    _ProblemFeedbackService.MessagePush(input.pt_ID, input.SolutionId, esburl);
                    var tel = _user.GetUserTelById(input.SolutionId);
                    //获取超时 时间 间隔
                    var content = _config.GetConfigCode("TimeGap");
                    string[] strArray = content.Value.Split(',');//字符串转数组
                    if (strArray.Any())
                    {
                        foreach (var item in strArray)
                        {
                            _send.Insert(new ProblemMsgSendDto
                            {
                                ProblemId = input.Id.Value,
                                Toa = blem.LastModifiedAt,
                                Content = string.Empty,
                                TxdTime = blem.LastModifiedAt.AddDays(int.Parse(item)),
                                Mobile = tel.Telnum,
                                ProjectCode = input.ProjectCode,
                                ProjectName = input.ProjectName,
                                IsToa = 0
                            });
                        }
                    }
                }
                else if (input.DealWithDynamic == "ProblemFBStatus3")//处理中的状态编辑成功后发送消息给质量人员进行验证
                {
                    _ProblemFeedbackService.MessagePushJY(input.pt_ID, esburl);
                    //如果处理人员规定时间类处理完，超时消息置为已发送状态
                    var SendList = _send.GetList().Where(o => o.ProblemId == input.Id && o.IsToa == 0);
                    if (SendList.Any())
                    {
                        foreach (var item in SendList)
                        {
                            _send.Update(item.Id, new ProblemMsgSendDto
                            {
                                Id = item.Id,
                                ProblemId = item.ProblemId,
                                Toa = item.LastModifiedAt,
                                Content = item.Content,
                                TxdTime = item.TxdTime,
                                Mobile = item.Mobile,
                                ProjectCode = item.ProjectCode,
                                ProjectName = item.ProjectName,
                                IsToa = 1
                            });

                        }
                    }
                }
            }
            //添加操作日志
            _log.AddOperationLog(true, "处理/验证问题反馈成功", JsonConvert.SerializeObject(input));
            return ResponseDto.Success("修改成功！");
        }

        /// <summary>
        /// 删除问题反馈
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete]
        public IResponseDto DoDelete([FromBody] DeleteRequesDto model)
        {
            var ids = model.GetIds();
            if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
            int count = _ProblemFeedbackService.Delete(ids);
            return ResponseDto.Success($"成功删除了{count}条数据。");
        }

        /// <summary>
        /// 获取项目信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetprojectList")]
        public IResponseDto GetprojectList()
        {
            var result = _projectBasicsService.GetList().Where(a => a.IsDeleted == false);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 获取问题类型
        /// </summary>
        /// <returns></returns>
        [HttpGet("getProblemType")]
        public IResponseDto getProblemType()
        {
            var result = _dictionaryService.GetSubItemList("ProblemType");
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 获取原因类型
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetReasonType")]
        public IResponseDto GetReasonTypeDict()
        {
            var result = _dictionaryService.GetSubItemList("ReasonType");
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 获取上传文件地址
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetInsertFilePath")]
        public string GetInsertFilePath()
        {
            var MyUrl = _configuration["FileUrl:MyUrl"];
            string url = MyUrl + "/ProblemFeedback/InsertFile";
            return url;
        }

        [HttpGet("GetInsertFilePath2")]
        public IActionResult GetInsertFilePath2()
        {
            var MyUrl = _configuration["FileUrl:MyUrl"];
            string url = MyUrl + "/ProblemFeedback/InsertFile";
            return new JsonResult(new { Imgurl=url });
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="UploadFile"></param>
        /// <returns></returns>
        [HttpPost("InsertFile")]
        public IActionResult InsertFile(IFormFileCollection UploadFile)
        {
            FileDto Uploadfile = new FileDto();
            Uploadfile.File = UploadFile[0];
            Uploadfile.RootPath = _evn.ContentRootPath;
            var result = _ProblemFeedbackdetailService.Upload(Uploadfile);
            return new JsonResult(new { id = result.Id, Path = result.Path });
        }

        /// <summary>
        /// 根据父id获取问题反馈文件
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetFilesByPid")]
        public IResponseDto GetFilesByPid(int Id)
        {
            var result = _ProblemFeedbackdetailService.GetFilesByPid(Id);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 获取图片下载路径
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetFilePreviewPath")]
        public string GetFilePreviewPath()
        {
            var MyUrl = _configuration["FileUrl:MyUrl"];
            string url = MyUrl + "/ProblemFeedback/GetPictureData";
            return url;
        }

        /// <summary>
        /// 获取图片二进制流
        /// </summary>
        /// <param name="imagepath"></param>
        /// <returns></returns>
        [HttpGet("GetPictureData")]
        public ActionResult GetUserPic(string fileUrl)
        {
            string path = fileUrl; //图片路径
            if (System.IO.File.Exists(path)) //判断文件是否存在
            {
                // 读文件成二进制流
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    long bufferLength = stream.Length;
                    byte[] bufferFile = new byte[bufferLength];
                    stream.Read(bufferFile, 0, bufferFile.Length);
                    string contentType = "image/jpeg";
                    stream.Close();
                    return new FileContentResult(bufferFile, contentType);
                }
            }
            throw new ArgumentException("需要预览文件不存在！");
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Get")]
        [AllowAnonymous]
        public ActionResult Get(int id)
        {
            var item = _ProblemFeedbackdetailService.GetFile(id);
            return new JsonResult(new { url= "data:image/png;base64," + Convert.ToBase64String(item) }) ;

        }
        /// <summary>
        /// 编辑问题反馈状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("UpdateProState")]
        public IResponseDto UpdateProState(int Id, string proState)
        {
            bool counts = _ProblemFeedbackService.UpdateProState(Id, proState);//修改
            if (!counts) return ResponseDto.Error(-1, "更新异常");
            return ResponseDto.Success("更新成功");
        }


        /// <summary>
        /// 问题反馈消息发送
        /// </summary>
        /// <returns></returns>
        [HttpPost("problemMagSend")]
        public IResponseDto problemMagSend(int proId, int userId)
        {
            string esburl = _configuration["ESBUrl:key"] + "/sendMessage";
            var result = _ProblemFeedbackService.MessagePush(proId, userId, esburl);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 获取问题处理动态
        /// </summary>
        /// <returns></returns>
        [HttpGet("DealWithdynamic")]
        public IResponseDto DealWithdynamic()
        {
            var list = _dictionaryService.GetSubItemList("ProblemFBStatus").OrderBy(o => o.DictionaryValue).ToList();
            return ResponseDto.Success(list);
        }

        /// <summary>
        /// 通过角色名查找下面的用户ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRoleNameByUserId")]
        public IResponseDto GetRoleNameByUserId(string RoleName)
        {
            var list = _ProblemFeedbackdetailService.GetRoleNameByUserId(RoleName);
            return ResponseDto.Success(list);
        }
    }
}
