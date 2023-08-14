using AutoMapper;
using Microsoft.Extensions.Configuration;
using RW.PMS.Application.Contracts.DTO.File;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Exceptions;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Project;
using RW.PMS.Domain.Repositories;
using System;
using System.IO;

namespace RW.PMS.Application.Project
{
    public class ProjectBasicsFilesService : CrudApplicationService<pro_basics_filesEntity, int>, IProjectBasicsFilesService
    {
        private readonly IConfiguration _configuration;

        public ProjectBasicsFilesService(IDataValidatorProvider dataValidator, IRepository<pro_basics_filesEntity, int> repository,
            IConfiguration configuration,
            IMapper mapper, Lazy<ICurrentUser> currentUser) : base(dataValidator, repository, mapper, currentUser)
        {
            _configuration = configuration;
        }

        public FileReDto Upload(MultiFileDto input)
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
            var entity = new pro_basics_filesEntity
            {
                ContentType = input.File.ContentType,
                FileName = fileName,
                FileSize = input.File.Length,
                RootPath = rootPath,
                PID = input.Pid,
                RelativePath = $"\\{currentFolder}\\{fileName}"
            };
            Repository.Insert(entity);
            return new FileReDto { Id = entity.Id, Path = entity.RelativePath };
        }


        public FileModelDto GetFileShow(int Pid)
        {
            var item = Repository.Select.Where(t=>t.PID==Pid).OrderByDescending(t=>t.Id).First();
            if (item == null) throw new LogicException(ExceptionCode.Failed, $"指定的文件不存在");
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

    }
}
