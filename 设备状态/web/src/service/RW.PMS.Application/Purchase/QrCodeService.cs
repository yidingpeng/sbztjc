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

namespace RW.PMS.Application.Purchase
{
    public class QrCodeService : CrudApplicationService<Qr_CodeEntity, int>, IQrCodeService
    {
        public QrCodeService(IDataValidatorProvider dataValidator,
           IRepository<Qr_CodeEntity, int> repository, IMapper mapper, Lazy<ICurrentUser> currentUser) : base(
           dataValidator, repository, mapper, currentUser)
        {

        }
        #region 二维码信息库
        /// <summary>
        /// 二维码信息库分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<QrCodeDto> PagedList(QrCodeSearchDto input)
        {
            var list = Repository.Select.From<Mat_SupplierEntity>((a, b) => a
                 .LeftJoin(ab => ab.Supplier == b.SupCode))
                 .WhereIf(input.QrCode.NotNullOrEmpty(), (a, b) => a.QrCode.Contains(input.QrCode))
                 .WhereIf(input.MaterialCode.NotNullOrEmpty(), (a, b) => a.MaterialCode.Contains(input.MaterialCode) || a.MaterialName.Contains(input.MaterialCode))
                 .WhereIf(input.ProjectCode.NotNullOrEmpty(), (a, b) => a.ProjectCode.Contains(input.ProjectCode) || a.ProjectName.Contains(input.ProjectCode))
                 .Count(out var total)
                 .OrderByDescending(t => t.t1.Id)
                 .Page(input.PageNo, input.PageSize)
                 .ToList((a, b) => new
                 {
                     Qr = a,
                     supName = b
                 }).Select(t =>
                 {
                     var mapp = Mapper.Map<QrCodeDto>(t.Qr);
                     mapp.SupName = t.supName == null ? "" : t.supName.SupName;
                     return mapp;
                 }).ToList();
            return new PagedResult<QrCodeDto>(list, total);
        }

        /// <summary>
        /// 二维码信息库导出（带条件）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<QrCodeDto> PrintQrCodeList(QrCodeSearchDto input)
        {
            var list = Repository.Select.From<Mat_SupplierEntity>((a, b) => a
                 .LeftJoin(ab => ab.Supplier == b.SupCode))
                 .WhereIf(input.QrCode.NotNullOrEmpty(), (a, b) => a.QrCode.Contains(input.QrCode))
                 .WhereIf(input.MaterialCode.NotNullOrEmpty(), (a, b) => a.MaterialCode.Contains(input.MaterialCode) || a.MaterialName.Contains(input.MaterialCode))
                 .WhereIf(input.ProjectCode.NotNullOrEmpty(), (a, b) => a.ProjectCode.Contains(input.ProjectCode) || a.ProjectName.Contains(input.ProjectCode))
                 .OrderByDescending(t => t.t1.Id)
                 .ToList((a, b) => new
                 {
                     Qr = a,
                     supName = b
                 }).Select(t =>
                 {
                     var mapp = Mapper.Map<QrCodeDto>(t.Qr);
                     mapp.SupName = t.supName == null ? "" : t.supName.SupName;
                     return mapp;
                 }).ToList();
            return new List<QrCodeDto>(list);
        }

        /// <summary>
        /// 根据二维码查询信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public QrCodeDto GetByQrCode(string QrCode)
        {
            var list = Repository.Select.From<Mat_SupplierEntity>((a, b) => a
                 .LeftJoin(ab => ab.Supplier == b.SupCode))
                 .Where(a=>a.t1.QrCode == QrCode)
                 .ToList((a,b) => new
                 {
                     Qr = a,
                     supName = b
                 }).Select(t =>
                 {
                     var mapp = Mapper.Map<QrCodeDto>(t.Qr);
                     mapp.SupName = t.supName == null ? "" : t.supName.SupName;
                     return mapp;
                 }).FirstOrDefault();
            return list;
        }
        /// <summary>
        /// 二维码库是否已经生成
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool IsExist(QrCodeDto input)
        {
            var exist = Repository.Where(t => t.QrCode == input.QrCode).ToOne();
            if (exist == null) return false;
            if (input.Id.HasValue)
            {
                return input.Id.Value != exist.Id;
            }
            return true;
        }
        /// <summary>
        /// 修改是否质检的状态
        /// </summary>
        /// <param name="QrCode"></param>
        /// <param name="qualified"></param>
        /// <returns></returns>
        public bool UpdateMQualified(string QrCode, int qualified)
        {
            //根据二维码号获取信息
            var info = Repository.Select.Where(t => t.QrCode == QrCode).ToOne();
            if (info != null)
            {
                var result = Repository.Orm.Update<Qr_CodeEntity>(info.Id)
                                .Set(a => a.qualified, qualified)
                                .ExecuteAffrows();
                return result > 0;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
