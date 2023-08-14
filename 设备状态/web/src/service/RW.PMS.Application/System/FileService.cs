using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.File;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Exceptions;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using System.Linq;
using RW.PMS.Application.Contracts.DTO.Project;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Drawing;

namespace RW.PMS.Application.System
{
    public class FileService : CrudApplicationService<FileEntity, int>, IFileService
    {
        private readonly IConfiguration _configuration;
        private bool isFirst = true;

        public FileService(IDataValidatorProvider dataValidator, IRepository<FileEntity, int> repository,
            IMapper mapper, Lazy<ICurrentUser> currentUser, IConfiguration configuration) : base(dataValidator,
            repository, mapper, currentUser)
        {
            _configuration = configuration;
        }

        public FileOutputDto Upload(FileInputDto input)
        {
            if (input == null) throw new Exception("请上传你文件");
            var rootPath = input.RootPath;
            if (input.RootPath.IsNullOrWhiteSpace()) rootPath = _configuration["UploadPath"];
            //var currentFolder = Path.Combine("Upload", DateTime.Now.ToString("yyyy-MM"));
            var savePath = Path.Combine("/Upload", rootPath, DateTime.Now.ToString("yyyy-MM"));
            if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);
            var extension = Path.GetExtension(input.Filename);
            var fileName = $"{Guid.NewGuid().ToString().Replace("-", string.Empty)}{extension}";
            var filePath = Path.Combine(savePath, fileName);
            using var stream = File.Create(filePath);
            stream.Write(input.Data, 0, input.Data.Length);
            //input.File.CopyTo(stream);
            var entity = new FileEntity
            {
                ContentType = input.ContentType,
                FileName = input.Filename,
                FileSize = input.Data.Length,
                RootPath = rootPath,
                RelativePath = $"{savePath}\\{fileName}"
            };
            Repository.Insert(entity);
            return new FileOutputDto { Id = entity.Id, Name = entity.FileName, Path = "upload/" + entity.Id };
        }

        public void Remove(int id)
        {
            var entity = Get(id);
            if (entity != null)
            {
                var filePath = string.Concat(entity.RootPath, entity.RelativePath);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                Repository.DataFilter.Disable("IsDeleted");
                Repository.Delete(entity);
            }
        }

        public PagedResult<FileListDto> GetPagedList(FileQueryDto input)
        {
            var uid = CurrentUser.Value.Id;
            double maxSize = input.MaxSize * 1024 * 1024;
            var lst = Repository.Select.From<UserEntity>(
                    (f, u) => f.LeftJoin((f1) => f1.CreatedBy == u.Id)
                )
                .WhereIf(!string.IsNullOrEmpty(input.Type), x => x.t1.RootPath == "/" + input.Type)
                .WhereIf(input.IsMine, x => x.t1.CreatedBy == uid)
                .WhereIf(!string.IsNullOrEmpty(input.Name), x => x.t1.FileName.Contains(input.Name))
                .WhereIf(input.MaxSize > 0, x => x.t1.FileSize <= maxSize)
                .Count(out long total)
                .Page(input.PageNo, input.PageSize)
                .ToList(x => new { x.t1, x.t2 })
                .Select(x =>
                {
                    var entity = Mapper.Map<FileListDto>(x.t1);
                    if (x.t2 != null)
                        entity.Uploader = x.t2.RealName;
                    return entity;
                }).ToList();
            ;

            return new PagedResult<FileListDto>(lst, total);
        }

