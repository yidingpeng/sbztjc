using RW.PMS.Application.Contracts.DTO.Problem;
using RW.PMS.Domain.Entities.Basics;
using RW.PMS.Domain.Entities.Problem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RW.PMS.Application.Contracts.DTO.Project;

namespace RW.PMS.Application.Contracts.Basics
{
    public interface IMaterialDetailService : ICrudApplicationService<MaterialDetailEntity, int>
    {
        FileReDto Upload(FileDto input);

        /// <summary>
        /// 根据父id查询文件
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        List<ProblemFilesDto> GetFilesByPid(int pid);

        /// <summary>
        /// 修改图片父id
        /// </summary>
        /// <param name="imgFilesId"></param>
        /// <param name="pId"></param>
        void UpdateImgPid(string imgFilesId, int? pId);

        /// <summary>
        /// 逻辑删除该父id图片
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        bool UpdateImgPidtoZero(int pid);

        /// <summary>
        /// 根据id获取反馈图片信息id
        /// </summary>
        /// <param name="ptid"></param>
        /// <returns></returns>
        int[] GetImageByPtid(int ptid);

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        byte[] GetFile(int id);

        /// <summary>
        /// 通过角色名查找下面的用户ID
        /// </summary>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        int[] GetRoleNameByUserId(string RoleName);
    }
}
