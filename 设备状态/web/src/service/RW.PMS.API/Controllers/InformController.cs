using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Notice;
using RW.PMS.Application.Contracts.Message;
using RW.PMS.Application.Contracts.System;

namespace RW.PMS.API.Controllers
{
    public class InformController : RWBaseController
    {
        private readonly INoticeService _noticeService;
        private readonly IConfigService _configService;
        IMapper mapper;
        public InformController(
            INoticeService noticeService,
            IConfigService configService,
            IMapper map)
        {
            _noticeService = noticeService;
            _configService = configService;
            this.mapper = map;
        }

        [HttpGet("{id}")]
        public IResponseDto GetOne(int id)
        {
            var item = _noticeService.Get(id);
            var dto = mapper.Map<NoticeOutputDto>(item);
            return ResponseDto.Success(dto);
        }

        [HttpGet]
        public IResponseDto GetList([FromQuery] NoticeQueryInputDto input)
        {
            var lst = _noticeService.GetPagedList(input);
            return ResponseDto.Success(lst);
        }

        [HttpGet("types")]
        public IResponseDto GetTypes()
        {
            var lst = _configService.GetConfigs("noticeType");
            return ResponseDto.Success(lst);
        }

        [HttpPost]
        public IResponseDto Add([FromBody] NoticeEditDto input)
        {
            var entity = _noticeService.Insert(input);
            return ResponseDto.Success("添加成功");
        }

        [HttpGet("top")]
        public IResponseDto GetTops(int count = 999)
        {
            var lst = _noticeService.GetPagedList(new NoticeQueryInputDto { PageNo = 1, PageSize = 999 });
            return ResponseDto.Success(lst);
        }

        [HttpPost("publish")]
        public IResponseDto Publish(int id)
        {
            _noticeService.Publish(id);
            return ResponseDto.Success("发布成功");
        }

        [HttpPut]
        public IResponseDto Update([FromBody] NoticeEditDto input)
        {
            var entity = _noticeService.Update(input.Id, input);
            return ResponseDto.Success("修改成功");
        }

        [HttpDelete("{id}")]
        public IResponseDto Delete(int id)
        {
            var entity = _noticeService.Delete(id);
            return ResponseDto.Success("删除成功");
        }
    }
}
