using Microsoft.AspNetCore.Mvc;
using RW.PMS.API.Controllers;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Input.System;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.Exceptions;
using RW.PMS.Domain.Entities.Project;
using System.Web;


public class ProjectDeliveryFileController : RWBaseController
{
    private readonly IContract_InfoService _contract_InfoService;
    private readonly IDictionaryService _dictionaryService;
    private readonly IFileService _fileService;
    private readonly IContractInfoFilesService _contractInfoFilesService;
    private readonly IProjectDeliveryService _prodeliveryService;
    private readonly IWebHostEnvironment _evn;
    private readonly IProject_infoService _project_infoService;
    private readonly IConfiguration _configuration;
    private readonly IProjectBasicsService _projectBasicsService;
    private readonly IProjectInvoicingService _projectInvoicingService;
    private readonly IProjectDeliveryFileService _projectDeliveryFileService;


    public ProjectDeliveryFileController(IContract_InfoService contract_InfoService, IDictionaryService dictionaryService, IFileService fileService,
        IProjectInvoicingService projectInvoicingService, IProjectDeliveryFileService projectDeliveryFileService,
        IWebHostEnvironment evn, IProjectDeliveryService prodeliveryService, IProject_infoService projectinfoService, IContractInfoFilesService contractInfoFilesService,
        IConfiguration configuration, IProjectBasicsService projectBasicsService)
    {
        _contract_InfoService = contract_InfoService;
        _dictionaryService = dictionaryService;
        _fileService = fileService;
        _prodeliveryService = prodeliveryService;
        _evn = evn;
        _project_infoService = projectinfoService;
        _configuration = configuration;
        _projectBasicsService = projectBasicsService;
        _contractInfoFilesService = contractInfoFilesService;
        _projectInvoicingService = projectInvoicingService;
        _projectDeliveryFileService = projectDeliveryFileService;
    }
    /// <summary>
    /// 项目交付文件类型
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetDeliveryType")]
    public IResponseDto GetDeliveryType()
    {
        var result = _dictionaryService.GetSubItemList("DeliveryType");
        return ResponseDto.Success(result);
    }

    /// <summary>
    /// 获取分页数据
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet("GetList")]
    public IResponseDto GetList([FromQuery] ProjectDeliveryFileSearchDto input)
    {
        var result = _projectDeliveryFileService.GetPagedList(input);
        return ResponseDto.Success(result);
    }

    /// <summary>
    /// 根据id获取数据
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    [HttpGet("GetByIdList")]
    public IResponseDto GetByIdList(int Id)
    {
        var result = _projectDeliveryFileService.GetByIdList(Id);
        return ResponseDto.Success(result);
    }

    /// <summary>
    /// 添加项目交付文件
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("DoAdd")]
    public IResponseDto DoAdd([FromBody] ProjectDeliveryFileDto input)
    {
        var IsExist = _projectDeliveryFileService.IsExist(input);
        if (IsExist)
        {
            return ResponseDto.Error(500, "该项目交付文件数据已存在");
        }
        _projectDeliveryFileService.Insert(input);
        return ResponseDto.Success("添加成功！");
    }
    /// <summary>
    /// 修改项目交付文件
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut]
    public IResponseDto DoEdit([FromBody] ProjectDeliveryFileDto input)
    {
        if (input.Id.HasValue)
        {
            var IsExist = _projectDeliveryFileService.IsExist(input);
            if (IsExist)
            {
                return ResponseDto.Error(500, "该项目交付文件数据已存在");
            }
            var result = _projectDeliveryFileService.Update(input.Id.Value, input);
            return result > 0 ? ResponseDto.Success("修改成功") : ResponseDto.Error(0, ExceptionCode.UpdateFailed.ToString());
        }
        return ResponseDto.Error(500, "ID不能为空！");
    }

    /// <summary>
    /// 删除项目交付文件
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpDelete]
    public IResponseDto DoDelete([FromBody] DeleteRequesDto model)
    {
        var ids = model.GetIds();
        if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
        int count = _projectDeliveryFileService.Delete(ids);
        return ResponseDto.Success($"成功删除了{count}条数据。");
    }

}
