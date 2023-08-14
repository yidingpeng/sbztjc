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


public class ProjectAcceptanceController : RWBaseController
{
    private readonly IDictionaryService _dictionaryService;
    private readonly IConfiguration _configuration;
    private readonly IProjectAcceptanceService _projectAcceptanceService;
    private readonly IFileService _fileService;

    public ProjectAcceptanceController(IFileService fileService, IDictionaryService dictionaryService,IProjectAcceptanceService projectAcceptanceService,IConfiguration configuration)
    {
        _dictionaryService = dictionaryService;
        _configuration = configuration;
        _projectAcceptanceService = projectAcceptanceService;
        _fileService = fileService;
    }
    /// <summary>
    /// 验收类别下拉框数据
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetAcceptCategory")]
    public IResponseDto GetAcceptCategory()
    {
        var result = _dictionaryService.GetSubItemList("AcceptCategory");
        return ResponseDto.Success(result);
    }

    /// <summary>
    /// 验收结论下拉框数据
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetCheckAcceptCategory")]
    public IResponseDto GetCheckAcceptCategory()
    {
        var result = _dictionaryService.GetSubItemList("CheckAcceptCategory");
        return ResponseDto.Success(result);
    }

    /// <summary>
    /// 获取分页数据
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet("GetList")]
    public IResponseDto GetList([FromQuery] ProjectAcceptanceSearchDto input)
    {
        var result = _projectAcceptanceService.GetPagedList(input);
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
        var result = _projectAcceptanceService.GetByIdList(Id);
        return ResponseDto.Success(result);
    }

    /// <summary>
    /// 添加项目开票
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("DoAdd")]
    public IResponseDto DoAdd([FromBody] ProjectAcceptanceDto input)
    {
        var IsExist = _projectAcceptanceService.IsExist(input);
        if (IsExist)
        {
            return ResponseDto.Error(500, "该项目验收数据已存在");
        }
        _projectAcceptanceService.Insert(input);
        return ResponseDto.Success("添加成功！");
    }
    /// <summary>
    /// 修改项目开票
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut]
    public IResponseDto DoEdit([FromBody] ProjectAcceptanceDto input)
    {
        if (input.Id.HasValue)
        {
            var IsExist = _projectAcceptanceService.IsExist(input);
            if (IsExist)
            {
                return ResponseDto.Error(500, "该项目验收数据已存在");
            }
            var result = _projectAcceptanceService.Update(input.Id.Value, input);
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
        int count = _projectAcceptanceService.Delete(ids);
        return ResponseDto.Success($"成功删除了{count}条数据。");
    }

    /// <summary>
    /// 获取文件二进制流方法路径
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetFilePreviewPath")]
    public string GetFilePreviewPath()
    {
        var MyUrl = _configuration["FileUrl:MyUrl"];
        string url = MyUrl + "/File/GetFilePreview";
        return url;
    }

    /// <summary>
    /// 获取文件二进制流
    /// </summary>
    /// <param name="imagepath"></param>
    /// <returns></returns>
    [HttpGet("GetFilePreview")]
    public ActionResult GetFilePreview(string fileUrl,int id)
    {
        string path = fileUrl; //路径
        if (System.IO.File.Exists(path)) //判断文件是否存在
        {
            // 读文件成二进制流
            //using (FileStream stream = new FileStream(path, FileMode.Open,FileAccess.Read))
            //{
            //    long bufferLength = stream.Length;
            //    byte[] bufferFile = new byte[bufferLength];
            //    stream.Read(bufferFile, 0, bufferFile.Length);
            //    string contentType = "image/png";
            //    if (fileUrl.Split(".")[fileUrl.Split(".").Length - 1] == "pdf")
            //    {
            //        contentType = "application/pdf";
            //    }
            //    stream.Close();
            //    return new FileContentResult(bufferFile, contentType);
            //}
            var item = _fileService.GetFile2(id);
            MemoryStream ms = new MemoryStream(item.Data);
            return File(ms, item.ContentType, item.Filename, enableRangeProcessing: true);
        }
        throw new ArgumentException("需要预览文件不存在！");
    }

}
