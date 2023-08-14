using AutoMapper;
using RW.PMS.Application.Contracts.BOM;
using RW.PMS.Application.Contracts.BOM.DTO;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.Message;
using RW.PMS.Application.Contracts.System;
using RW.PMS.Application.Message;
using RW.PMS.Application.System;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Exceptions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.BOM;
using RW.PMS.Domain.Entities.Material;
using RW.PMS.Domain.Entities.Purchase;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.BOM
{
    public class BOMService : CrudApplicationService<BomTableEntity, int>, IBOMService
    {
        IModuleService moduleService;
        IESBMessageService esbMessageService;
        public BOMService(IDataValidatorProvider dataValidator,
                          IRepository<BomTableEntity, int> repository,
                          IMapper mapper,
                          Lazy<ICurrentUser> currentUser,
                          IModuleService moduleService,
                          IESBMessageService esbMessageService
                          ) : base(dataValidator, repository, mapper, currentUser)
        {
            this.moduleService = moduleService;
            this.esbMessageService = esbMessageService;
        }

        public bool Approve(CommentResult comment)
        {
            var uname = CurrentUser.Value.RealName;
            var model = this.Repository.Where(x => x.Id == comment.BomId).First();
            var beforeNode = model.Status;
            if (beforeNode != comment.Node)
                throw new LogicException("状态错误，请刷新后重试。");
            model.Audit(comment.Result);
            this.Repository.Update(model);

            //给发起人通知
            var commentResult = comment.Result ? "通过" : "拒绝";
            //还是考虑异步发送消息
            esbMessageService.SendToUser("/BOM/detail?id=" + comment.BomId, $"您的BOM表已被[{uname}]审批{commentResult}：{comment.Comment}", model.CreateBy);
            //给审批人发送通知
            SendAuditMessage(model);

            //已完成,推送到PLM中
            if (model.IsCompleted())
            {
                this.Repository.Orm.Insert(TableToPrimary(model)).ExecuteAffrows();
                var items = this.Repository.Orm
                    .Select<BomItemEntity, pdm_material>()
                    .Where((a, b) => a.BomId == model.Id && a.Code == b.Code)
                    .ToList((a, b) => new { Item = a, Material = b });
                var plmItems = items.Select(x => ItemToPLMItem(x.Item, x.Material)).ToList();
                this.Repository.Orm.Insert(plmItems).ExecuteAffrows();
            }

            this.Repository.Orm.Insert(new BOMApprovalLogEntity
            {
                ApprovedTime = DateTime.Now,
                BomId = comment.BomId,
                Node = beforeNode,
                Result = comment.Result,
                User = uname,
                Desc = comment.Comment,
            }).ExecuteAffrows();

            return true;
        }

        void SendAuditMessage(BomTableEntity table)
        {
            string title = "";
            string user = "";
            switch (table.Status)
            {
                case BOMStatusDesc.Auditing:
                    title = $"请审核BOM表[{table.ProjectCode}{table.BOMName}]";
                    user = table.AuditBy;
                    break;
                case BOMStatusDesc.Purchasing:
                    title = $"请审核BOM表[{table.ProjectCode}{table.BOMName}]";
                    user = table.PurchaseBy;
                    break;
                case BOMStatusDesc.Approving:
                    title = $"请审批BOM表[{table.ProjectCode}{table.BOMName}]";
                    user = table.ApprovalBy;
                    break;
                case BOMStatusDesc.Completed:
                    title = $"您的BOM表[{table.ProjectCode}{table.BOMName}]审批完成。";
                    user = table.CreateBy;
                    break;
                default:
                    //默认状态不发送消息
                    Console.WriteLine($"不支持的消息类型:[{table.Status}]");
                    return;
            }
            esbMessageService.SendToUser("/BOM/detail?id=" + table.Id, title, user);
        }


        static IndentPrimaryEntity TableToPrimary(BomTableEntity table)
        {
            return new IndentPrimaryEntity
            {
                BomCode = table.BOMCode,
                BomId = table.Id,
                CurrentVersion = table.Version,
                ProjectCode = table.ProjectCode,
                ProjectName = table.ProjectName,
                Remark = table.Remark,
                Status = table.Status,
                //CreatedBy = BOMUser.Instance.Name,
                CreatedAt = DateTime.Now,
                LastModifiedAt = DateTime.Now,
                Applicant = table.ComitBy,
                ApplicationDate = (table.ComitTime.HasValue ? table.ComitTime.Value : DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss"),
            };
        }

        Mat_OperateEntity ItemToPLMItem(BomItemEntity item, pdm_material material)
        {
            var user = CurrentUser.Value.Id;
            return new Mat_OperateEntity
            {
                MaterialCode = item.Code,
                Count = item.Count,
                CreatedAt = DateTime.Now,
                CreatedBy = user,
                MaterialName = material.Name,
                BomId = item.BomId,
                ParentCode = item.Parent,
                CostPrice = material.RefPrice,
                //2023-02-10 新增 by yuanyong
                SerialNumber = item.Order,
                //IsDrawing = 
                //要标记物料当前版本是新增、删除、不动
                //Tag = 
                Designation = material.Alias,
                Specification = material.Model,
                TechnicalParameters = material.Specification,
                MaterialQuality = material.Material,
                Brand = material.Manufacturer,
                Unit = material.BasicUnit,
                Weight = material.Weight + material.WeightUnit,
                SubmittedUnit = item.Inspector,
                MatSource = material.Source,
                DistributionTime = DateTime.Now,
                Remark = item.Remark,
                UnitPrice = material.RefPrice,
                Amount = material.RefPrice * item.Count,


            };
        }

        public List<CommentResult> GetCommentList(CommentSearchDto search)
        {
            throw new NotImplementedException();
        }

        public List<BOMItemDto> GetItems(GetBOMItemDto search)
        {
            throw new NotImplementedException();
        }

        public PagedResult<BOMTableDto> GetBOMList(BOMTableSearchDto search)
        {
            var user = CurrentUser.Value;
            var isManager = moduleService.HasModuleOperation(user.Id, "bom:manager");

            var lst = this.Repository.Select
                .Where(x => x.Status == BOMStatusDesc.Approving || x.Status == BOMStatusDesc.Auditing || x.Status == BOMStatusDesc.Purchasing || x.Status == BOMStatusDesc.Completed)
                //非管理员只能查看到自己审核的
                .WhereIf(!isManager, x => x.ApprovalBy == user.RealName || x.AuditBy == user.RealName || x.PurchaseBy == user.RealName)
                .WhereIf(!string.IsNullOrEmpty(search.Key),
                    x => x.ProjectCode.Contains(search.Key)
                    || x.ProjectName.Contains(search.Key)
                    || x.BOMCode.StartsWith(search.Key)
                    || x.BOMName.Contains(search.Key)
                    )
                .WhereIf(!string.IsNullOrEmpty(search.Version), x => x.Version == search.Version)
                .OrderByDescending(x => x.CreateTime)
                .Count(out long total)
                .Page(search.PageNo, search.PageSize)
                .ToList()
                .Select(x => Mapper.Map<BOMTableDto>(x).SetUser(user.RealName).SetManager(isManager)).ToList();
            return new PagedResult<BOMTableDto>(lst, total);
        }

        public BOMDataDto GetBOM(int id)
        {
            var user = CurrentUser.Value;
            var isManager = moduleService.HasModuleOperation(user.Id, "bom:manager");

            BOMDataDto dto = new BOMDataDto();

            var table = this.Repository.Get(id);
            var tableDto = Mapper.Map<BOMTableDto>(table).SetUser(user.RealName).SetManager(isManager);
            dto.Table = tableDto;
            //BOM项
            var items = this.Repository.Orm
                .Select<BomItemEntity, pdm_material>()
                .Where(x => x.t1.Code == x.t2.Code)
                .Where(x =>
                    this.Repository.Orm.Select<BomTableEntity>()
                    .Where(a => (a.ProjectCode == table.ProjectCode && a.Status == BOMStatusDesc.Completed) || a.Id == id)
                    .ToList(a => a.Id)
                    .Contains(x.t1.BomId)
                )
                .OrderBy(x => x.t1.Order)
                .ToList(x => new { x.t1, x.t2 })
                .Select(x =>
                 {
                     var item = Mapper.Map<BOMItemDto>(x.t1);
                     item.Material = x.t2;
                     return item;
                 })
                .OrderBy(x => new BOMLevel(x.Order))
                .ToList();
            dto.Items = items;

            //BOM审批记录
            dto.Comments = this.Repository.Orm
                .Select<BOMApprovalLogEntity>()
                .Where(x => x.BomId == id)
                .ToList()
                .Select(x => Mapper.Map<CommentListDto>(x))
                .ToList()
                ;


            return dto;
        }
        public bool UndoBOM(int id)
        {
            var table = this.Repository.Where(x => x.Id == id).First();
            table.Undo();
            return this.Repository.Update(table) > 0;
        }

        public PagedResult<BOMTableDto> GetMyBOM(MyBomTableSearchDto search)
        {
            var user = CurrentUser.Value;
            var isManager = moduleService.HasModuleOperation(user.Id, "bom:manager");

            var lst = this.Repository.Select
                //非管理员只能查看到自己审核的
                .Where(x => x.ComitBy == user.RealName || x.CreateBy == user.RealName)
                .WhereIf(!string.IsNullOrEmpty(search.Key),
                    x => x.ProjectCode.Contains(search.Key)
                    || x.ProjectName.Contains(search.Key)
                    || x.BOMCode.StartsWith(search.Key)
                    || x.BOMName.Contains(search.Key)
                    )
                .WhereIf(!string.IsNullOrEmpty(search.Version), x => x.Version == search.Version)
                .Count(out long total)
                .Page(search.PageNo, search.PageSize)
                .ToList()
                .Select(x => Mapper.Map<BOMTableDto>(x).SetUser(user.RealName).SetManager(isManager)).ToList();
            return new PagedResult<BOMTableDto>(lst, total);
        }
    }
}
