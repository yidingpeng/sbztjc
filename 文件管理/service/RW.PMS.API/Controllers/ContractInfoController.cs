using Microsoft.AspNetCore.Mvc;
using RW.PMS.API.Controllers;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Input.System;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.Exceptions;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.Domain.Entities.Project;
using System.Web;


public class ContractInfoController : RWBaseController
{
    private readonly IContract_InfoService _contract_InfoService;
    private readonly IDictionaryService _dictionaryService;
    private readonly IPaymentCollectionPlanService _paymentCollectionPlanService;
    private readonly IContractInfoFilesService _contractInfoFilesService;
    private readonly IWebHostEnvironment _evn;
    private readonly IConfiguration _configuration;
    private readonly IProjectBasicsService _projectBasicsService;
    IClientCompanyService _clientCompanyService;
    IPayment_CollectionService _pament;
    IContractDetailService _cds;
    IProjectDeviceDetailsService _pddService;


    public ContractInfoController(IContract_InfoService contract_InfoService, IDictionaryService dictionaryService, IPaymentCollectionPlanService paymentCollectionPlanService,
        IWebHostEnvironment evn,IClientCompanyService clientCompanyService, IContractInfoFilesService contractInfoFilesService,
        IConfiguration configuration, IProjectBasicsService projectBasicsService, IPayment_CollectionService pament, IContractDetailService cds, IProjectDeviceDetailsService pddService)
    {
        _contract_InfoService = contract_InfoService;
        _dictionaryService = dictionaryService;
        _evn = evn;
        _configuration = configuration;
        _projectBasicsService = projectBasicsService;
        _contractInfoFilesService = contractInfoFilesService;
        _paymentCollectionPlanService = paymentCollectionPlanService;
        _clientCompanyService = clientCompanyService;
        _pament = pament;
        _cds = cds;
        _pddService = pddService;
    }

    [HttpGet("getContractMode")]
    public IResponseDto getContractMode()
    {
        var result = _dictionaryService.GetSubItemList("pay_mode");
        return ResponseDto.Success(result);
    }

    [HttpPost("GetContractPagedList")]
    public IResponseDto GetContractPagedList([FromQuery] Contract_InfoSearchDto input)
    {
        var list = _contract_InfoService.GetContractPagedList(input);
        //foreach (var item in result)
        //{
        //获取项目名称字符和项目编号字符
        //string ptNamesTxt = "";
        //string ptCodesTxt = "";
        //string[] ArrPt_Ids = item.pt_idsTxt.Split(",");
        //var proBaseList = _projectBasicsService.GetList();
        //foreach (var ProItem in proBaseList)
        //{
        //    foreach (var ArrId in ArrPt_Ids)
        //    {
        //        if (ProItem.Id == Int32.Parse(ArrId))
        //        {
        //            ptNamesTxt += ProItem.ProjectName + ',';
        //            ptCodesTxt += ProItem.ProjectCode + ',';
        //        }
        //    }
        //}
        //if (ptNamesTxt.Length>0)
        //{
        //    item.pt_NamesTxt = ptNamesTxt[0..^1];
        //    item.pt_codesTxt = ptCodesTxt[0..^1];
        //}
        //获取市场片区，客户名称
        //string marketA = "";
        //string comName = "";
        //List<int> marketAArr = new List<int>();
        //foreach (var ptitem in ArrPt_Ids)
        //{
        //    var ProInfo = _projectBasicsService.GetList().Where(t => t.Id == Int32.Parse(ptitem)).ToList();
        //    if (ProInfo.Any())
        //    {
        //        var comId = ProInfo.First().ClientCompany;
        //        var comInfo = _clientCompanyService.GetByIdList(comId).ToList();
        //        if (comInfo.Any())
        //        {
        //            if (comInfo.First().MarketArea > 0 && _clientCompanyService.GetByIdList(comId).Any())
        //            {
        //                marketA += _clientCompanyService.GetByIdList(comId).First().MarketAreaTxt + ',';
        //                marketAArr.Add(_clientCompanyService.GetByIdList(comId).First().MarketArea);
        //            }
        //            comName += _clientCompanyService.GetByIdList(comId).First().ClientName + ',';
        //        }
        //    }
        //}
        //item.MarketAreaTxt = marketA == "" ? "" : marketA[0..^1];
        //item.marketAreaArr = marketAArr.Any()? marketAArr.ToArray(): Array.Empty<int>();
        //item.ClientName = comName == "" ? "" : comName[0..^1];
        //}
        //List<Contract_InfoDto> newList = result.ToList();
        //if (input.pt_Name.NotNullOrWhiteSpace())
        //{
        //     newList= newList.Where(a => a.pt_NamesTxt.Contains(input.pt_Name)).ToList();
        //}
        //if (input.ClientName.NotNullOrWhiteSpace())
        //{
        //    newList = newList.Where(a => a.ClientName.Contains(input.ClientName)).ToList();
        //}
        //if (input.MarketArea > 0)
        //{
        //    newList = newList.Where(a => Array.IndexOf(a.marketAreaArr,input.MarketArea)!=-1).ToList();
        //}
        //var IEnumerableList = newList.Skip((input.PageNo - 1) * input.PageSize).Take(input.PageSize);
        ////把IEnumerable类型转换成list类型
        //var ResultList = new List<Contract_InfoDto>(IEnumerableList);
        //PagedResult<Contract_InfoDto> finalList = new PagedResult<Contract_InfoDto>(list: ResultList, total: ResultList.Count);
        return ResponseDto.Success(list);
    }


