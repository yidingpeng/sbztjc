using AutoMapper;
using RW.PMS.Application.Contracts.DTO.Problem;
using RW.PMS.Domain.Entities.Probelem;
using RW.PMS.Domain.Entities.Problem;

namespace RW.PMS.Application.Contracts.Mapper
{
    public class ProblemProfile : Profile
    {
        public ProblemProfile()
        {
            CreateMap<ProblemfeedbackEntity, ProblemFeedbackDto>();
            CreateMap<ProblemFeedbackDto, ProblemfeedbackEntity>();

            CreateMap<ProblemFeedbackdetailEntity, ProblemFeedbackdetailDto>();
            CreateMap<ProblemFeedbackdetailDto, ProblemFeedbackdetailEntity>();

            CreateMap<ProblemMsgSendEntity, ProblemMsgSendDto>();
            CreateMap<ProblemMsgSendDto, ProblemMsgSendEntity>();

        }

    }
}
