using AutoMapper;
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
    public class PurchaseDetailService : CrudApplicationService<Mat_PurchaseDetailEntity, int>, IPurchaseDetailService
    {
        public PurchaseDetailService(IDataValidatorProvider dataValidator, IRepository<Mat_PurchaseDetailEntity, int> repository, 
            IMapper mapper, Lazy<ICurrentUser> currentUser) : base(dataValidator, repository, mapper, currentUser)
        {
        }
    }
}
