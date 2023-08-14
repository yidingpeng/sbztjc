using System;
using AutoMapper;
using RW.PMS.Application.Contracts;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Security.User;

namespace RW.PMS.Application
{
    public class ApplicationService : IApplicationService
    {
        protected readonly Lazy<ICurrentUser> CurrentUser;
        protected readonly IDataValidatorProvider DataValidator;
        protected readonly IMapper Mapper;

        public ApplicationService(IDataValidatorProvider dataValidator, IMapper mapper, Lazy<ICurrentUser> currentUser)
        {
            CurrentUser = currentUser;
            DataValidator = dataValidator;
            Mapper = mapper;
        }
    }
}