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
    public interface ITaskProcessInfoService : ICrudApplicationService<TaskProcessInfoEntity, int>
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<TaskProcessInfoView> PagedList(TaskProcessInfoSearchDto input);

        /// <summary>
        /// 根据项目ID查询设备信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<TaskProcessInfoView> GetByProIDList(int ProjectID);

        /// <summary>
        /// 项目设别信息新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool Insert(TaskProcessInfoView input);

        /// <summary>
        /// 项目设备信息修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool Update(TaskProcessInfoView input);
        /// <summary>
        /// 设备编号唯一
        /// </summary>
        /// <param name="ProjectCode"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool ProOnly(string TaskCode, int Id);
        /// <summary>
        /// 获取项目的父子集合信息
        /// </summary>
        /// <returns></returns>
        IList<TaskProcessInfoView> GetTreeList();
        /// <summary>
        /// 修改上移下移
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        bool UpdateSeqNo(int Id, int ParentId, string Type);

        /// <summary>
        /// 获取当前层级最大排序号
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        int MaxSeqNo(int ParentId);

        /// <summary>
        /// 获取当前层级最小排序号
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        int MinSeqNo(int ParentId);
    }
}
