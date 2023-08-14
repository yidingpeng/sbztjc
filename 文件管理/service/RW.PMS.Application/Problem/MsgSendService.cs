using AutoMapper;
using RW.PMS.Application.Contracts.DTO.Problem;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Problem;
using RW.PMS.Domain.Repositories;
using System;

namespace RW.PMS.Application.Problem
{
    public class MsgSendService : CrudApplicationService<ProblemMsgSendEntity, int>, IMsgSendService
    {
        public MsgSendService(IDataValidatorProvider dataValidator,
            IRepository<ProblemMsgSendEntity, int> repository, IMapper mapper,
            Lazy<ICurrentUser> currentUser) :
            base(dataValidator, repository, mapper, currentUser)
        {
        }
    }
}
