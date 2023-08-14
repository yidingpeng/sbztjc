using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.Domain.Repositories;
using AutoMapper;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.Domain.Entities.System;
using System.Dynamic;
using RW.PMS.Domain.Entities.Probelem;
using RW.PMS.Application.Contracts.Probelm;
using RW.PMS.Application.Contracts.DTO.Problem;
using RW.PMS.Domain.Entities.Project;
using RW.PMS.Application.Contracts.Project;
using System.Collections;
using RW.PMS.Application.Contracts.Input.Message;
using RW.PMS.Application.Contracts.Message;
using RW.PMS.Application.Contracts.System;

namespace RW.PMS.Application.Problem
{
    public class ProblemFeedbackService : CrudApplicationService<ProblemfeedbackEntity, int>, IProblemFeedbackService
    {
        private readonly IProblemFeedbackdetailService _problemFeedbackdetailService;
        private readonly IProjectBasicsService _projectBasicsService;
        private readonly IThirdPartService _thirdPartService;
        private readonly IUserService _userService;
        private readonly IConfigService _configService;
        public ProblemFeedbackService(IDataValidatorProvider dataValidator, IProjectBasicsService projectBasicsService,
            IThirdPartService thirdPartService, IUserService userService, IConfigService configService,
           IRepository<ProblemfeedbackEntity, int> repository, IMapper mapper,
           Lazy<ICurrentUser> currentUser, IProblemFeedbackdetailService problemFeedbackdetailService) : base(
           dataValidator, repository, mapper, currentUser)
        {
            _problemFeedbackdetailService = problemFeedbackdetailService;
            _projectBasicsService = projectBasicsService;
            _thirdPartService = thirdPartService;
            _userService = userService;
            _configService = configService;
        }
        public PagedResult<ProblemFeedbackDto> GetPagedList(ProblemFeedbackSerchDto input)
        {
            string[] DateArray = input.FeedbackTime.NotNullOrWhiteSpace() ? input.FeedbackTime.Split("~") : null;
            var list = Repository.Select.From<DictionaryEntity, DictionaryEntity, ProjectBasicsEntity, UserEntity, UserEntity>(
                (p, d, d2, b, u, u2) => p
                .LeftJoin(p => p.ProblemTypeID == d.Id)
                .LeftJoin(p => p.DealWithDynamic == d2.DictionaryValue)
                .LeftJoin(p => p.pt_ID == b.Id)
                .LeftJoin(p => p.SolutionId == u.Id)
                .LeftJoin(p => p.UserId == u2.Id))
                .WhereIf(input.ProjectName.NotNullOrWhiteSpace(), t => t.t4.ProjectName.Contains(input.ProjectName) || t.t4.ProjectCode.Contains(input.ProjectName))
                .WhereIf(input.ProblemTypeID != 0, t => t.t1.ProblemTypeID == input.ProblemTypeID)
                .WhereIf(input.addressId != 0, t => t.t1.addressId == input.addressId)
                .WhereIf(input.DealWithDynamic.NotNullOrWhiteSpace(), t => t.t1.DealWithDynamic == input.DealWithDynamic)
                .WhereIf(input.SolutionId > 0, t => t.t1.SolutionId == input.SolutionId)
                .WhereIf(input.FeedbackTime.NotNullOrEmpty(), t => t.t1.FeedbackTime >= Convert.ToDateTime(DateArray[0].ToString()) && t.t1.FeedbackTime < Convert.ToDateTime(DateArray[1].ToString()).AddDays(1))
                .OrderByDescending(t => t.t1.Id)
                 .Count(out var total).Page(input.PageNo, input.PageSize)
                 .ToList((p, d, d2, b, u, u2) => new
                 {
                     pEntity = p, 
                     //问题类型
                     proType = d.DictionaryText,
                     ptName = b.ProjectName,
                     ptCode = b.ProjectCode,
                     soluN = u.Account,
                     //反馈人员
                     userN = u2.Account,
                     dealWithDynamic = d2.DictionaryText,
                 })
                 .Select(t =>
                 {
                     var output = Mapper.Map<ProblemFeedbackDto>(t.pEntity);
                     output.ProblemTypeText = t.proType;
                     output.ProjectName = t.ptName;
                     output.ProjectCode = t.ptCode;
                     output.SolutionName = t.soluN;
                     output.UserName = t.userN;
                     output.DealWithDynamicTxt = t.dealWithDynamic;
                     return output;
                 });

            var pList = _problemFeedbackdetailService.GetList().Where(a => a.IsDeleted == false);
            var ResultList = new List<ProblemFeedbackDto>(list);

            foreach (var pItem in pList)
            {
                foreach (var item in ResultList)
                {
                    if (item.FeedbackTime.GetValueOrDefault().ToString("yyyyMMdd") == "00010101")
                    {
                        item.FeedbackTime = null;
                    }
                    if (item.ActualSettlementDate.GetValueOrDefault().ToString("yyyyMMdd") == "00010101")
                    {
                        item.ActualSettlementDate = null;
                    }
                    if (item.EstimatedSettlementDate.GetValueOrDefault().ToString("yyyyMMdd") == "00010101")
                    {
                        item.EstimatedSettlementDate = null;
                    }
                    if (pItem.PID == item.Id)
                    {
                        item.PicId = new ExpandoObject();
                        item.PicId.value = pItem.Id;
                        item.PicId.imgUrl = pItem.RelativePath;
                    }
                }
            }
            foreach (var item in ResultList)
            {
                var ProjectInfo = _projectBasicsService.GetList().Where(t => t.Id == item.pt_ID).FirstOrDefault();
                if (ProjectInfo != null)
                {
                    var PprojectCode = _projectBasicsService.GetList().Where(t => t.Id == ProjectInfo.ParentId).FirstOrDefault();
                    if (PprojectCode != null)
                    {
                        item.PprojectCode = PprojectCode.ProjectCode;
                    }
                    //else
                    //{
                    //    item.PprojectCode = item.ProjectCode;
                    //    item.ProjectCode = null;
                    //}
                }
            }
            return new PagedResult<ProblemFeedbackDto>(ResultList, total);

        }
        /// <summary>
        /// 所有问题反馈信息集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ProblemFeedbackDto> GetAllList(ProblemFeedbackSerchDto input)
        {
            string[] DateArray = input.FeedbackTime.NotNullOrWhiteSpace() ? input.FeedbackTime.Split("~") : null;
            var list = Repository.Select.From<DictionaryEntity, DictionaryEntity, ProjectBasicsEntity, UserEntity, UserEntity>(
                (p, d, d2, b, u, u2) => p
                .LeftJoin(p => p.ProblemTypeID == d.Id)
                .LeftJoin(p => p.DealWithDynamic == d2.DictionaryValue)
                .LeftJoin(p => p.pt_ID == b.Id)
                .LeftJoin(p => p.SolutionId == u.Id)
                .LeftJoin(p => p.UserId == u2.Id))
                .WhereIf(input.ProjectName.NotNullOrWhiteSpace(), t => t.t4.ProjectName.Contains(input.ProjectName) || t.t4.ProjectCode.Contains(input.ProjectName))
                .WhereIf(input.ProblemTypeID != 0, t => t.t1.ProblemTypeID == input.ProblemTypeID)
                .WhereIf(input.addressId != 0, t => t.t1.addressId == input.addressId)
                .WhereIf(input.DealWithDynamic.NotNullOrWhiteSpace(), t => t.t1.DealWithDynamic == input.DealWithDynamic)
                .WhereIf(input.SolutionId > 0, t => t.t1.SolutionId == input.SolutionId)
                .WhereIf(input.FeedbackTime.NotNullOrEmpty(), t => t.t1.FeedbackTime >= Convert.ToDateTime(DateArray[0].ToString()) && t.t1.FeedbackTime < Convert.ToDateTime(DateArray[1].ToString()).AddDays(1))
                .OrderByDescending(t => t.t1.Id)
                .ToList((p, d, d2, b, u, u2) => new
                {
                    pEntity = p,
                    //问题类型
                    proType = d.DictionaryText,
                    ptName = b.ProjectName,
                    ptCode = b.ProjectCode,
                    soluN = u.Account,
                    //反馈人员
                    userN = u2.Account,
                    dealWithDynamic = d2.DictionaryText,
                })
                 .Select(t =>
                 {
                     var output = Mapper.Map<ProblemFeedbackDto>(t.pEntity);
                     output.ProblemTypeText = t.proType;
                     output.ProjectName = t.ptName;
                     output.ProjectCode = t.ptCode;
                     output.SolutionName = t.soluN;
                     output.UserName = t.userN;
                     output.DealWithDynamicTxt = t.dealWithDynamic;
                     return output;
                 });
            return new List<ProblemFeedbackDto>(list);

        }

