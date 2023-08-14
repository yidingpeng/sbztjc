using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Organization;
using RW.PMS.Application.Contracts.DTO.Post;
using RW.PMS.Application.Contracts.Service;
using RW.PMS.Application.Contracts.System;

namespace RW.PMS.API.Controllers
{
    public class PostController : RWBaseController
    {
        IPostService _postService;
        public PostController(IPostService postService)
        {
            this._postService = postService;
        }

        [HttpGet]
        public IResponseDto GetList([FromQuery] PostQueryDto search)
        {
            //return new string[] { "value1", "value2" };
            var result = _postService.GetPagedList(search);
            return ResponseDto.Success(result);
        }

        [HttpPost]
        //[LogFilter(PageName = "组织架构", ActionName = "修改")]
        public IResponseDto Post([FromBody] PostListDto input)
        {
            var result = _postService.Insert(input);
            return ResponseDto.InsertResult(result != null);
        }

        [HttpPut]
        //[LogFilter(PageName = "组织架构", ActionName = "修改", Format = "{account} {ActionName} {PageName} {input.Name}")]
        public IResponseDto Put([FromBody] PostListDto input)
        {
            var result = _postService.Update(input.Id, input);
            return ResponseDto.UpdateResult(result > 0);
        }

        // DELETE api/<OrganizationController>/5
        [HttpDelete()]
        public IResponseDto Delete([FromBody] DeleteRequesDto input)
        {
            var ids = input.GetIds();
            var result = _postService.Delete(ids);
            return ResponseDto.DeleteResult(result > 0);
        }
    }
}