    [HttpPost("ContractAdd")]
    public IResponseDto ContractAdd([FromBody] Contract_InfoDto input)
    {
        var IsExist = _contract_InfoService.IsExist(input);
        if (IsExist)
        {
            return ResponseDto.Error(500, "该交合同编号已存在");
        }
        var entity = _contract_InfoService.Insert(input);
        var id = _contract_InfoService.GetList().ToList().OrderByDescending(a => a.Id).First().Id;
        UpdateContractPid(input.ConWordFilesId, id);
        UpdateContractPid(input.ConPdfFilesId, id);
        if (entity.Id > 0)
        {
            var targetId = entity.pt_idsTxt.Split(',').Select(t => t.To<int>()).ToArray();
            if (targetId.Length > 0)
            {
                var list = _pddService.GetIdsList(targetId);
                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        _cds.Insert(new ContractDetailDto
                        {
                            PID = entity.Id,
                            ProjectId = item.ProjectCode,
                            ProjectName = item.ProjectName,
                            EquipmentUnitPrice = item.DevicePrice,
                            EquipmentCount = item.Qty,
                            EquipmentTotalPrice = Math.Round(item.DevicePrice * item.Qty, 2),
                            DeliverDate = item.DeliverDate
                        });
                    }
                }
            }

        }
        //_paymentCollectionPlanService.UpdatePaymentCPlan(input.NewConPayPlanStr,id);
        return entity.Id > 0 ? ResponseDto.Success("添加成功") : ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
    }

    /// <summary>
    /// 添加新合同信息时，修改原先该信息所属文件pid为本id
    /// </summary>
    /// <param name="ConFilesId"></param>
    /// <param name="pId"></param>
    [HttpGet("UpdateContractPid")]
    public void UpdateContractPid(string ConFilesId,int? pId)
    {
        _contractInfoFilesService.UpdateImgPid(ConFilesId,pId);
    }

    [HttpPut]
    public IResponseDto ContractEdit([FromBody] Contract_InfoDto input)
    {
        if (input.Id.HasValue)
        {
            var IsExist = _contract_InfoService.IsExist(input);
            if (IsExist)
            {
                return ResponseDto.Error(500, "该交合同编号已存在");
            }
            var result = _contract_InfoService.Update(input.Id.Value, input);
            return result > 0 ? ResponseDto.Success("修改成功") : ResponseDto.Error(0, ExceptionCode.UpdateFailed.ToString());
        }
        return ResponseDto.Error(0, ExceptionCode.UpdateFailed.ToString());
    }

    [HttpDelete()]
    public IResponseDto ContractDelete([FromBody] DeleteRequesDto input)
    {
        var ids = input.GetIds();
        var result = _contract_InfoService.Delete(ids);
        return result > 0 ? ResponseDto.Success("删除成功") : ResponseDto.Error(0, ExceptionCode.DeleteFailed.ToString());
    }
 

    [HttpPost("InsertFile")]
    public IActionResult InsertFile([FromForm]int filepid, IFormFileCollection UploadFile)
    {
        string ids = "", paths = "";
        foreach (var item in UploadFile)
        {
            MultiFileDto uploadFile = new MultiFileDto();
            uploadFile.File = item;
            uploadFile.RootPath = _evn.ContentRootPath;
            uploadFile.Pid = filepid;
            var result= _contractInfoFilesService.Upload(uploadFile);
            ids  =ids + result.Id.ToString() + ',';
            paths  = paths + result.Path.ToString() + ',';
        }
        return new JsonResult(new { id = ids, Path = paths });
    }

    [HttpGet("UpdownFile")]
    public IActionResult UpdownFile(string fileUrl, int id)
    {
        string path = fileUrl; //路径
        if (System.IO.File.Exists(path)) //判断文件是否存在
        {
            // 读文件成二进制流
            //using (FileStream stream = new FileStream(path, FileMode.Open))
            //{
            //    long bufferLength = stream.Length;
            //    byte[] bufferFile = new byte[bufferLength];
            //    stream.Read(bufferFile, 0, bufferFile.Length);
            //    string contentType = "application/pdf";
            //    if (fileUrl.Split(".")[fileUrl.Split(".").Length - 1] != "pdf")
            //    {
            //        stream.Close();
            //        //返回下载文件流到前端
            //        return new FileStreamResult(new FileStream(fileUrl, FileMode.Open), "application/octet-stream") { FileDownloadName = fileName };
            //    }
            //    stream.Close();
            //    return new FileContentResult(bufferFile, contentType);
            //}
            var item = _contractInfoFilesService.GetFileShow(id);
            MemoryStream ms = new MemoryStream(item.Data);
            return File(ms, item.ContentType, item.Filename, enableRangeProcessing: true);
        }
        throw new ArgumentException("需要预览文件不存在！");
    }

    [HttpGet("GetInsertFilePath")]
    public string GetInsertFilePath()
    {
        var MyUrl = _configuration["FileUrl:MyUrl"];
        string url = MyUrl + "/ContractInfo/InsertFile";
        return url;
    }

    [HttpGet("GetDownloadFilePath")]
    public string GetDownloadFilePath()
    {
        var MyUrl = _configuration["FileUrl:MyUrl"];
        string url = MyUrl + "/ContractInfo/UpdownFile";
        return url;
    }

    [HttpGet("DeleteFilesByPid")]
    public IResponseDto DeleteFilesByPid(int Pid, int filesType)
    {
        var result= _contractInfoFilesService.updateFilesByPid(Pid,filesType);
        return result ? ResponseDto.Success("删除成功") : ResponseDto.Error(0, "删除失败");
    }

    [HttpGet("GetprojectList")]
    public IResponseDto GetprojectList()
    {
        var result = _projectBasicsService.GetList().Where(a=> a.IsDeleted==false);
        return ResponseDto.Success(result);
    }
    /// <summary>
    /// 根据ID获取合同信息
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetByIdList")]
    public IResponseDto GetByIdList(int Id)
    {
        var list = _contract_InfoService.GetByIdList(Id);
        return ResponseDto.Success(list);
    }

    /// <summary>
    /// 根据客户名称获取合同数据
    /// </summary>
    /// <param name="clietId"></param>
    /// <returns></returns>
    [HttpGet("GetListByClientId")]
    public IResponseDto GetListByClientId(int clietId)
    {
        List<Contract_InfoDto> QueryList = new List<Contract_InfoDto>();
        //var reslult = _contract_InfoService.GetListByClientId(clietId);
        var ptsInfo = _projectBasicsService.GetList().Where(t => t.ClientCompany == clietId).ToList();
        var conInfo = _contract_InfoService.GetAllList();
        foreach (var ptinfo in ptsInfo)
        {
            foreach (var con in conInfo)
            {
                string[] ptArr = con.pt_idsTxt.Split(",");
                for (int i=0;i<ptArr.Length;i++)
                {
                    if (Int32.Parse(ptArr[i])==ptinfo.Id)
                    {
                        QueryList.Add(con);
                    }
                }
            }
        }
        foreach (var item in QueryList)
        {
            string ptNamesTxt = "";
            string ptCodesTxt = "";
            string[] ArrPt_Ids = item.pt_idsTxt.Split(",");
            var proBaseList = _projectBasicsService.GetList();
            foreach (var ProItem in proBaseList)
            {
                foreach (var ArrId in ArrPt_Ids)
                {
                    if (ProItem.Id == Int32.Parse(ArrId))
                    {
                        ptNamesTxt += ProItem.ProjectName + ',';
                        ptCodesTxt += ProItem.ProjectCode + ',';
                    }
                }
            }
            if (ptNamesTxt.Length > 0)
            {
                item.pt_NamesTxt = ptNamesTxt[0..^1];
                item.pt_codesTxt = ptCodesTxt[0..^1];
            }
            //获取市场片区，客户名称
            string marketA = "";
            string comName = "";
            foreach (var ptitem in ArrPt_Ids)
            {
                var comId = _projectBasicsService.GetList().Where(t => t.Id == Int32.Parse(ptitem)).First().ClientCompany;
                if (comId > 0)
                {
                    marketA += _clientCompanyService.GetByIdList(comId).First().MarketAreaTxt + ',';
                    comName += _clientCompanyService.GetByIdList(comId).First().ClientName + ',';
                }
            }
            item.MarketAreaTxt = marketA == "" ? "" : marketA[0..^1];
            item.ClientName = comName == "" ? "" : comName[0..^1];
        }
        return ResponseDto.Success(QueryList);
    }

    /// <summary>
    /// 获取数据表中最新添加的内部合同编码
    /// </summary>
    /// <returns></returns>
    [HttpGet("GetTheLastCtCode")]
    public IActionResult GetTheLastCtCode()
    {
        var result = _contract_InfoService.GetTheLastCtCode();
        return new JsonResult(new { ctCode=result });
    }
    /// <summary>
    /// Id集合查询回款信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet("GetIdsList")]
    public IResponseDto GetIdsList(string Ids)
    {
        DeleteRequesDto dto = new()
        {
            Ids = Ids
        };
        var ids = dto.GetIds();
        var result = _pament.GetProIdsList(ids);
        return ResponseDto.Success(result);
    }

    /// <summary>
    /// PID查询合同子集信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet("GetByPIDList")]
    public IResponseDto GetByPIDList(int PID)
    {
        var result = _cds.GetByPIDList(PID);
        return ResponseDto.Success(result);
    }
    /// <summary>
    /// 合同子数据新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("ContractDetailAdd")]
    public IResponseDto ContractDetailAdd([FromBody] ContractDetailDto input)
    {
        var entity = _cds.Insert(input);
        //if (entity.Id > 0)
        //{
        //    //新增后需要增加金额到合同总金额中
        //    _contract_InfoService.EditHTPrice(entity.PID);
        //}
        return entity.Id > 0 ? ResponseDto.Success("添加成功") : ResponseDto.Error(0, ExceptionCode.InsertFailed.ToString());
    }
    /// <summary>
    /// 合同子数据修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut("ContractDetailEdit")]
    public IResponseDto ContractDetailEdit([FromBody] ContractDetailDto input)
    {
        if (input.Id.HasValue)
        {
            var result = _cds.Update(input.Id.Value, input);
            //if (result > 0)
            //{
            //    //新增后需要增加金额到合同总金额中
            //    _contract_InfoService.EditHTPrice(input.PID);
            //}
            return result > 0 ? ResponseDto.Success("修改成功") : ResponseDto.Error(0, ExceptionCode.UpdateFailed.ToString());
        }
        return ResponseDto.Error(0, ExceptionCode.UpdateFailed.ToString());
    }
    /// <summary>
    /// 合同项目信息删除
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpDelete("ContractDetailDelete")]
    public IResponseDto ContractDetailDelete([FromBody] DeleteRequesDto input)
    {
        var ids = input.GetIds();
        var result = _cds.Delete(ids);
        if (result > 0)
        {
            var entity =_cds.GetList().Where(o=>o.Id == ids[0]).FirstOrDefault();
            //if (entity != null)
            ////新增后需要增加金额到合同总金额中
            //_contract_InfoService.EditHTPrice(entity.PID);
        }
        return result > 0 ? ResponseDto.Success("删除成功") : ResponseDto.Error(0, ExceptionCode.DeleteFailed.ToString());
    }
}
