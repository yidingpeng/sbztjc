using System;
using System.IO;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using RW.PMS.Application.Contracts.DTO.System;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;

namespace RW.PMS.Application.System
{
    public class AttachmentUploadService : CrudApplicationService<AttachmentUploadEntity, int>, IAttachmentUploadService
    {
        private readonly IConfiguration _configuration;

        public AttachmentUploadService(IDataValidatorProvider dataValidator, IRepository<AttachmentUploadEntity, int> repository,
            IMapper mapper, Lazy<ICurrentUser> currentUser, IConfiguration configuration) : base(dataValidator,
            repository, mapper, currentUser)
        {
            _configuration = configuration;
        }

        public FileReDto Upload(FileDto input)
        {
            var rootPath = input.RootPath;
            if (input.RootPath.IsNullOrWhiteSpace()) rootPath = _configuration["UploadPath"];
            var currentFolder = Path.Combine("Upload", DateTime.Now.ToString("yyyyMMdd"));
            var savePath = Path.Combine(rootPath, currentFolder);
            if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);
            var filePath = Path.Combine(savePath, input.File.FileName);
            using var stream = File.Create(filePath);
            input.File.CopyTo(stream);
            Random rd = new Random();

            //获取三位随机数
            int iRandom = rd.Next(100, 999);
            var entity = new AttachmentUploadEntity
            {
                ContentType = input.File.ContentType,
                FileID= DateTime.Now.ToString("yyyyMMddHHmmss")+ iRandom,
                FileName = input.File.FileName,
                FileSize = input.File.Length,
                RootPath = rootPath,
                ExpireDate = DateTime.Now,
                IsView = 0,
            RelativePath = $"\\{currentFolder}\\{input.File.FileName}"
            };
            Repository.Insert(entity);
            return new FileReDto { FileId = entity.FileID, Path = entity.RelativePath};
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
    }
}