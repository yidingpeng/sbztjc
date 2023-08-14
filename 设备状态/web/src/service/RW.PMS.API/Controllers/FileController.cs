using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.Application.Contracts.System;
using RW.PMS.Domain.Entities.DocManagement;
using RW.PMS.Domain.Repositories.DocManagement;
using System.Diagnostics;

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
        public readonly ILogService _log;

        public FileController(IFileService fileService, IWebHostEnvironment evn, IConfiguration configuration, IDocManagementRepository docManagementRepository, ILogService log,
            IProjectBasicsService projectBasicsService, IDictionaryService dictionaryService)
        {
            _evn = evn;
            _fileService = fileService;
            _dictionaryService = dictionaryService;
            _configuration = configuration;
            _projectBasicsService = projectBasicsService;
            _docManagementRepository = docManagementRepository;
            _log = log;
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
        public string GetFileUrl()
        {
            var MyUrl = _configuration["FileUrl:MyUrl"];
            return MyUrl;
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

        #region 文档管理
        /// <summary>
        /// 获取指定路径下所有文件夹
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllFolders")]
        public ActionResult GetAllFolders(string? path)
        {
            var result = _fileService.GetAllFolders(path);
            return new JsonResult(new { fileFolders = result });
        }

        /// <summary>
        /// 获取指定目录下所有文件的完整路径
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetFileInfo")]
        public IActionResult GetFileInfo(string FolderName,bool IsPdf)
        {
            var result = _fileService.GetFileInfo(FolderName);
            var filesNameL = new List<string>();
            var filesFullNameL = new List<string>();
            var filesSizeL = new List<long>();
            if (result.Any())
            {
                foreach (var item in result)
                {
                    if (IsPdf)
                    {
                        if (!item.Name.Split(".")[^1] .Equals("pdf") && !item.Name.Split(".")[^1].Equals("PDF"))
                            continue;
                    }
                    filesNameL.Add(item.Name);
                    filesFullNameL.Add(item.FullName);
                    filesSizeL.Add(item.Length);
                }
            }
            var filesName = filesNameL.ToArray();
            var filesFullName = filesFullNameL.ToArray();
            var filesSize = filesSizeL.ToArray();
            return new JsonResult(new { filesName = filesName, filesFullName = filesFullName, filesSize = filesSize });
        }

        /// <summary>
        /// 根据文件完整路径获取文件
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        [HttpGet("GetFileByPath")]
        public ActionResult GetFileByPath(string fullPath)
        {
            var item = _fileService.GetFileByPath(fullPath);
            MemoryStream ms = new MemoryStream(item.Data);
            return File(ms, item.ContentType, item.Filename, enableRangeProcessing: true);
        }
        /// <summary>
        /// 根据项目文件夹获取pdf存放文件夹
        /// </summary>
        /// <param name="ProFolder"></param>
        /// <param name="FileFolder"></param>
        /// <returns></returns>
        [HttpGet("GetFileByFolderName")]
        public IResponseDto GetFileByFolderName(string ProFolder, string FileFolder)
        {
            var result = _fileService.GetFileByFolderName( ProFolder, FileFolder);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 获取需查看的文档文件夹
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetFolders")]
        public IResponseDto GetFolders()
        {
            var list = _dictionaryService.GetSubItemList("SCFolderName");
            return ResponseDto.Success(list);
        }

        /// <summary>
        /// 获取管理端文档文件夹
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetGLFolders")]
        public IResponseDto GetGLFolders()
        {
            var list = _dictionaryService.GetSubItemList("GLFolderName");
            return ResponseDto.Success(list);
        }

        static bool isExecuted = false;
        static object locker = new object();
        /// <summary>
        /// 更新文档管理表格数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("UpdateDocManage")]
        public IResponseDto UpdateDocManage()
        {
            lock (locker)
            {
                if (!isExecuted)
                {
                    isExecuted = true;                   
                    Task.Run(() =>
                    {
                        //TODO:写日志，开始
                        //_log.AddOperationLog(true, "更新文档管理信息开始", "开始更新");
                        Stopwatch watch = new Stopwatch();
                        watch.Start();
                        //删除文档管理表格中所有数据
                        _docManagementRepository.Delete(i => i.ProjectCN != "");
                        //获取所有项目信息
                        string key = "";
                        var prolist = _projectBasicsService.GetAllList(key);
                        string[] foldersGL = Directory.GetDirectories(_configuration["serverNameGL:url"], "", SearchOption.AllDirectories);
                        string[] foldersSC = Directory.GetDirectories(_configuration["serverNameSC:url"], "", SearchOption.AllDirectories);
                        List<DocManagementEntity> docManaList = new();
                        foreach (var proItem in prolist)
                        {
                            var proCNItem = proItem.ProjectCode + '_' + proItem.ProjectName;
                            ////获取生产员项目文件夹的路径
                            //var SCFolderPath = _fileService.GetFileByProFolderName(_configuration["serverNameSC:url"], proCNItem, false);
                            ////获取管理员项目文件夹的路径
                            //var GLFolderPath = _fileService.GetFileByProFolderName(_configuration["serverNameGL:url"], proCNItem, true);
                            ////保存数据到list中
                            //DocManagementEntity docManaItem = new() { ProjectCN = proCNItem, FolderAddressSC = SCFolderPath.FullPath ?? "", FolderAddressGL = GLFolderPath.FullPath ?? "" };
                            //docManaList.Add(docManaItem);


                            //循环遍历管理端和生产端所获取的文件路径，查找项目文件夹路径并保存
                            var SCFolderPath = "";
                            var GLFolderPath = "";
                            foreach (var item in foldersSC)
                            {
                                if (item.Contains(proCNItem))
                                {
                                    SCFolderPath = item;
                                    break;
                                }
                            }
                            foreach (var item in foldersGL)
                            {
                                if (item.Contains(proCNItem))
                                {
                                    GLFolderPath = item;
                                    break;
                                }
                            }
                            //保存数据到list中
                            DocManagementEntity docManaItem = new() { ProjectCN = proCNItem, FolderAddressSC = SCFolderPath ?? "", FolderAddressGL = GLFolderPath ?? "" };
                            docManaList.Add(docManaItem);
                        }
                        //保存数据到数据库中
                        _docManagementRepository.Insert(docManaList);
                        watch.Stop();
                        //用时：watch.Elapse
                        Console.WriteLine($"用时：{watch.Elapsed:hh\\:mm\\:ss}");
                        Console.WriteLine($"添加数量：{docManaList.Count}");
                        isExecuted = false;
                        //TODO：写日志，结束，并记录用时;
                        //_log.AddOperationLog(true, "更新文档管理信息完毕", "用时：" + watch.Elapsed + "；项目数：" + docManaList.Count);
                    });
                    return ResponseDto.Success("请求成功");
                }
                else
                {
                    return ResponseDto.Error(200, "命令正在执行，请勿重复请求。");
                }
            }

        }

        /// <summary>
        /// 获取文档分页数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="PageNo"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        [HttpGet("GetDocPagedList")]
        public IResponseDto GetDocPagedList(string key)
        {
            var result = _docManagementRepository.GetPagedList(key);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 给文件添加水印并返回该文件
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        [HttpGet("SetWatermark")]
        public ActionResult SetWatermark(string fullPath)
        {
            var item = _fileService.SetWatermark(fullPath);
            MemoryStream ms = new MemoryStream(item.Data);
            return File(ms, item.ContentType, item.Filename, enableRangeProcessing: true);
        }
        #endregion
    }
}