        public List<ProblemFeedbackDto> GetAllList(string key, int PageNo, int PageSize)
        {
            var list = Repository.Select.From<DictionaryEntity, DictionaryEntity, ProjectBasicsEntity, UserEntity, UserEntity>(
                (p, d, d2, b, u, u2) => p
                .LeftJoin(p => p.ProblemTypeID == d.Id)
                .LeftJoin(p => p.DealWithDynamic == d2.DictionaryValue)
                .LeftJoin(p => p.pt_ID == b.Id)
                .LeftJoin(p => p.SolutionId == u.Id)
                .LeftJoin(p => p.UserId == u2.Id))
                .WhereIf(key.NotNullOrWhiteSpace(), t => t.t4.ProjectName.Contains(key) || t.t4.ProjectCode.Contains(key))
                .OrderByDescending(t => t.t1.Id).Page(PageNo, PageSize)
                 .ToList((p, d, d2, b, u, u2) => new
                 {
                     pEntity = p,
                     //问题类型
                     proType = d.DictionaryText,
                     ptName = b.ProjectName,
                     ptCode = b.ProjectCode,
                     soluN = u.Account,
                     //反馈人员
                     userN = u2.Account,
                     dealWithDynamic = d2.DictionaryText,
                 })
                 .Select(t =>
                 {
                     var output = Mapper.Map<ProblemFeedbackDto>(t.pEntity);
                     output.ProblemTypeText = t.proType;
                     output.ProjectName = t.ptName;
                     output.ProjectCode = t.ptCode;
                     output.SolutionName = t.soluN;
                     output.UserName = t.userN;
                     output.DealWithDynamicTxt = t.dealWithDynamic;

                     return output;
                 });

            var pList = _problemFeedbackdetailService.GetList().Where(a => a.IsDeleted == false);
            var ResultList = new List<ProblemFeedbackDto>(list);

            foreach (var pItem in pList)
            {
                foreach (var item in ResultList)
                {
                    if (item.FeedbackTime.GetValueOrDefault().ToString("yyyyMMdd") == "00010101")
                    {
                        item.FeedbackTime = null;
                    }
                    if (item.ActualSettlementDate.GetValueOrDefault().ToString("yyyyMMdd") == "00010101")
                    {
                        item.ActualSettlementDate = null;
                    }
                    if (item.EstimatedSettlementDate.GetValueOrDefault().ToString("yyyyMMdd") == "00010101")
                    {
                        item.EstimatedSettlementDate = null;
                    }
                }
            }
            return new List<ProblemFeedbackDto>(ResultList);

        }

