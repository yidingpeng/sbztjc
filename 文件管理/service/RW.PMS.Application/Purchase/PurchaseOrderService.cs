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
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Purchase
{
    public class PurchaseOrderService : CrudApplicationService<Mat_PurchaseOrderEntity, int>, IPurchaseOrderService
    {
        public PurchaseOrderService(IDataValidatorProvider dataValidator, IRepository<Mat_PurchaseOrderEntity, int> repository,
            IMapper mapper, Lazy<ICurrentUser> currentUser) : base(dataValidator, repository, mapper, currentUser)
        {
        }

        /// <summary>
        /// 申请列表分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<Mat_PurchaseOrderDto> GetPagedList(PurchaseOrderSearchDto input)
        {
            var list = Repository.Select.From<UserEntity>((a, b) => a.LeftJoin(a => a.CreatedBy == b.Id))
                 .WhereIf(input.Title.NotNullOrEmpty(), a => a.t1.Title.Contains(input.Title))
                 .WhereIf(input.Reason.NotNullOrEmpty(), a => a.t1.Reason.Contains(input.Reason))
                 .WhereIf(input.status > 0, a => a.t1.status == input.status)
                 .WhereIf(input.CreatedBy > 0, a => a.t1.CreatedBy == input.CreatedBy)
                 .OrderByDescending((a,b)=>a.Id)
                 .Count(out var total)
                 .Page(input.PageNo, input.PageSize)
                 .ToList((a, b) => new
                 {
                     AllM = a,
                     User = b,
                 })
                 .Select(t =>
                 {
                     var mapp = Mapper.Map<Mat_PurchaseOrderDto>(t.AllM);
                     mapp.CreatedByName = t.User == null ? "" : t.User.Account;
                     return mapp;
                 }).ToList();
            return new PagedResult<Mat_PurchaseOrderDto>(list, total);
        }

        public bool IsExist(Mat_PurchaseOrderDto input)
        {
            var exist = Repository.Where(t => t.ApplicationNo == input.ApplicationNo && t.IsDeleted == false).ToOne();
            if (exist == null) return false;
            if (input.Id.HasValue)
            {
                return input.Id.Value != exist.Id;
            }
            return true;
        }
    }
}
