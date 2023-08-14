using RW.PMS.Application.Contracts.DTO.File;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Domain.Entities.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Project
{
    public interface IProjectBasicsFilesService : ICrudApplicationService<pro_basics_filesEntity, int>
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        FileReDto Upload(MultiFileDto input);

        /// <summary>
        /// 根据pid获取最新上传文件
        /// </summary>
        /// <param name="Pid"></param>
        /// <returns></returns>
        FileModelDto GetFileShow(int Pid);
    }
}