        public List<ProblemFeedbackDto> GetAllListById(int id)
        {
            var list = Repository.Select.From<DictionaryEntity, DictionaryEntity, ProjectBasicsEntity, UserEntity, UserEntity>(
                (p, d, d2, b, u, u2) => p
                .LeftJoin(p => p.ProblemTypeID == d.Id)
                .LeftJoin(p => p.DealWithDynamic == d2.DictionaryValue)
                .LeftJoin(p => p.pt_ID == b.Id)
                .LeftJoin(p => p.SolutionId == u.Id)
                .LeftJoin(p => p.UserId == u2.Id))
                .WhereIf(id > 0, t => t.t1.Id == id)
                .OrderByDescending(t => t.t1.Id)
                 .ToList((p, d, d2, b, u, u2) => new
                 {
                     pEntity = p,
                     //问题类型
                     proType = d.DictionaryText,
                     ptName = b.ProjectName,
                     ptCode = b.ProjectCode,
                     soluN = u.Account,
                     //反馈人员
                     userN = u2.Account,
                     dealWithDynamic = d2.DictionaryText,
                 })
                 .Select(t =>
                 {
                     var output = Mapper.Map<ProblemFeedbackDto>(t.pEntity);
                     output.ProblemTypeText = t.proType;
                     output.ProjectName = t.ptName;
                     output.ProjectCode = t.ptCode;
                     output.SolutionName = t.soluN;
                     output.UserName = t.userN;
                     output.DealWithDynamicTxt = t.dealWithDynamic;
                     return output;
                 });
            return new List<ProblemFeedbackDto>(list);
        }
        /// <summary>
        /// 根据ID获取问题反馈信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ProblemFeedbackDto GetProblemFeedbackInfo(int Id)
        {
            var list = Repository.Select.From<DictionaryEntity, DictionaryEntity, ProjectBasicsEntity, UserEntity, UserEntity>(
                (p, d, d2, b, u, u2) => p
                .LeftJoin(p => p.ProblemTypeID == d.Id)
                .LeftJoin(p => p.DealWithDynamic == d2.DictionaryValue)
                .LeftJoin(p => p.pt_ID == b.Id)
                .LeftJoin(p => p.SolutionId == u.Id)
                .LeftJoin(p => p.UserId == u2.Id))
                .Where(t => t.t1.Id == Id)
                .OrderByDescending(t => t.t1.Id)
                .ToList((p, d, d2, b, u, u2) => new
                {
                    pEntity = p,
                    //问题类型
                    proType = d.DictionaryText,
                    ptName = b.ProjectName,
                    ptCode = b.ProjectCode,
                    soluN = u.Account,
                    //反馈人员
                    userN = u2.Account,
                    dealWithDynamic = d2.DictionaryText,
                })
                 .Select(t =>
                 {
                     var output = Mapper.Map<ProblemFeedbackDto>(t.pEntity);
                     output.ProblemTypeText = t.proType;
                     output.ProjectName = t.ptName;
                     output.ProjectCode = t.ptCode;
                     output.SolutionName = t.soluN;
                     output.UserName = t.userN;
                     output.DealWithDynamicTxt = t.dealWithDynamic;
                     return output;
                 }).FirstOrDefault();
            return list;
        }
        public bool IsExist(ProblemFeedbackDto input)
        {
            var exist = Repository.Where(t => t.pt_ID == input.pt_ID && t.ProblemTypeID == input.ProblemTypeID).ToOne();
            if (exist == null) return false;
            if (input.Id.HasValue)
            {
                return input.Id.Value != exist.Id;
            }
            return true;
        }

