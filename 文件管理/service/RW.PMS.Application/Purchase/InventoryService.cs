using AutoMapper;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Purchase;
using RW.PMS.Application.Contracts.Purchase;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Purchase;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Purchase
{
    public class InventoryService : CrudApplicationService<Mat_InventoryEntity, int>, IinventoryService
    {
        public InventoryService(IDataValidatorProvider dataValidator,
           IRepository<Mat_InventoryEntity, int> repository, IMapper mapper, Lazy<ICurrentUser> currentUser) : base(
           dataValidator, repository, mapper, currentUser)
        {

        }

        /// <summary>
        /// 库存信息分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<InventoryDto> InventoryPagedList(InventorySearchDto input)
        {
            var list = Repository.Orm.Select<Mat_InventoryEntity>()
                 .WhereIf(input.MaterialCode.NotNullOrEmpty(), a => a.MaterialCode.Contains(input.MaterialCode) || a.MaterialName.Contains(input.MaterialName))
                 .WhereIf(input.ProjectCode.NotNullOrEmpty(), a => a.ProjectCode.Contains(input.ProjectCode) || a.ProjectName.Contains(input.ProjectName))
                 .Count(out var total)
                 .Page(input.PageNo, input.PageSize)
                 .ToList();
            return new PagedResult<InventoryDto>(Mapper.Map<List<InventoryDto>>(list), total);
        }
        /// <summary>
        /// 根据物料编码查询库存
        /// </summary>
        /// <param name="materialCode"></param>
        /// <returns></returns>
        public Mat_InventoryEntity GetInventoryInfo(string materialCode)
        {
            return Repository.Where(o => o.MaterialCode == materialCode).ToOne();
        }
        /// <summary>
        /// 库存数量变更
        /// </summary>
        /// <param name="type"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool InvCountEdit(int count,string materialCode)
        {
            Repository.Orm.Update<Mat_InventoryEntity>()
                          .Set(a => a.WarehousCount, count)
                          .Where(a => a.MaterialCode == materialCode)
                          .ExecuteAffrows();
            return true;
        }

        /// <summary>
        /// 库存数量变更
        /// </summary>
        /// <param name="type"></param>
        /// <param name="count"></param>
        /// <param name="materialCode"></param>
        /// <returns></returns>
        public bool InvCountEdit(int type, int count, string materialCode)
        {
            bool result = false;
            var material = Repository.Where(o => o.MaterialCode == materialCode).ToOne();
            if (material != null)
            {
                var TotalCount = material.WarehousCount;
                if (type == 1)
                {
                    TotalCount += count;
                }
                else if (type == 2)
                {
                    TotalCount -= count;
                }
                if (TotalCount >= 0)
                {
                    int up = Repository.Orm.Update<Mat_InventoryEntity>()
                                              .Set(a => a.WarehousCount, TotalCount)
                                              .Where(a => a.MaterialCode == materialCode)
                                              .ExecuteAffrows();
                    result = up > 0;
                }
            }
            return result;
        }
    }
}
