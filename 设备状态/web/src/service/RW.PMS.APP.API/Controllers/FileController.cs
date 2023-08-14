using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.Application.Contracts.System;
using RW.PMS.Domain.Entities.DocManagement;
using RW.PMS.Domain.Repositories.DocManagement;

namespace RW.PMS.API.Controllers
{
    public class FileController : RWBaseController
    {
        IWebHostEnvironment _evn;
        IConfiguration _configuration;
        private readonly IFileService _fileService;
        IDictionaryService _dictionaryService; 
        IProjectBasicsService _projectBasicsService;
        private readonly IDocManagementRepository _docManagementRepository;

        public FileController(IFileService fileService, IWebHostEnvironment evn, IConfiguration configuration, IDocManagementRepository docManagementRepository,
            IProjectBasicsService projectBasicsService, IDictionaryService dictionaryService)
        {
            _evn = evn;
            _fileService = fileService;
            _dictionaryService = dictionaryService;
            _configuration = configuration;
            _projectBasicsService = projectBasicsService;
            _docManagementRepository = docManagementRepository;
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
            var result = _fileService.Upload2(Uploadfile);
            return new JsonResult(new { id = result.Id, Path = result.Path });
        }

        /// <summary>
        /// 预览文件
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpGet("UpdownFile")]
        public IActionResult UpdownFile(string fileUrl, string fileName)
        {
            if (fileName != null)
            {
                return new FileStreamResult(new FileStream(fileUrl, FileMode.Open), "application/octet-stream") { FileDownloadName = fileName };
            }
            return null;
        }

        /// <summary>
        /// 获取上传、预览地址
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetFileUrl")]
        public IActionResult GetFileUrl()
        {
            var MyUrl = _configuration["FileUrl:MyUrl"];
            return new JsonResult(new { fileurl = MyUrl });
        }

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="imagepath"></param>
        /// <returns></returns>
        [HttpGet("GetFilePreview")]
        public ActionResult GetFilePreview(string fileUrl, int id)
        {
            string path = fileUrl; //路径
            if (System.IO.File.Exists(path)) //判断文件是否存在
            {
                // 读文件成二进制流
                //using (FileStream stream = new FileStream(path, FileMode.Open,FileAccess.Read))
                //{
                //    long bufferLength = stream.Length;
                //    byte[] bufferFile = new byte[bufferLength];
                //    stream.Read(bufferFile, 0, bufferFile.Length);
                //    string contentType = "image/png";
                //    if (fileUrl.Split(".")[fileUrl.Split(".").Length - 1] == "pdf")
                //    {
                //        contentType = "application/pdf";
                //    }
                //    stream.Close();
                //    return new FileContentResult(bufferFile, contentType);
                //}
                var item = _fileService.GetFile2(id);
                MemoryStream ms = new MemoryStream(item.Data);
                return File(ms, item.ContentType, item.Filename, enableRangeProcessing: true);
            }
            throw new ArgumentException("需要预览文件不存在！");
        }
    }
}
