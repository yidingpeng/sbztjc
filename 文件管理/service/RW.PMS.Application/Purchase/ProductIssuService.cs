using AutoMapper;
using RW.PMS.Application.Contracts.Purchase;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Purchase;
using RW.PMS.Domain.Repositories;
using System;

namespace RW.PMS.Application.Purchase
{
    public class ProductIssuService : CrudApplicationService<Mat_ProductIssuEntity, int>, IProductIssuService
    {
        public ProductIssuService(IDataValidatorProvider dataValidator,
           IRepository<Mat_ProductIssuEntity, int> repository, IMapper mapper, Lazy<ICurrentUser> currentUser) : base(
           dataValidator, repository, mapper, currentUser)
        {

        }
    }
}
