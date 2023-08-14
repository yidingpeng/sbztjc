using AutoMapper;
using Microsoft.Extensions.Configuration;
using RW.PMS.Application.Contracts.Basics;
using RW.PMS.Application.Contracts.DTO.Basics;
using RW.PMS.Application.Contracts.DTO.Problem;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Exceptions;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Basics;
using RW.PMS.Domain.Repositories;
using RW.PMS.Domain.Repositories.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RW.PMS.Application.Basics
{
    public class MaterialDetailService : CrudApplicationService<MaterialDetailEntity, int>, IMaterialDetailService
    {
        private readonly IConfiguration _configuration;
        private readonly IRoleService _roleService;
        private readonly IUserRoleRepository _userRoleRepository;
        public MaterialDetailService(IDataValidatorProvider dataValidator,
            IRepository<MaterialDetailEntity, int> repository, IMapper mapper,
            IConfiguration configuration, IRoleService roleService, IUserRoleRepository userRoleRepository,
            Lazy<ICurrentUser> currentUser) :
            base(dataValidator, repository, mapper, currentUser)
        {
            _configuration = configuration;
            _roleService = roleService;
            _userRoleRepository = userRoleRepository;
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
            var entity = new MaterialDetailEntity
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

        public List<ProblemFilesDto> GetFilesByPid(int pid)
        {
            List<ProblemFilesDto> ProFilesList = new List<ProblemFilesDto>();
            string a = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string[] baseUrl = a.Split("\\bin");
            var list = Repository.Select.Where(a => a.PID == pid && a.IsDeleted == false).ToList();
            foreach (var item in list)
            {
                ProblemFilesDto file = new ProblemFilesDto();
                file.FileName = item.FileName;
                //file.RootPath = baseUrl[0] + item.RelativePath;
                file.RootPath = "data:image/png;base64," + Convert.ToBase64String(GetFile(item.Id));
                file.ContentType = item.ContentType;
                ProFilesList.Add(file);
            }
            return ProFilesList;
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
                        MaterialDetailEntity detail = Repository.Select.Where(a => a.IsDeleted == false).ToList().Where(a => a.Id == Convert.ToInt32(item)).FirstOrDefault();
                        if (detail != null)
                        {
                            detail.PID = pId ?? 0;
                            if (detail.PID != 0)
                            {
                                MaterialDetailDto info = new MaterialDetailDto();
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
            }
        }

        public bool UpdateImgPidtoZero(int pid)
        {
            var detailList = Repository.Select.Where(a => a.IsDeleted == false && a.PID == Convert.ToInt32(pid)).ToList();
            foreach (var item in detailList)
            {
                MaterialDetailDto info = new MaterialDetailDto();
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

        public int[] GetImageByPtid(int ptid)
        {
            var list = Repository.Select.Where(a => a.PID == ptid).ToList(a => a.Id);
            if (list.Any())
            {
                var val = new int[0];
                val = list.ToArray();
                return val;
            }
            return Array.Empty<int>();
        }

        public byte[] GetFile(int id)
        {
            var item = Get(id);
            if (item == null) throw new LogicException(ExceptionCode.Failed, $"指定的文件不存在[{id}]");
            var path = ("\\" + item.RelativePath).Split("\\\\");
            var FullPath = Path.Combine(item.RootPath, path[^1]);
            using (FileStream fs = new FileStream(FullPath, FileMode.Open))
            {
                byte[] bts = new byte[fs.Length];
                fs.Read(bts, 0, bts.Length);
                return bts;
            }
        }

        /// <summary>
        /// 通过角色名查找下面的用户ID
        /// </summary>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        public int[] GetRoleNameByUserId(string RoleName)
        {
            //根据角色名称获取角色id
            var roleid = _roleService.GetList().Where(t => t.RoleName == RoleName).FirstOrDefault();
            List<int> list = new List<int>();
            if (roleid != null)
            {
                //获取拥有该角色的所有用户id
                var useridArr = _userRoleRepository.Where(t => t.RoleId == roleid.Id).ToList();
                foreach (var item in useridArr)
                {
                    list.Add(item.UserId);
                }
            }
            return list.ToArray();
        }
    }
}
