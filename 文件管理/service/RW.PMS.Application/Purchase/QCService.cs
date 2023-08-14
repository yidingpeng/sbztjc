using AutoMapper;
using RW.PMS.Application.Contracts.DTO.Purchase;
using RW.PMS.Application.Contracts.Purchase;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Common;
using RW.PMS.Domain.Entities.Purchase;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RW.PMS.Application.Purchase
{
    public class QCService : CrudApplicationService<MatQCEntity, int>, IQCService
    {
        public QCService(IDataValidatorProvider dataValidator,
           IRepository<MatQCEntity, int> repository, IMapper mapper, Lazy<ICurrentUser> currentUser) : base(
           dataValidator, repository, mapper, currentUser)
        {

        }

        /// <summary>
        /// 质检分页
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<QCDto> QCPagedList(QCSearchDto input)
        {
            var list = Repository.Select.From<Qr_CodeEntity>((a, b) => a.LeftJoin(o => o.QrCode == b.QrCode))
                 .WhereIf(input.MaterialCode.NotNullOrEmpty(), (a, b) => b.MaterialCode.Contains(input.MaterialCode) || b.MaterialName.Contains(input.MaterialCode))
                 .WhereIf(input.ProjectCode.NotNullOrEmpty(), (a, b) => b.ProjectCode.Contains(input.ProjectCode) || b.ProjectName.Contains(input.ProjectCode))
                 .Count(out var total)
                 .Page(input.PageNo, input.PageSize)
                 .ToList((a, b) => new
                 {
                     qc = a,
                     qr = b
                 }).Select(t =>
                 {
                     var op = Mapper.Map<QCDto>(t.qc);
                     op.ProjectCode = t.qr == null ? "" : t.qr.ProjectCode;
                     op.ProjectName = t.qr == null ? "" : t.qr.ProjectName;
                     op.MaterialCode = t.qr == null ? "" : t.qr.MaterialCode;
                     op.MaterialName = t.qr == null ? "" : t.qr.MaterialName;
                     op.Count = t.qr == null ? 0 : t.qr.Count;
                     op.Supplier = t.qr == null ? "" : t.qr.Supplier;
                     return op;
                 }).ToList();
            return new PagedResult<QCDto>(list, total);
        }
        /// <summary>
        /// 根据二维码查询质检信息
        /// </summary>
        /// <param name="QrCode"></param>
        /// <returns></returns>
        public QCDto QcInfo(string QrCode)
        {
            var info = Repository.Select.From<Qr_CodeEntity>((a, b) => a
                 .LeftJoin(t => t.QrCode == b.QrCode))
                .Where((a, b) => a.QrCode == QrCode)
                 .ToList((a, b) => new
                 {
                     qc = a,
                     qr = b
                 }).Select(t =>
                 {
                     var op = Mapper.Map<QCDto>(t.qc);
                     op.ProjectCode = t.qr.ProjectCode;
                     op.ProjectName = t.qr.ProjectName;
                     op.MaterialCode = t.qr.MaterialCode;
                     op.MaterialName = t.qr.MaterialName;
                     op.Count = t.qr.Count;
                     op.Supplier = t.qr.Supplier;
                     return op;
                 }).FirstOrDefault();
            return info;
        }
        /// <summary>
        /// 是否存在重复质检信息
        /// </summary>
        /// <param name="QrCode"></param>
        /// <returns></returns>
        public bool Exists(string QrCode)
        {
            var list = Repository.Where(o => o.QrCode == QrCode).ToOne();
            return list != null;
        }
    }
}
