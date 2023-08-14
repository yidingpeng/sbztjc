using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.Exceptions;

namespace RW.PMS.API.Controllers
{
    public class ProjectDeliveryController : RWBaseController
    {
        private readonly IContract_InfoService _contract_InfoService;
        private readonly IDictionaryService _dictionaryService;
        private readonly IFileService _fileService;
        private readonly IProjectDeliveryService _prodeliveryService;
        private readonly IWebHostEnvironment _evn;
        private readonly IProject_infoService _project_infoService;
        private readonly IProjectBasicsService _projectBasicsService;
        private readonly IConfiguration _configuration;

        public ProjectDeliveryController(IContract_InfoService contract_InfoService, IDictionaryService dictionaryService, IConfiguration configuration, 
            IFileService fileService,  IWebHostEnvironment evn,  IProjectDeliveryService prodeliveryService, 
            IProject_infoService projectinfoService, IProjectBasicsService projectBasicsService)
        {
            _contract_InfoService = contract_InfoService;
            _dictionaryService = dictionaryService;
            _fileService = fileService;
            _prodeliveryService = prodeliveryService;
            _configuration = configuration;
            _evn = evn;
            _project_infoService = projectinfoService;
            _projectBasicsService = projectBasicsService;
        }

        /// <summary>
        /// 项目交付信息获取验收阶段
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAcceptancePhase")]
        public IResponseDto GetAcceptancePhase()
        {
            var result = _dictionaryService.GetSubItemList("AcceptancePhase");
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 获取项目交付分页数据
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetDeliveryPagedList")]
        public IResponseDto GetDeliveryPagedList([FromQuery] Project_DeliverySearchDto input)
        {
            var result = _prodeliveryService.GetPagedList(input);
            return ResponseDto.Success(result);
        }
        /// <summary>
        /// 获取项目交付分页数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetByIdList")]
        public IResponseDto GetByIdList(int Id)
        {
            var list = _prodeliveryService.GetByIdList(Id);
            return ResponseDto.Success(list);
        }
        /// <summary>
        /// 项目交付新增
        /// </summary>
        /// <returns></returns>
        [HttpPost("ProDeliveryAdd")]
        public IResponseDto ProDeliveryAdd([FromBody] Project_DeliveryDto input)
        {
            var IsExist = _prodeliveryService.IsExist(input);
            if (IsExist)
            {
                return ResponseDto.Error(500, "该交付信息单据已存在");
            }
            var entity = _prodeliveryService.Insert(input);
            return entity.Id > 0 ? ResponseDto.Success("添加成功") : ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
        }

        /// <summary>
        /// 项目交付编辑
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IResponseDto ProDeliveryEdit([FromBody] Project_DeliveryDto input)
        {
            if (input.Id.HasValue)
            {
                var IsExist = _prodeliveryService.IsExist(input);
                if (IsExist)
                {
                    return ResponseDto.Error(500, "该交付信息单据已存在");
                }
                var result = _prodeliveryService.Update(input.Id.Value, input);
                return result > 0 ? ResponseDto.Success("修改成功") : ResponseDto.Error(0, ExceptionCode.UpdateFailed.ToString());
            }
            return ResponseDto.Error(0, ExceptionCode.UpdateFailed.ToString());
        }

        /// <summary>
        /// 项目交付逻辑删除
        /// </summary>
        /// <param name="keys">主键Id数组</param>
        /// <returns></returns>

        [HttpDelete()]
        public IResponseDto ProDeliveryDelete([FromBody] DeleteRequesDto input)
        {
            var ids = input.GetIds();
            var result = _prodeliveryService.Delete(ids);
            return result > 0 ? ResponseDto.Success("删除成功") : ResponseDto.Error(0, ExceptionCode.DeleteFailed.ToString());
        }
        /// <summary>
        /// 获取项目
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetprojectList")]
        public IResponseDto GetprojectList()
        {
            var result = _projectBasicsService.GetList().Where(a=>a.IsDeleted==false);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 获取项目交付所有信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllProjectDelivery")]
        public IResponseDto GetAllProjectDelivery()
        {
            var result = _prodeliveryService.GetList();
            return ResponseDto.Success(result);
        }
    }
}
