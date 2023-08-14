using AutoMapper;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Purchase;
using RW.PMS.Application.Contracts.Purchase;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Purchase;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RW.PMS.Application.Purchase
{
    public class SupplierService : CrudApplicationService<Mat_SupplierEntity, int>, ISupplierService
    {
        public SupplierService(IDataValidatorProvider dataValidator,
           IRepository<Mat_SupplierEntity, int> repository, IMapper mapper, Lazy<ICurrentUser> currentUser) : base(
           dataValidator, repository, mapper, currentUser)
        {
            
        }
        /// <summary>
        /// 供应商分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<SupplierDto> PagedList(SupplierSearchDto input)
        {
            var list = Repository.Orm.Select<Mat_SupplierEntity>()
                 .WhereIf(input.SupCode.NotNullOrEmpty(), a => a.SupCode.Contains(input.SupCode))
                 .WhereIf(input.SupName.NotNullOrEmpty(), a => a.SupName.Contains(input.SupName))
                 .WhereIf(input.SupPrincipal.NotNullOrEmpty(), a => a.SupPrincipal.Contains(input.SupPrincipal))
                 .Count(out var total)
                 .Page(input.PageNo, input.PageSize)
                 .ToList();
            return new PagedResult<SupplierDto>(Mapper.Map<List<SupplierDto>>(list), total);
        }

        /// <summary>
        /// 判断是否存在相同供应商
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool IsExist(SupplierDto input)
        {
            var exist = Repository.Where(t => t.SupCode == input.SupCode && t.IsDeleted == false).ToOne();
            if (exist == null) return false;
            if (input.Id.HasValue)
            {
                return input.Id.Value != exist.Id;
            }
            return true;
        }

        public SupplierDto GetInfoByMCode(string materialCode)
        {
            var info = Repository.Select.From<Mat_OperateEntity>((s, o) => s.LeftJoin(s => s.SupCode == o.Supplier))
                 .Where((s, o) => o.MaterialCode == materialCode)
                 .ToList((s, o) => new
                 {
                     sup = s,
                     op = o
                 })
                 .Select(t =>
                 {
                     var mapp = Mapper.Map<SupplierDto>(t.sup);
                     return mapp;
                 }).First();
            return info;
        }

        
    }
}
