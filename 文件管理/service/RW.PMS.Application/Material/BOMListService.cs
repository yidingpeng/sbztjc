using AutoMapper;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Material;
using RW.PMS.Application.Contracts.Material;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Material;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Material
{
    public class BOMListService : CrudApplicationService<BOMListEntity, int>, IBOMListService
    {
        public BOMListService(IDataValidatorProvider dataValidator, IRepository<BOMListEntity, int> repository, 
            IMapper mapper, Lazy<ICurrentUser> currentUser) : base(dataValidator, repository, mapper, currentUser)
        {
        }
        /// <summary>
        /// BOM清单分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<BOMListEntity> GetPageList(BOMListSearchDto input)
        {
            var list = Repository.Select
                .WhereIf(input.MaterialCode.NotNullOrWhiteSpace(), m => m.MaterialCode.Contains(input.MaterialCode))
                .WhereIf(input.MaterialName.NotNullOrWhiteSpace(), m => m.MaterialName.Contains(input.MaterialName))
                .WhereIf(input.ProjectCode.NotNullOrWhiteSpace(), m => m.ProjectCode.Contains(input.ProjectCode))
                .Count(out var total)
                .Page(input.PageNo, input.PageSize)
                .ToList();
            return new PagedResult<BOMListEntity>(list, total);
        }

        /// <summary>
        /// 批量增加
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        public bool BatchDoAdd(List<BOMListEntity> datas)
        {
            try
            {
                Repository.Orm.InsertOrUpdate<BOMListEntity>()
                    .SetSource(datas)
                    //.IfExistsDoNothing()//如果数据存在，啥事也不干
                    .ExecuteAffrows();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }
        /// <summary>
        /// 逻辑删除BOM
        /// </summary>
        /// <param name="MaterialCode"></param>
        /// <returns></returns>
        public bool DeleteBOM(string MaterialCode)
        {
            try
            {
                Repository.Orm.Update<BOMListEntity>()
                        .Set(a => a.IsDeleted, true)
                        .Where(a => a.MaterialCode == MaterialCode).ExecuteAffrows();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