        /// <summary>
        /// 编辑问题反馈状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool UpdateProState(int Id, string proState)
        {
            Repository.Orm.Update<ProblemfeedbackEntity>(Id).Set(a => a.DealWithDynamic, proState).ExecuteAffrows();
            return true;
        }
        /// <summary>
        /// 发送消息给接收人员进行问题处理
        /// </summary>
        /// <param name="proId"></param>
        /// <param name="userId"></param>
        /// <param name="esburl"></param>
        /// <returns></returns>
        public bool MessagePush(int proId, int userId, string esburl)
        {
            try
            {
                var ProInfo = _projectBasicsService.GetListById(proId).FirstOrDefault();
                var userTel = _userService.GetUserTelById(userId).Telnum;
                if (userTel.NotNullOrWhiteSpace() && ProInfo != null)
                {
                    ArrayList myArrayList = new ArrayList();
                    myArrayList.Add(new { type = "user", id = userTel });
                    var data = new ESBSendMessageInput
                    {
                        Title = "您有一条关于" + ProInfo.ProjectName + "(" + ProInfo.ProjectCode + ")项目的问题反馈任务,请及时进行处理。",
                        Type = 2,
                        Desc = "",
                        Link = "/problem/problemfeedback",
                        From = "PLM",
                        TargetPlantform = "OA",
                        Targets = myArrayList.ToArray(),
                        SendMode = 0,
                        SendModeValue = ""
                    };
                    _thirdPartService.PostRequestAsync(esburl, data);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }
        /// <summary>
        /// 发送消息给责任判定通知人
        /// </summary>
        /// <param name="proId"></param>
        /// <param name="esburl"></param>
        /// <returns></returns>
        public bool MessagePushPD(int proId, string esburl)
        {
            try
            {
                ArrayList myArrayList = new ArrayList();
                //获取责任判定通知人
                var PDR = _configService.GetConfigCode("Judging");
                string[] strArray = PDR.Value.Split(',');//字符串转数组
                if (strArray.Length > 0)
                {
                    foreach (var item in strArray)
                    {
                        var tel = _userService.GetUserTelByAccount(item);
                        if (tel != null)
                        {
                            myArrayList.Add(new { type = "user", id = tel.Telnum });
                        }
                    }
                    //根据项目ID获取项目信息
                    var ProInfo = _projectBasicsService.GetListById(proId).FirstOrDefault();
                    var data = new ESBSendMessageInput
                    {
                        Title = "您有一条关于" + ProInfo.ProjectName + "(" + ProInfo.ProjectCode + ")项目的责任判定任务,请及时进行处理。",
                        Type = 2,
                        Desc = "",
                        Link = "/problem/problemfeedback",
                        From = "PLM",
                        TargetPlantform = "OA",
                        Targets = myArrayList.ToArray(),
                        SendMode = 0,
                        SendModeValue = ""
                    };
                    _thirdPartService.PostRequestAsync(esburl, data);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 发送消息给质量检验通知人
        /// </summary>
        /// <param name="proId"></param>
        /// <param name="esburl"></param>
        /// <returns></returns>
        public bool MessagePushJY(int proId, string esburl)
        {
            try
            {
                ArrayList myArrayList = new ArrayList();
                //获取质量检验通知人账号
                var PDR = _configService.GetConfigCode("ExperimentResult");
                string[] strArray = PDR.Value.Split(',');//字符串转数组
                if (strArray.Length > 0)
                {
                    foreach (var item in strArray)
                    {
                        var tel = _userService.GetUserTelByAccount(item);
                        if (tel != null)
                        {
                            myArrayList.Add(new { type = "user", id = tel.Telnum });
                        }
                    }
                    //根据项目ID获取项目信息
                    var ProInfo = _projectBasicsService.GetListById(proId).FirstOrDefault();
                    var data = new ESBSendMessageInput
                    {
                        Title = "您有一条关于" + ProInfo.ProjectName + "(" + ProInfo.ProjectCode + ")项目的质量检验任务,请及时进行处理。",
                        Type = 2,
                        Desc = "",
                        Link = "/problem/problemfeedback",
                        From = "PLM",
                        TargetPlantform = "OA",
                        Targets = myArrayList.ToArray(),
                        SendMode = 0,
                        SendModeValue = ""
                    };
                    _thirdPartService.PostRequestAsync(esburl, data);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
