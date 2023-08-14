using AutoMapper;
using Microsoft.Extensions.Configuration;
using RW.PMS.Application.Contracts.DTO.File;
using RW.PMS.Application.Contracts.DTO.Problem;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Probelm;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Exceptions;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Problem;
using RW.PMS.Domain.Entities.Project;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Problem
{
    public class ContractInfoFilesService : CrudApplicationService<ContractInfoFilesEntity, int>, IContractInfoFilesService
    {
        private readonly IConfiguration _configuration;
        public ContractInfoFilesService(IDataValidatorProvider dataValidator,
            IRepository<ContractInfoFilesEntity, int> repository, IMapper mapper,
            IConfiguration configuration,
            Lazy<ICurrentUser> currentUser) : 
            base(dataValidator, repository, mapper, currentUser)
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
            var entity = new ContractInfoFilesEntity
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

        public bool updateFilesByPid(int pid, int filesType)
        {
            var list = filesType == 1 ? Repository.Select.Where(t => t.PID == pid && t.ContentType== "application/pdf").ToList() :
                Repository.Select.Where(t => t.PID == pid && (t.ContentType == "application/msword"||t.ContentType== "application/vnd.openxmlformats-officedocument.wordprocessingml.document")).ToList();
            foreach (var item in list)
            {
                ContractInfoFilesListDto info = new ContractInfoFilesListDto();
                info.PID = 0;
                info.ContentType = item.ContentType;
                info.FileName = item.FileName;
                info.FileSize = item.FileSize;
                info.RootPath = item.RootPath;
                info.RelativePath = item.RelativePath;
                Update(item.Id, info);
                var filePath = string.Concat(item.RootPath, item.RelativePath);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            return true;
        }


        public void UpdateImgPid(string ConFilesId, int? pId)
        {
            string[]? ConArray = ConFilesId.NotNullOrWhiteSpace() ? ConFilesId.Split(",") : null;
            if (ConArray != null)
            {
                foreach (var item in ConArray)
                {
                    if (item.NotNullOrEmpty())
                    {
                        ContractInfoFilesEntity detail = Repository.Select.Where(a => a.IsDeleted == false).ToList().Where(a => a.Id == Convert.ToInt32(item)).First();
                        detail.PID = pId ?? 0;
                        ContractInfoFilesListDto info = new ContractInfoFilesListDto();
                        info.PID = detail.PID;
                        info.ContentType = detail.ContentType;
                        info.FileName = detail.FileName;
                        info.FileSize = detail.FileSize;
                        info.RootPath = detail.RootPath;
                        info.RelativePath = detail.RelativePath;
                        Update(detail.Id, info);
                    }
                }
            }
        }

        public FileModelDto GetFileShow(int id)
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
    }
}