        public FileModelDto GetFile(int id)
        {
            var item = Get(id);
            if (item == null) throw new LogicException(ExceptionCode.Failed, $"指定的文件不存在[{id}]");
            var path = Path.Combine(item.RootPath, item.RelativePath);

            var dto = Mapper.Map<FileModelDto>(item);

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                byte[] bts = new byte[fs.Length];
                fs.Read(bts, 0, bts.Length);
                dto.Data = bts;
                return dto;
            }
        }

        public FileModelDto GetFile2(int id)
        {
            var item = Get(id);
            if (item == null) throw new LogicException(ExceptionCode.Failed, $"指定的文件不存在[{id}]");
            var path = Path.Combine(item.RootPath, item.RelativePath);

            var dto = Mapper.Map<FileModelDto>(item);
            string a = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string[] baseUrl = a.Split("\\bin");
            using (FileStream fs = new FileStream(baseUrl[0] + path, FileMode.Open))
            {
                byte[] bts = new byte[fs.Length];
                fs.Read(bts, 0, bts.Length);
                dto.Data = bts;
                return dto;
            }
        }

        public FileReDto Upload2(FileDto input)
        {
            var rootPath = input.RootPath;
            if (input.RootPath.IsNullOrWhiteSpace()) rootPath = _configuration["UploadPath"];
            var currentFolder = Path.Combine("Upload", DateTime.Now.ToString("yyyyMMdd"));
            var savePath = Path.Combine(rootPath, currentFolder);
            if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);
            var extension = Path.GetExtension(input.File.FileName);
            var fileName = $"{Guid.NewGuid().ToString().Replace("-", string.Empty)}{extension}";
            var filePath = Path.Combine(savePath, fileName);
            using var stream = File.Create(filePath);
            input.File.CopyTo(stream);
            var entity = new FileEntity
            {
                ContentType = input.File.ContentType,
                FileName = fileName,
                FileSize = input.File.Length,
                RootPath = rootPath,
                RelativePath = $"\\{currentFolder}\\{fileName}"
            };
            Repository.Insert(entity);
            return new FileReDto { Id = entity.Id, Path = entity.RelativePath };
        }


        #region 文档管理

        public FileModelDto GetFileByPath(string fullPath)
        {
            //获取文件夹路径
            var FolderName = fullPath[..(fullPath.Length - 1 - fullPath.Split("\\")[^1].Length)];
            //获取文件名称
            var fileName = fullPath.Split("\\")[^1];
            var files = GetFileInfo(FolderName);
            if (files.Any())
            {
                FileInfo downloadFile = files.Where(t => t.Name == fileName).First();
                FileModelDto dto = new FileModelDto { ContentType = "application/pdf", FileSize = (int)downloadFile.Length, Filename = fileName };
                using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                {
                    byte[] bts = new byte[fs.Length];
                    fs.Read(bts, 0, bts.Length);
                    dto.Data = bts;
                    return dto;
                }
            }
            return new FileModelDto();
        }

        public List<FoldersInfoDto> GetAllFolders(string path)
        {
            string DirPath = @path.ToString();   //指定一个路径
            string[] folders = Directory.GetDirectories(DirPath);
            var hasStr = folders.Where(t => t.Contains("项目资料")).ToList();
            if (hasStr.Any() && isFirst)
            {
                isFirst = false;
                folders = hasStr.ToArray();
            }
            var FoldersInfo = new List<FoldersInfoDto>();
            for (int i = 0; i < folders.Length; i++)
            {
                FoldersInfo.Add(new FoldersInfoDto { label = folders[i].Split("\\")[^1], FullPath = folders[i] });
            }
            return FoldersInfo;
        }

        public FoldersInfoDto GetFileByFolderName(string ProFolder, string FileFolder)
        {
            string DirPath = _configuration["serverNameSC:url"];
            string[] folders = Directory.GetDirectories(@DirPath);
            var AllFolders = folders.Where(t => t.Contains("项目资料")).ToList();
            foreach (var Fitems in AllFolders)
            {
                var proresult = GetFolderPathByFolderName(Fitems, ProFolder);
                if (proresult.FullPath != null)
                {
                    string[] ProFolders = Directory.GetDirectories(proresult.FullPath);
                    foreach (var proItems in ProFolders)
                    {
                        var FileResult = GetFolderPathByFolderName(proItems, FileFolder);
                        //如该项目文件夹下无需要查找的存放pdf文件的文件夹，但该项目下子项目文件夹有，则需要返回一个空的FoldersInfoDto对象
                        if (FileResult.FullPath != null)
                        {
                            var resultArr = FileResult.FullPath.Split("\\");
                            List<string> list = new(resultArr);
                            list.RemoveRange(0, 6);
                            resultArr = list.ToArray();
                            var ProFolderArr = resultArr.Where(t => t.Split("_")[0].ToString().Length > 6).ToArray();
                            if (!ProFolderArr[^1].Equals(ProFolder))
                                return new FoldersInfoDto();
                            return FileResult;
                        }
                    }
                }
            }
            return new FoldersInfoDto();
        }

        public FoldersInfoDto GetFileByProFolderName(string serverUrl,string ProFolder,bool isAll)
        {
            string SCDirPath = @serverUrl.ToString();
            //获取服务器路径下所有文件夹名称
            string[] folders = Directory.GetDirectories(SCDirPath);
            //获取包含“项目资料”文本的文件夹
            var AllFolders = isAll ? folders.ToList() : folders.Where(t => t.Contains("项目资料")).ToList();
            //循环所有文件夹名称获取其文件夹路径
            foreach (var Fitems in AllFolders)
            {
                var proresult = GetFolderPathByFolderName(Fitems, ProFolder);
                if (proresult.FullPath != null)
                {
                    return proresult;
                }
            }
            return new FoldersInfoDto();
        }

        public FoldersInfoDto GetFolderPathByFolderName(string QueryPath, string FolderName)
        {
            string[] folders = Directory.GetDirectories(QueryPath);
            if (folders.Where(t => t.Contains(FolderName)).ToList().Count > 0)
            {
                return new FoldersInfoDto { label = "", FullPath = folders.Where(t => t.Contains(FolderName)).First() };
            }
            else
            {
                foreach (var Fitems in folders)
                {
                    if (Directory.GetDirectories(Fitems).ToList().Count > 0)
                    {
                        var result = GetFolderPathByFolderName(Fitems, FolderName);
                        if (result.FullPath != null)
                            return result;
                    }
                }
            }
            return new FoldersInfoDto();
        }

        public FileInfo[] GetFileInfo(string FolderName)
        {
            string dir = @FolderName.ToString();
            DirectoryInfo directoryInfo = new DirectoryInfo(dir);
            try
            {
                FileInfo[] fileinfos = directoryInfo.GetFiles();
                return fileinfos;
            }
            catch (Exception)
            {
                return new FileInfo[0];
            }
        }


        public FileModelDto SetWatermark(string filePath)
        {
            string a = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string[] baseUrl = a.Split("\\bin");
            string savePath = baseUrl[0] + _configuration["SaveCopyFile:url"];
            string tempPath = savePath + "\\" + Path.GetFileNameWithoutExtension(filePath) + "_temp.pdf";
            try
            {
                string text = DateTime.Now.ToString();
                PdfReader pdfReader = new(filePath);
                int total = pdfReader.NumberOfPages + 1;

                if (!Directory.Exists(savePath))
                    Directory.CreateDirectory(savePath);
                else
                {
                    //删除文件夹中原有文件
                    DirectoryInfo dir = new(savePath);
                    FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                    foreach (FileSystemInfo i in fileinfo)
                    {
                        File.Delete(i.FullName);      //删除指定文件
                    }
                }
                using (var fs = new FileStream(tempPath, FileMode.Create))
                {
                    PdfStamper pdfStamper = new(pdfReader, fs);
                    var psize = pdfReader.GetPageSize(1);
                    float width = psize.Width;
                    float height = psize.Height;
                    PdfContentByte content;
                    BaseFont font = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1257, BaseFont.EMBEDDED);
                    PdfGState gs = new();
                    int waterMarkNameLenth = text.Length;

                    for (int i = 1; i < total; i++)
                    {
                        var fontLength = 12;
                        content = pdfStamper.GetOverContent(i);//在内容上方加水印
                        // content = pdfStamper.GetUnderContent(i);//在内容下方加水印
                        // 透明度
                        gs.FillOpacity = 0.6f;

                        content.SetGState(gs);
                        //content.SetGrayFill(0.3f);
                        //开始写入文本
                        content.BeginText();
                        content.SetColorFill(BaseColor.GRAY);
                        content.SetFontAndSize(font, fontLength);
                        //content.SetTextMatrix(120, 120);
                        var Margin = 90;//调整水印的间距
                        var TextSize = new Size(text.Length * fontLength + Margin, text.Length * fontLength + Margin);
                        var RenderVeiwSize = new Size
                        {
                            Width = (int)(width / TextSize.Width) + (width % TextSize.Width != 0 ? 1 : 0),
                            Height = (int)(height / TextSize.Height) + (height % TextSize.Height != 0 ? 1 : 0)
                        };

                        for (int h = 0; h < RenderVeiwSize.Height; h++)
                        {
                            for (int w = 0; w < RenderVeiwSize.Width; w++)
                            {
                                content.ShowTextAligned(Element.ALIGN_CENTER, _configuration["SaveCopyFile:firstTxt"], (float)(TextSize.Width * w + TextSize.Width / 2.1), (float)(height - TextSize.Height * h - TextSize.Height / 2.1), 45);
                                content.ShowTextAligned(Element.ALIGN_CENTER, text, TextSize.Width * w + TextSize.Width / 2, height - TextSize.Height * h - TextSize.Height / 2, 45);
                            }
                        }

                        content.EndText();
                    }
                    if (pdfStamper != null)
                        pdfStamper.Close();

                    if (pdfReader != null)
                        pdfReader.Close();
                    
                    File.Copy(tempPath, savePath+ "\\"+Path.GetFileName(filePath), true);
                }
                File.Delete(tempPath);
                return GetFileByPath(savePath + "\\" + Path.GetFileName(filePath));
            }
            catch (Exception)
            {
                return new FileModelDto();
            }
        }
        #endregion
    }
}