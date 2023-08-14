using FreeSql;
using RW.PMS.Domain.Entities.DocManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Repositories.DocManagement
{
    public interface IDocManagementRepository : IBaseRepository<DocManagementEntity>
    {
        /// <summary>
        /// 获取文档分页数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="PageNo"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        List<DocManagementEntity> GetPagedList(string key);
    }
}
