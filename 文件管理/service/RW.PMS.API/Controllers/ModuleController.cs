using Microsoft.AspNetCore.Mvc;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Module;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories.System;

namespace RW.PMS.API.Controllers;

public class ModuleController : RWBaseController
{
    private readonly IModuleService _moduleService;
    private readonly IOperationRepository _operationRepository;
    public ModuleController(IModuleService moduleService, IOperationRepository operationRepository)
    {
        _moduleService = moduleService;
        _operationRepository = operationRepository;
    }

    [HttpGet]
    public IResponseDto GetList()
    {
        var treeList = _moduleService.GetTreeList();
        return ResponseDto.Success(treeList);
    }

    [HttpGet("{id}/Operation")]
    public IResponseDto GetOperation(int id)
    {
        var list = _moduleService.GetOperation(id);
        return ResponseDto.Success(list);
    }

    [HttpPost]
    public IResponseDto DoAdd([FromBody] ModuleDetailDto input)
    {
        _moduleService.Insert(input);
        return ResponseDto.Success();
    }

    [HttpPut]
    public IResponseDto DoEdit([FromBody] ModuleDetailDto input)
    {
        if (input.Id.HasValue)
        {
            _moduleService.Update(input.Id.Value, input);
            return ResponseDto.Success();
        }
        return ResponseDto.Error(500, "ID不能为空！");
    }

    [HttpDelete("{id}")]
    public IResponseDto DoDelete(int id)
    {
        return ResponseDto.Success();
    }

    /// <summary>
    /// 批量修改全部
    /// </summary>
    [HttpPut("DoOperationEditAll")]
    public IResponseDto DoOperationEdit([FromBody] List<OperationEntity> listData)
    {
        if (listData[0].OperationCode.NotNullOrWhiteSpace())
        {
            for (int i = 0; i < listData.Count; i++)
            {
                for (int j = i + 1; j < listData.Count; j++)
                {
                    if (listData[i].OperationCode == listData[j].OperationCode)
                    {
                        return ResponseDto.Error(500, "功能编码已存在！");
                    }
                }
            }
            _operationRepository.Edit(listData);
            return ResponseDto.Success("修改成功");
        }
        return ResponseDto.Error(500, "功能编码不能为空！");
    }

    /// <summary>
    /// 修改单个数据
    /// </summary>
    [HttpPut("DoOperationEdit")]
    public IResponseDto DoOperationEditOne([FromBody] OperationEntity entity)
    {
        var result = _operationRepository.EditOne(entity);
        return ResponseDto.UpdateResult(result, "修改成功！", "未修改任何数据。");
    }

    [HttpPost("DoOperationAdd")]
    public IResponseDto DoOperationAdd([FromBody] OperationEntity input)
    {
        if (_operationRepository.Add(input))
        {
            return ResponseDto.Success("添加成功");
        }
        return ResponseDto.Error(500, "功能编码已存在！");
    }

    [HttpDelete("DoOperationDelete")]
    public IResponseDto DoOperationDelete(string OperationCode)
    {
        var a = _operationRepository.Delete(i => i.OperationCode.Equals(OperationCode));
        return a > 0 ? ResponseDto.Success("删除成功") : ResponseDto.Error(500, "删除失败！");
    }

    [HttpPut("Sort")]
    public IResponseDto DoSort(SortDto dto)
    {
        var result = _moduleService.UpdateSort(dto);
        return ResponseDto.UpdateResult(result);
    }
}