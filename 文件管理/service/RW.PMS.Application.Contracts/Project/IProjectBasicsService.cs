using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Output.Project;
using RW.PMS.Domain.Entities.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Project
{
    public interface IProjectBasicsService: ICrudApplicationService<ProjectBasicsEntity, int>
    {
        /// <summary>
        /// 项目信息分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<ProjectBasicsView> PagedList(ProjectBasicsSearchDto input);

        /// <summary>
        /// 项目基础信息导出数据源
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<ProjectBasicsView> ProjectListDerive(ProjectBasicsSearchDto input);

        /// <summary>
        /// 查询所有项目
        /// </summary>
        /// <param name="key"></param>
        /// <param name="PageNo"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        PagedResult<ProjectBasicsView> GetAllProject(string key, int PageNo, int PageSize);
        /// <summary>
        /// 项目信息新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool Insert(ProjectBasicsListDto input);
        /// <summary>
        /// 项目信息修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool Update(ProjectBasicsListDto input);
        /// <summary>
        /// 根据父ID查询项目基础数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<ProjectBasicsView> GetByParentIdList(int Id);

        /// <summary>
        /// 获取项目的父子信息
        /// </summary>
        /// <param name="ProjectKey"></param>
        /// <returns></returns>
        IList<ProjectBasicsView> GetTreeList(string ProjectKey);

        /// <summary>
        /// 获取项目的父子集合信息表格
        /// </summary>
        /// <returns></returns>
        IList<ProjectBasicsView> GetTreeListTable(string ProjectKey, string pt_idsTxt, int PageNo, int PageSize);

        /// <summary>
        /// 项目编号唯一
        /// </summary>
        /// <param name="ProjectCode"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        bool ProOnly(string ProjectCode, int Id);

        /// <summary>
        /// 根据ID查询父ID的项目信息
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="proClass"></param>
        /// <returns></returns>
        List<ProjectBasicsView> GetListByClientIdAndClass(int clientId, string proClassTxt);

        /// <summary>
        /// 根据id查询信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        List<ProjectBasicsView> GetListById(int Id);

        /// <summary>
        /// 营销销售数据基础信息分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<SellBasicsDataView> SellBasicsPagedList(SellBasicsDataSearchDto input);

        /// <summary>
        /// 营销销售数据基础信息子项目信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<SellBasicsDataView> ParentSellBasicsPagedList(int Id);

        /// <summary>
        /// 获取所有一级项目
        /// </summary>
        /// <param name="ProjectName"></param>
        /// <param name="ProjectCode"></param>
        /// <returns></returns>
        List<ProjectBasicsView> GetListByIds(string ProjectName, string ProjectCode);

        /// <summary>
        /// 根据项目项目编码和项目名称查询项目
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        PagedResult<ProjectBasicsView> GetAllList(string key, int PageNo, int PageSize);

        /// <summary>
        /// 编辑项目信息状态类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool UpdateProState(int Id, int ProjectStatus,int proState);

        /// <summary>
        /// 编辑项目备注信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool UpdateProRemark(int Id, string Remark);

        /// <summary>
        /// 获取所有项目信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        List<ProjectBasicsView> GetAllList(string key);

        /// <summary>
        /// 消息提醒
        /// </summary>
        /// <param name="link"></param>
        /// <param name="esburl"></param>
        /// <returns></returns>
        bool MessagePush(string link, string esburl);

        /// <summary>
        /// 通过项目ID查询问题反馈信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool ProblemInfo(int[] ids);
    }
}
