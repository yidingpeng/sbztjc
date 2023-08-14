

using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Domain.Entities.Project;
using System.Collections.Generic;

namespace RW.PMS.Application.Contracts.Project

{
    /// <summary>
    ///      项目质保金信息应用服务
    /// </summary>
	public interface IProjectRetMoneyService : ICrudApplicationService<ProjectRetMoneyEntity, int>
    {
        /// <summary>
        ///     获取 项目质保金信息分页数据
        /// </summary>
        /// <param name="input"><see cref="ProjectRetMoneySearchDto" />搜索输入项</param>
        /// <returns></returns>
        public PagedResult<ProjectRetMoneyOutput> GetPagedList(ProjectRetMoneySearchDto input);

        /// <summary>
		/// 根据Id获取质保金信息
		/// </summary>
		/// <param name="Id"></param>
		/// <returns></returns>
		List<ProjectRetMoneyOutput> GetByIdList(int Id);

        /// <summary>
        /// 判断该项目是否已存在质保金
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool IsExist(ProjectRetMoneyDto input);
    }
}
