using AutoMapper;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Purchase;
using RW.PMS.Application.Contracts.Message;
using RW.PMS.Application.Contracts.Purchase;
using RW.PMS.Application.Contracts.System;
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
    public class FifoService:CrudApplicationService<Mat_FifoEntity, int>, IFifoService
    {
        public FifoService(IDataValidatorProvider dataValidator,
           IRepository<Mat_FifoEntity, int> repository, IMapper mapper, Lazy<ICurrentUser> currentUser) : base(
           dataValidator, repository, mapper, currentUser)
        {

        }
        #region 采购出入库信息
        /// <summary>
        /// 出入库信息分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<FifoDto> FifoPagedList(FifoSearchDto input)
        {
            var list = Repository.Orm.Select<Mat_FifoEntity, Mat_InventoryEntity>()
                     .LeftJoin(o => o.t1.MaterialCode == o.t2.MaterialCode)
                     .WhereIf(input.Type > 0, a => a.t1.Type == input.Type)
                     .WhereIf(input.MaterialCode.NotNullOrEmpty(), a => a.t1.MaterialCode.Contains(input.MaterialCode) || a.t1.MaterialName.Contains(input.MaterialCode))
                     .WhereIf(input.ProjectCode.NotNullOrEmpty(), a => a.t1.ProjectCode.Contains(input.ProjectCode) || a.t1.ProjectName.Contains(input.ProjectCode))
                     .OrderByDescending(o=>o.t1.FifoDateTime)
                     .Count(out var total)
                     .Page(input.PageNo, input.PageSize)
                     .ToList(a => new
                     {
                         AllM = a.t1,
                         inventory = a.t2
                     })
                     .Select(t =>
                     {
                         var mapp = Mapper.Map<FifoDto>(t.AllM);
                         mapp.KuCun = t.inventory == null ? 0 : t.inventory.WarehousCount;
                         return mapp;
                     }).ToList();
            return new PagedResult<FifoDto>(list, total);
        }

        /// <summary>
        /// 根据二维码号查询最新的出入库信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public FifoDto GetByQrCodeFifo(string qrcode ,int type)
        {
            var list = Repository.Select.From<Mat_InventoryEntity>((a,b)=>a.LeftJoin(o=>o.MaterialCode==b.MaterialCode))
                .Where((a,b) => a.QrCode == qrcode && a.Type == type)
                .OrderByDescending((a,b) => a.CreatedAt)
                .ToList(a => new {
                    AllM = a.t1,
                    inventory = a.t2
                }).Select(t => {
                    var mapp = Mapper.Map<FifoDto>(t.AllM);
                    mapp.KuCun = t.inventory == null ? 0 : t.inventory.WarehousCount;
                    return mapp;
                }).FirstOrDefault();
            return list;
        }
        /// <summary>
        /// 返回已经出入库的数量
        /// </summary>
        /// <param name="qrcode"></param>
        /// <returns></returns>
        public int GetCkCount(string qrcode,int type)
        {
            int count = 0;
            var list = Repository.Where(o => o.QrCode == qrcode && o.Type == type).ToList();
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    count += item.Count;
                }
            }
            return count;
        }

        /// <summary>
        /// 判断是否已经出入库
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool IsExist(FifoDto input)
        {
            var exist = Repository.Where(t => t.Type == input.Type && t.QrCode==input.QrCode).ToOne();
            if (exist == null) return false;
            if (input.Id.HasValue)
            {
                return input.Id.Value != exist.Id;
            }
            return true;
        }
        #endregion
    }
}
