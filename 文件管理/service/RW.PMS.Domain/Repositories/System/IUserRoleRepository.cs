using FreeSql;
using RW.PMS.Domain.Entities.System;
using System.Collections.Generic;

namespace RW.PMS.Domain.Repositories.System
{
    /// <summary>
    /// 用户角色仓储基类
    /// </summary>
    public interface IUserRoleRepository : IBaseRepository<UserRoleEntity>
    {
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        List<UserRoleEntity> GetList();
    }

}