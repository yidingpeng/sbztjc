using AutoMapper;
using RW.PMS.Application.Contracts.Input.Message;
using RW.PMS.Application.Contracts.Output.Message;
using RW.PMS.Domain.Entities.Message;

namespace RW.PMS.Application.Contracts.Mapper
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<MessageConfigEntity, MessageConfigOutput>()
                .ForMember(t => t.MessageLevel, opt => opt.Ignore());
            CreateMap<MessageConfigInput, MessageConfigEntity>();
            CreateMap<MessageConfigEntity, MessageConfigView>()
                .ForMember(t => t.Target, opt => opt.Ignore());

            CreateMap<MessageContentView, MessageContentOutput>()
                .ForMember(t => t.MessageLevel, opt => opt.MapFrom(s => s.DictionaryText));
        }
    }
}