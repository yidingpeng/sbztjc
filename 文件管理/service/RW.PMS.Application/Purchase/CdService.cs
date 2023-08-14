using AutoMapper;
using RW.PMS.Application.Contracts.DTO.Purchase;
using RW.PMS.Application.Contracts.Purchase;
using RW.PMS.CrossCutting.DataValidator;
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
    public class CdService : CrudApplicationService<MatCDEntity, int>, ICdService
    {
        public CdService(IDataValidatorProvider dataValidator,
           IRepository<MatCDEntity, int> repository, IMapper mapper, Lazy<ICurrentUser> currentUser) : base(
           dataValidator, repository, mapper, currentUser)
        {

        }

        /// <summary>
        /// 根据二维码查询报检信息
        /// </summary>
        /// <param name="QrCode"></param>
        /// <returns></returns>
        public CDDto CDInfo(string QrCode)
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
                     var op = Mapper.Map<CDDto>(t.qc);
                     op.ProjectCode = t.qr.ProjectCode;
                     op.ProjectName = t.qr.ProjectName;
                     op.MaterialCode = t.qr.MaterialCode;
                     op.MaterialName = t.qr.MaterialName;
                     op.Supplier = t.qr.Supplier;
                     return op;
                 }).FirstOrDefault();
            return info;

        }
    }
}
