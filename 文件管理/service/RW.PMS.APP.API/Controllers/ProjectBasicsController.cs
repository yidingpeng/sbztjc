using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.Project;

namespace RW.PMS.API.Controllers
{
    public class ProjectBasicsController : RWBaseController
    {
        IProjectBasicsService _projectBasicsService;
        public ProjectBasicsController(IProjectBasicsService projectBasicsService)
        {
            _projectBasicsService = projectBasicsService;
        }
        /// <summary>
        /// 获取所有项目信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllList")]
        public IResponseDto GetAllList(string? key)
        {
            var result = _projectBasicsService.GetAllList(key);
            return ResponseDto.Success(result);
        }
    }
}
