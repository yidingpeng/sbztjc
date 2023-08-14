using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Domain.Entities.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Project
{
    public interface IProjectDeviceDetailsService : ICrudApplicationService<ProjectDeviceDetailsEntity, int>
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<ProjectDeviceDetailsView> PagedList(ProjectDeviceDetailsSearchDto input);

        /// <summary>
        /// 根据项目ID查询设备信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<ProjectDeviceDetailsView> GetByProIDList(int ProjectID);

        /// <summary>
        /// 根据多个项目ID查询所有设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<ProjectDeviceDetailsView> GetIdsList(int[] intarrs);

        /// <summary>
        /// 项目设别信息新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool Insert(ProjectDeviceDetailsView input);

        /// <summary>
        /// 项目设备信息修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool Update(ProjectDeviceDetailsView input);
        /// <summary>
        /// 设备编号唯一
        /// </summary>
        /// <param name="ProjectCode"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool ProOnly(string DeviceNo, int Id);
    }
}
