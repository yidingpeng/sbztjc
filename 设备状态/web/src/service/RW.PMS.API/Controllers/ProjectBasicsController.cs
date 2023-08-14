using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.Security.User;

namespace RW.PMS.API.Controllers
{
    public class ProjectBasicsController : RWBaseController
    {
        IProjectBasicsService _projectBasicsService;
        IDictionaryService _dictionaryService;
        IClientCompanyService _clientCompanyService;
        IContract_InfoService _contract_InfoService;
        IProjectDynamicService _projectDynamicService;
        IProjectContactsService _projectContactsService;
        IUserService _user;
        IConfiguration _configuration;
        IProjectBasicsFilesService _projectBasicsFilesService;
        public readonly ICurrentUser _CurrentUser;
        private readonly IWebHostEnvironment _evn;

        public ProjectBasicsController(IProjectBasicsService projectBasicsService, IDictionaryService dictionaryService,
            IWebHostEnvironment evn, IProjectContactsService projectContactsService, ICurrentUser CurrentUser, IProjectBasicsFilesService projectBasicsFilesService
            , IClientCompanyService clientCompanyService, IContract_InfoService contract_InfoService, IProjectDynamicService projectDynamicService, IUserService user, IConfiguration configuration)
        {
            _projectBasicsService = projectBasicsService;
            _dictionaryService = dictionaryService;
            _clientCompanyService = clientCompanyService;
            _contract_InfoService = contract_InfoService;
            _projectDynamicService= projectDynamicService;
            _projectContactsService = projectContactsService;
            _user = user;
            _evn = evn;
            _configuration = configuration;
            _CurrentUser = CurrentUser;
            _projectBasicsFilesService = projectBasicsFilesService;
        }
        /// <summary>
        /// 项目基础信息分页
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("List")]
        public IResponseDto GetList([FromQuery] ProjectBasicsSearchDto search)
        {
            var result = _projectBasicsService.PagedList(search);
            return ResponseDto.Success(result);
        }
        /// <summary>
        /// 导出项目数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("ExportProList")]
        public IResponseDto ExportProList([FromQuery] ProjectBasicsSearchDto search)
        {
            List<ProjectBasicsView> listBook = _projectBasicsService.ProjectListDerive(search);
            return ResponseDto.Success(listBook);
        }
        /// <summary>
        /// 项目基础信息
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("AllList")]
        public IResponseDto GetAllList(string? key, int PageNo, int PageSize)
        {
            var result = _projectBasicsService.GetAllProject(key, PageNo, PageSize);
            return ResponseDto.Success(result);
        }
        /// <summary>
        ///根据父Id查询子项目信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("ParentIdList")]
        public IResponseDto GetByParentIdList(int Id)
        {
            var result = _projectBasicsService.GetByParentIdList(Id); 
            return ResponseDto.Success(result);
        }
        /// <summary>
        ///根据Id查询项目信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetListById")]
        public IResponseDto GetListById(int Id)
        {
            var result = _projectBasicsService.GetListById(Id);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public IResponseDto DoAdd([FromBody] ProjectBasicsListDto input)
        {
            bool counts = _projectBasicsService.ProOnly(input.ProjectCode, input.Id);//验重
            if (counts) return ResponseDto.Error(-1, "存在重复的项目编号！");
            _projectBasicsService.Insert(input);
            string userName = "",
                dicText = "";
            var ProData = _projectBasicsService.GetList().Where(o => o.ProjectCode == input.ProjectCode && o.IsDeleted == false).FirstOrDefault();
            if (ProData !=null)
            {
                var userData = _user.GetList().Where(o => o.Id == ProData.CreatedBy && o.IsDeleted == false).FirstOrDefault();
                if (userData !=null)
                {
                    userName = userData.RealName;
                }
                var dicData = _dictionaryService.GetList().Where(o => o.IsDeleted == false && o.Id == input.ProjectStatus).FirstOrDefault();
                if (dicData !=null)
                {
                    dicText = dicData.DictionaryText;
                }
                ProjectDynamicView entity = new ProjectDynamicView();
                entity.operationContent = "(新建项目)" + userName + "新建了此项目，项目编号是：" + input.ProjectCode + "、项目状态是：" + dicText;
                entity.dyType = 219;
                entity.projectID = ProData.Id;
                _projectDynamicService.Insert(entity);
            }
            return ResponseDto.Success("添加成功！");
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        public IResponseDto DoEdit([FromBody] ProjectBasicsListDto input)
        {
            if (input.Id <= 0) return ResponseDto.Error(500, "ID不能为空！");
            bool counts = _projectBasicsService.ProOnly(input.ProjectCode, input.Id);//验重
            if (counts) return ResponseDto.Error(-1, "存在重复的项目编号！");
            _projectBasicsService.Update(input);

            return ResponseDto.Success("修改成功！");
        }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpDelete]
        public IResponseDto DoDelete([FromBody] DeleteRequesDto model)
        {
            var ids = model.GetIds();
            if (ids.Length == 0) return ResponseDto.Error(500, "未指定删除的ID。");
            //如果存在项目上的问题反馈，则不能删除项目信息
            bool prob = _projectBasicsService.ProblemInfo(ids);
            if (prob) return ResponseDto.Error(500, "该项目存在问题反馈信息，无法删除！");
            int count = _projectBasicsService.Delete(ids);
            foreach (var item in ids)
            {
                DeleteChiledren(item);
            }
            return ResponseDto.Success($"成功删除了{count}条数据。");
        }
        /// <summary>
        /// 删除父项目下的所有子项目
        /// </summary>
        /// <param name="Id"></param>
        [HttpGet("DeleteChiledren")]
        public void DeleteChiledren(int Id)
        {
            var childrenId = _projectBasicsService.GetByParentIdList(Id);
            if (childrenId.Any())
            {
                foreach (var CItem in childrenId)
                {
                    if (CItem.HasChildren == true)
                    {
                        DeleteChiledren(CItem.Id);
                    }
                    _projectBasicsService.Delete(CItem.Id);
                }
            }
        }

        /// <summary>
        /// 获取字典项目分类
        /// </summary>
        /// <returns></returns>
        [HttpGet("Class")]
        public IResponseDto ProjectClass()
        {
            var list = _dictionaryService.GetSubItemList("ProjectClass");
            List<BaseCommonOutput> result = new List<BaseCommonOutput>();
            for (int i = 0; i < list.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    label = list[i].DictionaryText,
                    value = list[i].Id
                };
                result.Add(baseCommonOutput);
            }
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 获取字典项目业务类型
        /// </summary>
        /// <returns></returns>
        [HttpGet("Type")]
        public IResponseDto Businesstype()
        {
            var list = _dictionaryService.GetSubItemList("BusinessType");
            List<BaseCommonOutput> result = new List<BaseCommonOutput>();
            for (int i = 0; i < list.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    label = list[i].DictionaryText,
                    value = list[i].Id
                };
                result.Add(baseCommonOutput);
            }
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 获取字典重要等级
        /// </summary>
        /// <returns></returns>
        [HttpGet("Grade")]
        public IResponseDto UrgentGrade()
        {
            var list = _dictionaryService.GetSubItemList("UrgentGrade");
            List<BaseCommonOutput> result = new List<BaseCommonOutput>();
            for (int i = 0; i < list.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    label = list[i].DictionaryText,
                    value = Convert.ToInt32(list[i].DictionaryValue)
                };
                result.Add(baseCommonOutput);
            }
            return ResponseDto.Success(result);
        }
        /// <summary>
        /// 获取字典项目管理方式
        /// </summary>
        /// <returns></returns>
        [HttpGet("Style")]
        public IResponseDto ProManagementStyle()
        {
            var list = _dictionaryService.GetSubItemList("ProManagementStyle");
            List<BaseCommonOutput> result = new List<BaseCommonOutput>();
            for (int i = 0; i < list.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    label = list[i].DictionaryText,
                    value = list[i].Id
                };
                result.Add(baseCommonOutput);
            }
            return ResponseDto.Success(result);
        }
        /// <summary>
        /// 获取字典项目当前状态
        /// </summary>
        /// <returns></returns>
        [HttpGet("State")]
        public IResponseDto ProState()
        {
            var list = _dictionaryService.GetSubItemList("ProState");
            List<BaseCommonOutput> result = new List<BaseCommonOutput>();
            for (int i = 0; i < list.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    label = list[i].DictionaryText,
                    value = list[i].Id
                };
                result.Add(baseCommonOutput);
            }
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 获取字典项目状态
        /// </summary>
        /// <returns></returns>
        [HttpGet("ProjectStatus")]
        public IResponseDto ProjectStatus()
        {
            var list = _dictionaryService.GetSubItemList("ProjectStatus");
            List<BaseCommonOutput> result = new List<BaseCommonOutput>();
            for (int i = 0; i < list.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    label = list[i].DictionaryText,
                    value = list[i].Id
                };
                result.Add(baseCommonOutput);
            }
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 客户公司信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("Client")]
        public IResponseDto ClientCompany()
        {
            var pagedList = _clientCompanyService.GetList();
            List<BaseCommonOutput> result = new List<BaseCommonOutput>();
            for (int i = 0; i < pagedList.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    label = pagedList[i].ClientName,
                    value = pagedList[i].Id
                };
                result.Add(baseCommonOutput);
            }
            return ResponseDto.Success(result);
        }
        /// <summary>
        /// 项目合同信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("ContractList")]
        public IResponseDto ContractList(string name)
        {
            var pagedList = _contract_InfoService.GetList().Where(a=>a.contractName.Contains(name) ||a.ct_code.Contains(name)).ToList();
            List<BaseCommonOutput> result = new List<BaseCommonOutput>();
            for (int i = 0; i < pagedList.Count; i++)
            {
                BaseCommonOutput baseCommonOutput = new BaseCommonOutput()
                {
                    label = pagedList[i].contractName,
                    name = pagedList[i].ct_code,
                    value = pagedList[i].Id
                };
                result.Add(baseCommonOutput);
            }
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 树形项目信息数据集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("TreeList")]
        public IResponseDto ProBasicsTreeList(string? ProjectKey)
        {
            var treeList = _projectBasicsService.GetTreeList(ProjectKey);
            return ResponseDto.Success(treeList);
        }

        /// <summary>
        /// 树形项目信息数据集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("TreeListTabel")]
        public IResponseDto ProBasicsTreeListTable(string? ProjectKey, string? pt_idsTxt, int PageNo, int PageSize)
        {
            var treeList = _projectBasicsService.GetTreeListTable(ProjectKey,pt_idsTxt,PageNo,PageSize);
            var IEnumerableList = treeList.Skip((PageNo - 1) * PageSize).Take(PageSize);
            var ResultList = new List<ProjectBasicsView>(IEnumerableList);
            foreach (var item in ResultList)
            {
                AddShowInfo(item);
            }
            PagedResult<ProjectBasicsView> finalList = new PagedResult<ProjectBasicsView>(list: ResultList, total: treeList.Count);
            return ResponseDto.Success(finalList);
        }

        /// <summary>
        /// 树形列表获取市场片区、客户名称、营销负责人
        /// </summary>
        /// <param name="item"></param>
        [HttpGet("AddShowInfo")]
        public void AddShowInfo(ProjectBasicsView item)
        {
            if (item.Children != null)
            {
                foreach (var childItem in item.Children)
                {
                    AddShowInfo(childItem);
                }
            }
            var comId = item.ClientCompany;
            var DataInfo = _clientCompanyService.GetByIdList(comId).FirstOrDefault();
            item.MarketAreaTxt = DataInfo == null ? "" : DataInfo.MarketAreaTxt;
            item.ClientCompanyName = DataInfo == null ? "" : DataInfo.ClientName;
            var PersonInChargeList = _projectContactsService.GetAllList();
            foreach (var chargeItem in PersonInChargeList)
            {
                if (item.Id == chargeItem.PID && chargeItem.FZBKName == "营销经理")
                {
                    item.PersonInChargeName = chargeItem.ContactsName;
                    break;
                }
                else
                    item.PersonInChargeName = "";
            }
        }

        /// <summary>
        /// 树形项目信息数据集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("InfoList")]
        public IResponseDto ProBasicsList()
        {
            var List = _projectBasicsService.GetList().Where(s => s.ParentId == 0).ToList();
            return ResponseDto.Success(List);
        }

        /// <summary>
        /// 根据客户公司和项目类别查询数据
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="proClass"></param>
        /// <returns></returns>
        [HttpGet("GetListByClientIdAndClass")]

        public IResponseDto GetListByClientIdAndClass(int clientId, string proClassTxt)
        {
            var result = _projectBasicsService.GetListByClientIdAndClass(clientId, proClassTxt);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 项目基础信息分页
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("GetSellBasicsList")]
        public IResponseDto GetSellBasicsList([FromQuery] SellBasicsDataSearchDto search)
        {
            var result = _projectBasicsService.SellBasicsPagedList(search);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 营销销售数据基础信息子项目信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetParentSellBasicsList")]
        public IResponseDto GetParentSellBasicsList(int Id)
        {
            var result = _projectBasicsService.ParentSellBasicsPagedList(Id);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 获取所有一级项目信息
        /// </summary>
        /// <param name="ProjectName"></param>
        /// <param name="ProjectCode"></param>
        /// <returns></returns>
        [HttpGet("GetAllLevel1List")]
        public IResponseDto GetAllLevel1List(string ProjectName, string ProjectCode)
        {
            var result = _projectBasicsService.GetListByIds(ProjectName,ProjectCode);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 获取所有项目信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllProList")]
        public IResponseDto GetAllProList(string? key, int PageNo, int PageSize)
        {
            var result = _projectBasicsService.GetAllList(key,PageNo,PageSize);
            return ResponseDto.Success(result);
        }

        /// <summary>
        /// 编辑项目状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("UpdateProState")]
        public IResponseDto UpdateProState(int Id, int ProjectStatus,int proState)
        {
            bool counts = _projectBasicsService.UpdateProState(Id, ProjectStatus,proState);//修改
            string content = "（项目状态变更）" + _CurrentUser?.RealName + "变更了项目编号为"+
            _projectBasicsService.GetList().Where(t => t.Id == Id).First().ProjectCode + "的项目动态，项目状态变更为：" +
            _dictionaryService.GetList().Where(t => t.Id == ProjectStatus).First().DictionaryText + "，项目阶段变更为：" +
            _dictionaryService.GetList().Where(t => t.Id == proState).First().DictionaryText;
            _projectDynamicService.Insert(new ProjectDynamicView { dyType = 219, projectID = Id, operationContent = content });
            if (!counts) return ResponseDto.Error(-1, "更新状态异常");
            return ResponseDto.Success("更新成功");
        }

        /// <summary>
        /// 编辑项目备注信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("UpdateProRemark")]
        public IResponseDto UpdateProRemark(int Id, string Remark)
        {
            bool counts = _projectBasicsService.UpdateProRemark(Id, Remark);//修改
            if (!counts) return ResponseDto.Error(-1, "更新备注异常");
            return ResponseDto.Success("更新成功");
        }
        /// <summary>
        /// 获取所有项目信息-APP使用
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllList")]
        public IResponseDto GetAllList(string? key)
        {
            var result = _projectBasicsService.GetAllList(key);
            return ResponseDto.Success(result);
        }
        /// <summary>
        /// 项目团队消息推送
        /// </summary>
        /// <returns></returns>
        [HttpPost("TeamNewsFeed")]
        public IResponseDto TeamNewsFeed()
        {
            string esburl = _configuration["ESBUrl:key"] + "/sendMessage";
            string plmurl = _configuration["PLMUrl:key"];
            var result = _projectBasicsService.MessagePush(plmurl,esburl);
            if (!result) return ResponseDto.Error(-1, "发送失败");
            return ResponseDto.Success("发送成功");
        }

        [HttpGet("GetInsertFilePath")]
        public string GetInsertFilePath()
        {
            var MyUrl = _configuration["FileUrl:MyUrl"];
            string url = MyUrl + "/ProjectBasics/InsertFile";
            return url;
        }

        [HttpPost("InsertFile")]
        public IActionResult InsertFile([FromForm] int filepid, IFormFileCollection UploadFile)
        {
            MultiFileDto uploadFile = new();
            uploadFile.File = UploadFile[0];
            uploadFile.RootPath = _evn.ContentRootPath;
            uploadFile.Pid = filepid;
            var result = _projectBasicsFilesService.Upload(uploadFile);
            return new JsonResult(new { id = result.Id, Path = result.Path });
        }

        [HttpGet("GetFile")]
        public IActionResult GetFile(int Pid)
        {
            var item = _projectBasicsFilesService.GetFileShow(Pid);
            MemoryStream ms = new MemoryStream(item.Data);
            return File(ms, item.ContentType, item.Filename, enableRangeProcessing: true);
        }

        /// <summary>
        /// 根据项目编号获取项目信息
        /// </summary>
        /// <param name="proCode"></param>
        /// <returns></returns>
        [HttpGet("getDataByCode")]
        public IResponseDto getDataByCode(string proCode)
        {
            var data = _projectBasicsService.GetList().Where(t => t.ProjectCode == proCode)?.First()??null;
            return ResponseDto.Success(data) ;
        }
    }
}
