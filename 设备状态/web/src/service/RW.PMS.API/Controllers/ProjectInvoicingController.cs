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


public class ProjectInvoicingController : RWBaseController
{
    private readonly IProjectInvoicingService _projectInvoicingService;


    public ProjectInvoicingController(IProjectInvoicingService projectInvoicingService)
    {
        _projectInvoicingService = projectInvoicingService;
    }

    /// <summary>
    /// 获取项目开票信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet("GetList")]
    public IResponseDto GetList([FromQuery] ProjectInvoicingSearchDto input)
    {
        var result = _projectInvoicingService.GetPagedList(input);
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
        var result = _projectInvoicingService.GetByIdList(Id);
        return ResponseDto.Success(result);
    }

    /// <summary>
    /// 添加项目开票
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("DoAdd")]
    public IResponseDto DoAdd([FromBody] ProjectInvoicingDto input)
    {
        var IsExist = _projectInvoicingService.IsExist(input);
        if (IsExist)
        {
            return ResponseDto.Error(500, "该开票编号已存在");
        }
        _projectInvoicingService.Insert(input);
        return ResponseDto.Success("添加成功！");
    }
    /// <summary>
    /// 修改项目开票
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut]
    public IResponseDto DoEdit([FromBody] ProjectInvoicingDto input)
    {
        if (input.Id.HasValue)
        {
            var IsExist = _projectInvoicingService.IsExist(input);
            if (IsExist)
            {
                return ResponseDto.Error(500, "该开票编号已存在");
            }
            var result = _projectInvoicingService.Update(input.Id.Value, input);
            return result > 0 ? ResponseDto.Success("修改成功") : ResponseDto.Error(0, ExceptionCode.UpdateFailed.ToString());
        }
        return ResponseDto.Error(500, "ID不能为空！");
    }

    /// <summary>
    /// 删除项目开票
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpDelete]
    public IResponseDto DoDelete([FromBody] DeleteRequesDto model)
    {
        var ids = model.GetIds();
        if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
        int count = _projectInvoicingService.Delete(ids);
        return ResponseDto.Success($"成功删除了{count}条数据。");
    }

    /// <summary>
    /// 根据客户名称获取开票数据
    /// </summary>
    /// <param name="clietId"></param>
    /// <returns></returns>
    [HttpGet("GetListByClientId")]
    public IResponseDto GetListByClientId(int clietId)
    {
        var reslult = _projectInvoicingService.GetListByClientId(clietId);
        return ResponseDto.Success(reslult);
    }

}
