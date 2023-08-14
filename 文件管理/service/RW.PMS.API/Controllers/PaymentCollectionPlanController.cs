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


public class PaymentCollectionPlanController : RWBaseController
{
    private readonly IDictionaryService _dictionaryService;
    private readonly IPaymentCollectionPlanService _paymentCollectionPlanService;

    public PaymentCollectionPlanController(IDictionaryService dictionaryService, IPaymentCollectionPlanService paymentCollectionPlanService)
    {
        _dictionaryService = dictionaryService;
        _paymentCollectionPlanService = paymentCollectionPlanService;
    }
    /// <summary>
    /// 根据合同id查询数据
    /// </summary>
    /// <param name="contrctid"></param>
    /// <returns></returns>
    [HttpGet("List")]
    public IResponseDto GetList(int contrctid)
    {
        var result = _paymentCollectionPlanService.GetListByConId(contrctid);
        return ResponseDto.Success(result);
    }

    /// <summary>
    /// 删除合同id为0的回款计划
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet("DoDeleteCtIdZero")]
    public void DoDeleteCtIdZero(string? newIdsStr)
    {
        _paymentCollectionPlanService.DeleteAllCtIdZero(newIdsStr);
    }
    /// <summary>
    /// 修改回款计划
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// 
    [HttpPut]
    public IActionResult DoEditOrAdd([FromBody] List<PaymentCollectionPlanDto> input)
    {
        string retStr = "";
        foreach (var item in input)
        {
            if (item.Id > 0)
            {
                //编辑
                //判断是否存在相同数据
                if (!_paymentCollectionPlanService.HasHhisItem(item))
                {
                    //不存在，添加
                    var b = _paymentCollectionPlanService.Insert(item).Id;
                    retStr += b + ',';
                }
                else
                {
                    //存在，修改
                    _paymentCollectionPlanService.Update(item.Id.Value, item);
                }
            }
            else
            {
                //添加
                var a = _paymentCollectionPlanService.Insert(item).Id;
                retStr = retStr+ a + ",";
            }
        }
        return new JsonResult(new {newIds=retStr });
    }

    /// <summary>
    /// 回款比例
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    /// 
    [HttpPut("BiLiEditOrAdd")]
    public IResponseDto BiLiEditOrAdd([FromBody] List<PaymentCollectionPlanDto> input)
    {
        int result = 0;
        if (input.Count > 0)
        {
            foreach (var item in input)
            {
                if (item.Id > 0)
                {
                    //编辑
                    //判断是否存在相同数据
                    if (!_paymentCollectionPlanService.HasHhisItem(item))
                    {
                        //不存在，添加
                        _paymentCollectionPlanService.Insert(item);
                        result++;

                    }
                    else
                    {
                        //存在，修改
                        _paymentCollectionPlanService.Update(item.Id.Value, item);
                        result++;
                    }
                }
                else
                {
                    //添加
                    _paymentCollectionPlanService.Insert(item);
                    result++;
                }
            }

        }
        return result == input.Count ? ResponseDto.Success("保存成功") : ResponseDto.Error(0, "保存异常");
    }
}
