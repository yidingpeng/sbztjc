using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Message;
using RW.PMS.Application.Contracts.Input.Message;
using RW.PMS.Application.Contracts.Message;

namespace RW.PMS.API.Controllers
{
    public class MessageController : RWBaseController
    {
        private readonly IMessageContentService messageService;
        private readonly IMapper mapper;
        public MessageController(IMessageContentService messageService, IMapper mapper)
        {
            this.messageService = messageService;
            this.mapper = mapper;
        }

        [HttpGet()]
        public IResponseDto GetList([FromQuery] MessageSearchInputDto input)
        {
            var list = messageService.GetPagedList(input);
            return ResponseDto.Success(list);
        }

        [HttpGet("{id}")]
        public IResponseDto GetOne(int id)
        {
            var item = messageService.ReadMessage(id);
            return ResponseDto.Success(item);
        }


        [HttpGet("top")]
        public IResponseDto GetTop(int count)
        {
            var lst = messageService.GetUnReadMessage(count);

            return ResponseDto.Success(lst);
        }
    }
}
