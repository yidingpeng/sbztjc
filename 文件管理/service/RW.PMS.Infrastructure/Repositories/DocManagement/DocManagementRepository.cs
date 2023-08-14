using FreeSql;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.Domain.Entities.DocManagement;
using RW.PMS.Domain.Repositories.DocManagement;
using RW.PMS.Domain.Repositories.System;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RW.PMS.Infrastructure.Repositories.DocManagement
{
    public class DocManagementRepository : BaseRepository<DocManagementEntity>, IDocManagementRepository
    {
        public DocManagementRepository(UnitOfWorkManager uowManger) : base(uowManger.Orm, null)
        {

        }

        public List<DocManagementEntity> GetPagedList(string key)
        {
            return Select.Where( t => t.ProjectCN.Equals(key)).ToList();
        }
    }
}
