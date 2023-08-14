using FreeSql;
using RW.PMS.Domain.Entities.Common;
using RW.PMS.Domain.Entities.Material;
using RW.PMS.Domain.Repositories.Material;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RW.PMS.Infrastructure.Repositories.Material
{
    public class MaterialSortRepository : BaseRepository<MaterialSortEntity>, IMaterialSortRepository
    {
        public MaterialSortRepository(UnitOfWorkManager uowManger) : base(uowManger.Orm, null)
        {
           
        }
    }
}
