using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Basics;
using RW.PMS.Application.Contracts.Basics;
using RW.PMS.Application.Contracts.System;
using RW.PMS.Application.System;
using RW.PMS.Application.Contracts.DTO.Configuration;
using RW.PMS.Application.Basics;
using RW.PMS.Application.Contracts.DTO.Project;
using System.ComponentModel.Design;
using IDictionaryService = RW.PMS.Application.Contracts.System.IDictionaryService;
using Microsoft.AspNetCore.Authorization;

namespace RW.PMS.API.Controllers
{
    public class ToolController : RWBaseController
    {
        private readonly IToolService _toolService;
        private readonly IDictionaryService _dictionaryService;
        private readonly IToolDetailService _toolDetailService;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _evn;
        public ToolController(IToolService roleService, IDictionaryService dictionaryService,ToolDetailService toolDetailService, IConfiguration configuration, IWebHostEnvironment evn)
        {
            _toolService = roleService;
            _dictionaryService = dictionaryService;
            _toolDetailService = toolDetailService;
            _configuration = configuration;
            _evn = evn;
        }

        [HttpGet]
        public IResponseDto GetList([FromQuery] ToolSearchDto input)
        {
            var result = _toolService.GetPagedList(input);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 数据是否重复
        /// </summary>
        [HttpGet("GetRepeat")]
        public IResponseDto GetRepeat([FromQuery] ToolSearchDto input)
        {
            bool model = _toolService.Repeatjudgment(input);

            return ResponseDto.Success(model);
        }

        /// <summary>
        /// 根据id获取工具图片信息id
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        [HttpGet("GetImageByPtid")]
        public IActionResult GetImageByPtid(int pid)
        {
            var result = _toolDetailService.GetImageByPtid(pid);
            return new JsonResult(new { ids = result });
        }

        /// <summary>
        /// 将上传的图片修改对应父id
        /// </summary>
        /// <param name="imgFilesId"></param>
        /// <param name="pId"></param>
        [HttpGet("UpdateImgPid")]
        public void UpdateImgPid(string imgFilesId, int? pId)
        {
            _toolDetailService.UpdateImgPid(imgFilesId, pId);
        }

        /// <summary>
        /// 把该所有父id修改成0
        /// </summary>
        /// <param name="Pid"></param>
        /// <returns></returns>
        [HttpGet("UpdateImgPidtoZero")]
        public IResponseDto UpdateImgPidtoZero(int Pid)
        {
            var result = _toolDetailService.UpdateImgPidtoZero(Pid);
            return result ? ResponseDto.Success("删除成功") : ResponseDto.Error(0, "删除失败");
        }

        /// <summary>
        /// 获取上传文件地址
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetInsertFilePath")]
        public string GetInsertFilePath()
        {
            var MyUrl = _configuration["FileUrl:MyUrl"];
            string url = MyUrl + "/Tool/InsertFile";
            return url;
        }

        [HttpGet("GetInsertFilePath2")]
        public IActionResult GetInsertFilePath2()
        {
            var MyUrl = _configuration["FileUrl:MyUrl"];
            string url = MyUrl + "/Tool/InsertFile";
            return new JsonResult(new { Imgurl = url }); ;
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
            var result = _toolDetailService.Upload(Uploadfile);
            return new JsonResult(new { id = result.Id, Path = result.Path });
        }

        /// <summary>
        /// 根据父id获取工具图片文件
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetFilesByPid")]
        public IResponseDto GetFilesByPid(int Id)
        {
            var result = _toolDetailService.GetFilesByPid(Id);
         
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
            string url = MyUrl + "/Tool/GetPictureData";
            return url;
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
            var item = _toolDetailService.GetFile(id);
            return new JsonResult(new { url = "data:image/png;base64," + Convert.ToBase64String(item) });

        }

        /// <summary>
        /// 获取字典工具类别
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("ToolClass")]
        public IResponseDto GetToolClassList()
        {
            var result = _dictionaryService.GetSubItemList("ToolClass");
            List<BaseCommonOutput> list = new List<BaseCommonOutput>();
            for (int i = 0; i < result.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    label = result[i].DictionaryText,
                    value = result[i].Id
                };
                list.Add(baseCommonOutput);
            }
            return ResponseDto.Success(list);
        }
         
        /// <summary>
        /// 获取字典工具类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("ToolType")]
        public IResponseDto GetToolTypeList()
        {
            var result = _dictionaryService.GetSubItemList("ToolType");
            List<BaseCommonOutput> list = new List<BaseCommonOutput>();
            for (int i = 0; i < result.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    label = result[i].DictionaryText,
                    value = result[i].Id
                };
                list.Add(baseCommonOutput);
            }
            return ResponseDto.Success(list);
        }


        [HttpPost]
        public IResponseDto DoAdd([FromBody] ToolDto input)
        {
            var result = _toolService.Insert(input);
        
            if (result.Id > 0)
            {
                UpdateImgPid(input.imgFilesId, result.Id);
            }
            return ResponseDto.Success("添加成功！");
        }

        [HttpPut]
        public IResponseDto DoEdit([FromBody] ToolDto input)
        {
            if (input.Id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            UpdateImgPid(input.imgFilesId, input.Id);
            _toolService.Update(input.Id, input);
            return ResponseDto.Success("修改成功！");
        }

        [HttpDelete]
        public IResponseDto DoDelete([FromBody] DeleteRequesDto model)
        {
            var ids = model.GetIds();
            if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
            int count = _toolService.Delete(ids);
            return ResponseDto.Success($"成功删除了{count}条数据。");
        }
    }
}
