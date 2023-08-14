using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Domain.Entities.Project;
using RW.PMS.Application.Contracts.DTO.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Project
{
    public interface IContractInfoFilesService : ICrudApplicationService<ContractInfoFilesEntity, int>
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        FileReDto Upload(MultiFileDto input);

        /// <summary>
        /// 根据父id修改文件所属
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        bool updateFilesByPid(int pid, int filesType);

        /// <summary>
        /// 添加时修改对应文件pid
        /// </summary>
        /// <param name="ConFilesId"></param>
        /// <param name="pId"></param>
        void UpdateImgPid(string ConFilesId, int? pId);

        /// <summary>
        /// 获取pdf文件预览
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FileModelDto GetFileShow(int id);
    }
}


