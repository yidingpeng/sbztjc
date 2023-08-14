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


public class bd_SalesAreaInfoController : RWBaseController
{
    private readonly ISalesAreaInfoService _salesAreaInfoService;
    private readonly IDictionaryService _dictionaryService;


    public bd_SalesAreaInfoController(ISalesAreaInfoService salesAreaInfoService, IDictionaryService dictionaryService)
    {
        _salesAreaInfoService = salesAreaInfoService;
        _dictionaryService = dictionaryService;
    }

    [HttpGet("getPlaceName")]
    public IResponseDto getPlaceName()
    {
        var result = _dictionaryService.GetSubItemList("PlaceName");
        return ResponseDto.Success(result);
    }

    [HttpPost("GetPagedList")]
    public IResponseDto GetPagedList([FromQuery] bd_SalesAreaInfoSearchDto input)
    {
        var result = _salesAreaInfoService.GetPagedList(input);
        return ResponseDto.Success(result);
    }


    [HttpPost("doAdd")]
    public IResponseDto doAdd([FromBody] bd_SalesAreaInfoDto input)
    {
        var IsExist = _salesAreaInfoService.IsExist(input);
        if (IsExist)
        {
            return ResponseDto.Error(500, "编号或名称已存在");
        }
        var entity = _salesAreaInfoService.Insert(input);
        return entity.Id > 0 ? ResponseDto.Success("添加成功") : ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
    }

    [HttpPut]
    public IResponseDto doEdit([FromBody] bd_SalesAreaInfoDto input)
    {
        if (input.Id.HasValue)
        {
            var IsExist = _salesAreaInfoService.IsExist(input);
            if (IsExist)
            {
                return ResponseDto.Error(500, "该市场片区编号已存在");
            }
            var result = _salesAreaInfoService.Update(input.Id.Value, input);
            return result > 0 ? ResponseDto.Success("修改成功") : ResponseDto.Error(0, ExceptionCode.UpdateFailed.ToString());
        }
        return ResponseDto.Error(0, ExceptionCode.UpdateFailed.ToString());
    }

    [HttpDelete()]
    public IResponseDto doDelete([FromBody] DeleteRequesDto input)
    {
        var ids = input.GetIds();
        var result = _salesAreaInfoService.Delete(ids);
        return result > 0 ? ResponseDto.Success("删除成功") : ResponseDto.Error(0, ExceptionCode.DeleteFailed.ToString());
    }

    [HttpGet("GetSalseAreaSelect")]
    public IResponseDto GetSalseAreaSelect()
    {
        var result=_salesAreaInfoService.GetSalesAreaSelect();
        return ResponseDto.Success(result);
    }
}
