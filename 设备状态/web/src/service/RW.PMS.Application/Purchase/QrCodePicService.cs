using AutoMapper;
using Microsoft.Extensions.Configuration;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Purchase;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Purchase;
using RW.PMS.Domain.Repositories;
using System;
using System.IO;
using System.Linq;

namespace RW.PMS.Application.Purchase
{
    public class QrCodePicService : CrudApplicationService<Qr_codePicEntity, int>, IQrCodePicService
    {
        private readonly IConfiguration _configuration;
        public QrCodePicService(IDataValidatorProvider dataValidator, IRepository<Qr_codePicEntity, int> repository, IConfiguration configuration,
            IMapper mapper, Lazy<ICurrentUser> currentUser) : base(dataValidator, repository, mapper, currentUser)
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
            var extension = Path.GetExtension(input.File.FileName);
            var fileName = $"{Guid.NewGuid().ToString().Replace("-", string.Empty)}{extension}";
            var filePath = Path.Combine(savePath, fileName);
            using var stream = File.Create(filePath);
            input.File.CopyTo(stream);
            var entity = new Qr_codePicEntity
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


        public void UpdateImgPid(string imgFilesId, int? pId)
        {
            string[]? sImgsArray = imgFilesId.NotNullOrWhiteSpace() ? imgFilesId.Split(",") : null;
            if (sImgsArray != null)
            {
                foreach (var item in sImgsArray)
                {
                    if (item.NotNullOrWhiteSpace())
                    {
                        Qr_codePicEntity detail = Repository.Select.Where(a => a.IsDeleted == false).ToList().Where(a => a.Id == Convert.ToInt32(item)).FirstOrDefault();
                        if (detail != null)
                        {
                            detail.PID = pId ?? 0;
                            Repository.Orm.Update<Qr_codePicEntity>(item)
                                .Set(a => a.PID, detail.PID)
                                .ExecuteAffrows();
                        }
                    }
                }
            }
        }
    }
}
