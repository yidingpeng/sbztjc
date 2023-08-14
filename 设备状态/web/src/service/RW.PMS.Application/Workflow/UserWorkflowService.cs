using AutoMapper;
using FreeSql;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.File;
using RW.PMS.Application.Contracts.DTO.Message;
using RW.PMS.Application.Contracts.DTO.Organization;
using RW.PMS.Application.Contracts.DTO.Workflow;
using RW.PMS.Application.Contracts.Message;
using RW.PMS.Application.Contracts.Service;
using RW.PMS.Application.Contracts.System;
using RW.PMS.Application.Event;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.EventBus;
using RW.PMS.CrossCutting.Exceptions;
using RW.PMS.CrossCutting.Extender;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Entities.Workflow;
using RW.PMS.Domain.Entities.Workflow.Issue;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace RW.PMS.Application.Workflow
{
    internal class UserWorkflowService : CrudApplicationService<UserFlowEntity, int>, IUserWorkflowService
    {
        IRepository<WorkflowEntity, int> flowRepo;
        IRepository<UserEntity, int> userRepo;
        IRepository<WorkFlowAuditEntity, int> auditRepo;
        IRepository<WorkFlowNodeEntity, int> nodeRepo;
        IRepository<IssueFeedbackEntity, int> issueRepo;
        IRepository<UserAttachmentEntity, int> attachmentRepo;
        IRepository<FileEntity, int> fileRepo;
        IOrganizationService orgService;
        UnitOfWorkManager unitOfWork;
        IEventBus eventBus;
        IDictionaryService dictSerivce;

        public UserWorkflowService(
            IDataValidatorProvider dataValidator,
            UnitOfWorkManager unitOfWork,
            IRepository<WorkflowEntity, int> flowRepo,
            IRepository<UserFlowEntity, int> repository,
            IRepository<UserEntity, int> userRepo,
            IRepository<WorkFlowAuditEntity, int> auditRepo,
            IRepository<WorkFlowNodeEntity, int> nodeRepo,
            IRepository<IssueFeedbackEntity, int> issueRepo,
            IRepository<UserAttachmentEntity, int> attachmentRepo,
            IRepository<FileEntity, int> fileRepo,
            IOrganizationService orgService,
            IEventBus eventBus,
            IDictionaryService dictSerivce,
            IMapper mapper,
            Lazy<ICurrentUser> currentUser) : base(dataValidator, repository, mapper, currentUser)
        {
            this.unitOfWork = unitOfWork;
            this.auditRepo = auditRepo;
            this.flowRepo = flowRepo;
            this.nodeRepo = nodeRepo;
            this.issueRepo = issueRepo;
            this.userRepo = userRepo;
            this.orgService = orgService;
            this.attachmentRepo = attachmentRepo;
            this.fileRepo = fileRepo;
            this.eventBus = eventBus;
            this.dictSerivce = dictSerivce;
        }


        void AddAudit(int userFlowId, string text, bool result = true)
        {
            auditRepo.Insert(new WorkFlowAuditEntity
            {
                Comments = text,
                UserWorkflowId = userFlowId,
                Result = result
            });
        }

        public bool AddMine(AddUserFlowDto input)
        {
            //可注入UnitOfWorkManager unitOfWork
            //using(unitOfWork){}
            //最后需要unitOfWork.Commit()
            using (IUnitOfWork unit = unitOfWork.Begin())
            {
                try
                {
                    //1、添加流程信息
                    if (input.WorkflowId == 0) throw new LogicException(ExceptionCode.Failed, "请关联工作流模板");
                    var entity = flowRepo.Get(input.WorkflowId);
                    if (entity == null) throw new Exception("未找到相关工作流模板");
                    var ufEntity = Mapper.Map<UserFlowEntity>(input);
                    ufEntity.FlowData = entity.WorkFlowData;
                    ufEntity.SN = GenerateNo();
                    var result = Repository.Insert(ufEntity);

                    //1.1 添加附件，先删除所有附件再关联进去

                    input.Files.SaveTo(result.Id, "userflow", attachmentRepo);

                    input.Id = result.Id;
                    //2、添加流程详情
                    AddDetail(input.Type, input.Id, input.Detail);

                    if (input.AutoPublish)
                        AddAudit(ufEntity.Id, "提交审批");
                    //提交后自动发布，否则只是保存未发布
                    //用户可进行编辑，重新勾选提交并发布

                    var data = BaseFlowModel.ToModel(entity.WorkFlowData);
                    {
                        SendNode send = BaseFlowModel.FindNode<SendNode>(data);
                        if (send != null)
                        {
                            ufEntity.AllowUserSend = send.UserSelectFlag;
                            Repository.Update(ufEntity);
                        }
                    }

                    var nodeId = new NodeIdGenerator("N");
                    var nodes = CreateFlowNode(input, data, nodeId).ToList();
                    //手动添加最后一个节点
                    nodes.Add(ExecuteCompleted(input, new BaseFlowModel { NodeName = "结束" }, nodeId.Next()));
                    for (int i = 0; i < nodes.Count - 1; i++)
                        nodes[i].NextNodeId = nodes[i + 1].NodeId;
                    nodeRepo.Insert(nodes);
                    ExecuteNode(ufEntity, nodes);

                    unit.Commit();


                    if (input.AutoPublish)
                        eventBus.Trigger(new WorkflowEventData
                        {
                            Title = input.Title,
                            Action = WorkflowActionEnums.Publish,
                            DataId = result.Id,
                        });

                    return result.Id > 0;
                }
                catch
                {
                    unit.Rollback();
                    throw;
                }
            }
        }



        /// <summary>
        /// 执行流程节点
        /// 任何流程执行时，都需执行此方法
        /// </summary>
        bool ExecuteNode(int id)
        {
            var entity = Repository.Get(id);
            var nodes = nodeRepo.Where(x => x.UserFlowId == id).ToList();
            return ExecuteNode(entity, nodes);
        }

        /// <summary>
        /// 执行流程节点
        /// 任何流程执行时，都需执行此方法
        /// </summary>
        /// <param name="ufEntity">用户流程实体</param>
        /// <param name="nodes">当前流程的所有节点</param>
        bool ExecuteNode(UserFlowEntity ufEntity, List<WorkFlowNodeEntity> nodes)
        {
            var uid = CurrentUser.Value.Id;
            var auditUserIds = new List<int>();
            //发起人、抄送、条件都需要系统自动执行
            for (int i = 0; i < nodes.Count(); i++)
            {
                var node = nodes[i];
                //已完成的流程无需处理
                if (node.Status == WorkflowNodeStatus.Completed || node.Status == WorkflowNodeStatus.Skip)
                {
                    if (node.NodeType != NodeActionEnums.Promoter)
                        auditUserIds.AddRange(node.UserIds.StringToArray<int>());
                    continue;
                }
                //ufEntity.CurrentNode = node.NodeName + node.Handler.Substring(0, Math.Min(node.Handler.Length, 100));
                ufEntity.SetCurrentNode(node);
                //只要不是待处理，则认为流程异常，不进行自动处理
                if (node.Status != WorkflowNodeStatus.Default && node.Status != WorkflowNodeStatus.Approving) break;
                switch (node.NodeType)
                {
                    case NodeActionEnums.Default:
                        break;
                    case NodeActionEnums.Promoter:
                        //发起人提交时，默认执行了；此处有可能被驳回，需发起人重新填写
                        break;
                    case NodeActionEnums.Approver:
                        {
                            node.Approving();
                            if (node.ExamineMode == ExamineModeEnums.AllOf)
                            {
                                ufEntity.StartCountersigning();
                            }
                            //如果用户都已审过，则跳过
                            var needAuditUser = node.UserIds.StringToArray<int>().Where(x => !auditUserIds.Contains(x)).ToArray();
                            //没有可审批的用户，自动跳过
                            if (needAuditUser.Length == 0)
                            {
                                node.SkipFlow();
                            }
                            else
                            {
                                int u = needAuditUser.Minus(node.AuditUserIds.StringToArray<int>());
                                //int u = node.AuditUserIds.StringToArray<int>().Minus(needAuditUser);
                                if (u == 0)
                                {
                                    node.Completed();
                                }
                                else if (!node.IsCompleted() && u != 0)
                                {
                                    //审批由用户自审
                                    //此步骤需要发送消息
                                    var nuids = node.NotifyUserIds.StringToArray<int>().ToList();
                                    if (!nuids.Contains(u))
                                    {
                                        nuids.Add(u);
                                        node.NotifyUserIds = nuids.ArrayToString();
                                        eventBus.Trigger(new MessageEventData
                                        {
                                            Title = $"请审批[{ufEntity.Title}]流程",
                                            Content = $"请审批流程[{ufEntity.Title}]-[{node.Handler}]",
                                            DataId = ufEntity.Id,
                                            Type = "workflow",
                                            UserIds = new int[] { u }
                                        });
                                    }
                                }
                            }

                        }
                        break;
                    case NodeActionEnums.Send:
                        node.Completed();
                        //todo:抄送、发送消息
                        if (!string.IsNullOrEmpty(node.UserIds))
                            eventBus.Trigger(new WorkflowEventData
                            {
                                Title = ufEntity.Title,
                                DataId = ufEntity.Id,
                                Action = WorkflowActionEnums.Send,
                                UserIds = node.UserIds.StringToArray<int>(),
                            });
                        break;
                    case NodeActionEnums.Condition:
                        //条件：在上一步执行完后，自动执行条件步骤
                        node.Completed();
                        break;
                    case NodeActionEnums.Completed:
                        node.Completed();

                        //流程走完：应该给发送人一个消息，并将主流程标记为完成状态
                        eventBus.Trigger(new WorkflowEventData { Title = ufEntity.Title, DataId = ufEntity.Id, Action = WorkflowActionEnums.Completed, UserIds = new int[] { uid } });
                        ufEntity.OverTime = DateTime.Now;
                        ufEntity.Completed();
                        AddAudit(ufEntity.Id, "流程结束", true);
                        break;
                    default:
                        break;
                }
                //如果处理后，仍然是未完成，则取消（通常是需审批人进行审核）
                if (!node.IsCompleted()) break;
            }
            if (nodes.Count > 0)
                Repository.Update(ufEntity);
            nodeRepo.Update(nodes);
            return true;
        }

        int AddDetail(string type, int workflowId, JsonNode detail)
        {
            if (type == "issue")
            {
                //JsonSerializer.Deserialize<IssueDto>(detail);
                var dto = detail.Deserialize<IssueDto>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var entity = Mapper.Map<IssueFeedbackEntity>(dto);
                entity.UserFlowId = workflowId;
                issueRepo.Insert(entity);
                return entity.Id;
            }
            else
            {
                throw new LogicException(ExceptionCode.Failed, $"类型[{type}]暂不支持。");
            }
        }

        int EditDetail(string type, int userFlowId, JsonNode detail)
        {
            if (type == "issue")
            {
                var model = issueRepo.Where(x => x.UserFlowId == userFlowId).First();
                var dto = detail.Deserialize<IssueDto>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var entity = Mapper.Map(dto, model);
                //var entity = Mapper.Map<IssueFeedbackEntity>(detail);
                issueRepo.InsertOrUpdate(entity);
                return 1;
            }
            else
            {
                throw new LogicException(ExceptionCode.Failed, $"类型[{type}]暂不支持。");
            }
        }

        //0 创建流程节点
        IEnumerable<WorkFlowNodeEntity> CreateFlowNode(AddUserFlowDto input, BaseFlowModel model, NodeIdGenerator nodeId)
        {
            List<WorkFlowNodeEntity> entities = new List<WorkFlowNodeEntity>();
            //发起人，通常该项为第一项
            if (model is Promoter p)//发起人，可控制角色
            {
                entities.Add(ExecutePromoter(input, p, nodeId));
            }
            else if (model is ConditionNode c)//条件分支，系统自动判断
            {
                entities.AddRange(ExecuteCondition(input, c, nodeId));
            }
            else if (model is ApprovideNode approver)//审核人
            {
                entities.Add(ExecuteApprovide(input, approver, nodeId));
            }
            else if (model is SendNode send)//抄送，系统自动判断
            {
                entities.Add(ExecuteSend(input, send, nodeId));
            }

            if (model.ChildNode != null)
                entities.AddRange(CreateFlowNode(input, model.ChildNode, nodeId.Next()));

            return entities;
        }
        //1 发起人
        WorkFlowNodeEntity ExecutePromoter(AddUserFlowDto input, Promoter p, NodeIdGenerator nodeId)
        {

            return new WorkFlowNodeEntity
            {
                UserFlowId = input.Id,
                NodeName = p.NodeName,
                NodeType = NodeActionEnums.Promoter,
                Status = WorkflowNodeStatus.Completed,//发起人自动完成
                Handler = CurrentUser.Value.RealName,
                UserIds = CurrentUser.Value.Id.ToString(),
                NodeId = nodeId.ToString(),
            };
        }
        //2 条件
        IEnumerable<WorkFlowNodeEntity> ExecuteCondition(AddUserFlowDto input, ConditionNode c, NodeIdGenerator nodeId)
        {
            ConditionItemNode node = null;

            foreach (var item in c.ConditionNodes)
            {
                bool result = true;//如未配置条件，表示任意情况
                bool or = item.ConditionMode == 0;//多个条件采用“或”、“且”
                foreach (var condition in item.ConditionList)
                {
                    if (or)
                        result |= condition.Execute(input);
                    else
                        result &= condition.Execute(input);
                    if (result) break;
                }
                if (result)
                {
                    node = item;
                    break;
                }
                nodeId.Next();
            }
            //必须找到一个条件，否则流程无法继续，应该提示报错
            if (node == null) throw new LogicException("预处理流程失败，未匹配到任何条件。");

            var entity = new WorkFlowNodeEntity
            {
                NodeName = node.NodeName,
                UserFlowId = input.Id,
                NodeType = NodeActionEnums.Condition,
                Handler = "系统-" + node.ToString(),//条件分支由系统判断
                NodeId = nodeId.ToString(),
            };

            yield return entity;
            if (node.ChildNode != null)
            {
                var lst = CreateFlowNode(input, node.ChildNode, new NodeIdGenerator("L"));
                foreach (var item in lst) yield return item;
            }
        }
        //3 审批人
        WorkFlowNodeEntity ExecuteApprovide(AddUserFlowDto input, ApprovideNode model, NodeIdGenerator nodeId)
        {
            //自己本人ID
            var uid = CurrentUser.Value.Id;

            //根据审批方式，选择不同的审批人员
            switch (model.SetType)
            {
                case 1://指定人员
                    return new WorkFlowNodeEntity
                    {
                        NodeId = nodeId.ToString(),
                        NodeName = model.NodeName,
                        UserFlowId = input.Id,
                        NodeType = NodeActionEnums.Approver,
                        //IdType = 1,
                        Handler = model.NodeUserList.Select(x => x.Name).ArrayToString(),
                        UserIds = model.NodeUserList.Select(x => x.Id.ToString()).ArrayToString(),
                        ExamineMode = (ExamineModeEnums)model.ExamineMode,
                        Timeout = model.Term,
                        TimeoutHandle = model.TermMode == 0,
                    };
                case 2://主管 ，单级主管，指定第几级
                    {
                        var vistor = new KeyValueDto("", "");
                        //获取当前用户
                        var user = userRepo.Get(uid);
                        if (user == null) throw new LogicException(ExceptionCode.AccountNotExist);
                        OrgTreeNode orgNode = OrgTreeNode.GetTreeNode(orgService.GetTreeList(new OrganizationSearchDto()));
                        var current = orgNode.FindById(user.OrgId);
                        if (string.IsNullOrEmpty(current.Leader)) throw new LogicException("您没有直接主管！");
                        for (int i = 0; i < model.ExamineLevel; i++)
                        {
                            vistor.Key = current.LeaderUserId.ToString();
                            vistor.Value = current.Leader;
                            //自动找上一级，直到没有上一级了
                            if (current.Parent != null)
                                current = current.Parent;
                            else if (current.LeaderUserId == uid)//如果自己本来也是主管，那么就找上一级主管
                            {
                                i--;
                                continue;
                            }
                            else
                                break;
                        }

                        return new WorkFlowNodeEntity
                        {
                            NodeId = nodeId.ToString(),
                            NodeName = model.NodeName,
                            //IdType = 1,
                            UserFlowId = input.Id,
                            NodeType = NodeActionEnums.Approver,
                            Handler = $"主管({vistor.Value})",
                            UserIds = vistor.Key,
                            Timeout = model.Term,
                            TimeoutHandle = model.TermMode == 0,
                        };
                    }
                case 3://指定角色
                    {
                        var roleIds = model.NodeRoleList.Select(x => x.Id);
                        var users = userRepo.Where(x =>
                            userRepo.Orm.Select<UserRoleEntity>().Where(ur => roleIds.Contains(ur.RoleId)).ToList(ur => ur.UserId).Contains(x.Id)).ToList();

                        return new WorkFlowNodeEntity
                        {
                            NodeId = nodeId.ToString(),
                            NodeName = model.NodeName,
                            UserFlowId = input.Id,
                            NodeType = NodeActionEnums.Approver,
                            //IdType = 1,
                            Handler = $"角色[{model.NodeRoleList.Select(x => x.Name).ArrayToString()}]（{users.Select(x => x.RealName).ArrayToString()}）",
                            UserIds = users.Select(x => x.Id).ArrayToString(),
                            Timeout = model.Term,
                            TimeoutHandle = model.TermMode == 0,
                        };
                    }
                case 4://发起人自选   建议该项移除，不处理自选。否则添加表单时需解析此内容
                case 5://发起人自己
                    return new WorkFlowNodeEntity
                    {
                        NodeId = nodeId.ToString(),
                        NodeName = model.NodeName,
                        UserFlowId = input.Id,
                        //IdType = 1,
                        Handler = $"发起人",
                        NodeType = NodeActionEnums.Approver,
                        UserIds = uid.ToString(),
                        ExamineMode = (ExamineModeEnums)model.ExamineMode,
                        Timeout = model.Term,
                        TimeoutHandle = model.TermMode == 0,
                    };
                case 7://连续多级主管
                    {
                        var vistors = new List<KeyValueDto<int, string>>();
                        //获取当前用户
                        var user = userRepo.Get(uid);
                        if (user == null)
                            throw new LogicException(ExceptionCode.AccountNotExist);
                        OrgTreeNode orgNode = OrgTreeNode.GetTreeNode(orgService.GetTreeList(new OrganizationSearchDto()));
                        var current = orgNode.FindById(user.OrgId);
                        if (current == null) throw new LogicException("您没有部门归属");
                        if (string.IsNullOrEmpty(current.Leader)) throw new LogicException("您没有直接主管！");
                        //连续主管数，默认最上级
                        var levelCount = 99;
                        //指定第几级主管
                        if (model.DirectorMode == 1)
                            levelCount = model.DirectorLevel;
                        for (int i = 0; i < levelCount; i++)
                        {
                            vistors.Add(new KeyValueDto<int, string>(current.LeaderUserId.Value, current.Leader));
                            //自动找上一级，直到没有上一级了
                            if (current.Parent != null)
                                current = current.Parent;
                            else
                                break;
                        }


                        return new WorkFlowNodeEntity
                        {
                            NodeId = nodeId.ToString(),
                            NodeName = model.NodeName,
                            UserFlowId = input.Id,
                            NodeType = NodeActionEnums.Approver,
                            Status = WorkflowNodeStatus.Approving,
                            //IdType = 1,
                            Handler = $"多级主管（{vistors.Select(x => x.Value).ArrayToString()}）",
                            UserIds = vistors.Select(x => x.Key).ArrayToString(),
                            ExamineMode = (ExamineModeEnums)model.ExamineMode,
                            Timeout = model.Term,
                            TimeoutHandle = model.TermMode == 0,
                        };
                    }
                default:
                    return null;
                    break;
            }
        }
        //4 抄送
        WorkFlowNodeEntity ExecuteSend(AddUserFlowDto input, SendNode model, NodeIdGenerator nodeId)
        {
            var uids = model.NodeUserList.Select(x => x.Id).ToList();
            var handlers = model.NodeUserList.Select(x => x.Name).ToList();
            if (model.UserSelectFlag)
            {
                uids.AddRange(input.SendUsers.Select(x => x.Id));
                handlers.AddRange(input.SendUsers.Select(x => x.Name));
            }
            return new WorkFlowNodeEntity
            {
                NodeId = nodeId.ToString(),
                NodeName = model.NodeName,
                UserFlowId = input.Id,
                NodeType = NodeActionEnums.Send,
                Handler = handlers.Distinct().ArrayToString(),//有重名就完蛋
                UserIds = uids.Distinct().ArrayToString(),
            };
        }
        //5 完成
        WorkFlowNodeEntity ExecuteCompleted(AddUserFlowDto input, BaseFlowModel model, NodeIdGenerator nodeId)
        {
            var uid = CurrentUser.Value.Id;

            return new WorkFlowNodeEntity
            {
                NodeId = nodeId.ToString(),
                NodeName = model.NodeName,
                UserFlowId = input.Id,
                NodeType = NodeActionEnums.Completed,
                Handler = "流程结束",
                UserIds = uid.ToString(),
            };
        }

        public PagedResult<UserFlowListDto> GetUserFlowPagedList(UserFlowQueryDto input)
        {
            var uid = CurrentUser.Value.Id;
            //FIXME:
            //此处查询效率太低，由于userids是“1,2,3”的格式，又要防止出现“10,11,12,13”能匹配到“1,”和“,1”，所以只能是下面的写法
            var lst = Repository.Select.From<UserEntity>((f, u) => f.LeftJoin(a => a.CreatedBy == u.Id))
                .WhereIf(input.Status.HasValue, x => x.t1.Status == input.Status)
                .WhereIf(!string.IsNullOrEmpty(input.Title), x => x.t1.Title.Contains(input.Title))
                .Where(x => x.t1.CreatedBy == uid
                || (nodeRepo.Select.Any(n =>
                    n.UserFlowId == x.t1.Id
                    && (n.UserIds == uid.ToString()
                    || n.UserIds.StartsWith(uid + ",")
                    || n.UserIds.EndsWith("," + uid)
                    || n.UserIds.Contains("," + uid + ","))
                    )
                && x.t1.Status != UserFlowStatus.Saved)
                )
                .Count(out long total)
                .OrderByDescending(x => x.t1.CreatedAt)
                .Page(input.PageNo, input.PageSize)
                .ToList(x => new { x.t1, x.t2 })
                .Select(x =>
                {
                    var dto = Mapper.Map<UserFlowListDto>(x.t1).SetRequested(x.t2?.RealName);
                    dto.Type = dictSerivce.GetCacheValue("WorkflowType", dto.Type);
                    return dto;
                })
                .ToList();

            return new PagedResult<UserFlowListDto>(lst, total);
        }


        public bool ModifyMine(int id, EditUserFlowDto input)
        {
            var entity = Repository.Get(id);
            Mapper.Map(input, entity);
            var c = Repository.Update(entity);

            //1.1 添加附件，先删除所有附件再关联进去
            input.Files.SaveTo(id, "userflow", attachmentRepo);

            if (input.AutoPublish)
            {
                AddAudit(id, "提交审批");
                //设置发起人节点状态
                var node = nodeRepo.Where(x => x.UserFlowId == id && x.NodeType == NodeActionEnums.Promoter).First();
                if (node == null) throw new LogicException("发起人节点为空，请重新提交流程。");
                node.Status = WorkflowNodeStatus.Completed;
                nodeRepo.Update(node);
            }
            //TODO:修改详情
            EditDetail(input.Type, input.Id, input.Detail);

            eventBus.Trigger(LogEventData.OperationData(OperationActionEnum.Update, c > 0, "我的流程"));

            return c > 0;
        }


        public bool RemoveMine(int id)
        {
            var c = Repository.Delete(id);
            eventBus.Trigger(LogEventData.OperationData(OperationActionEnum.Delete, c > 0, "我的流程"));
            return c > 0;
        }

        public UserFlowModelDto GetUserFlow(int id)
        {
            //此步骤有点多余，防止流程没有走到下一步，查看时再执行一次，调试时可以用到
            ExecuteNode(id);

            var uid = CurrentUser.Value.Id;
            //用户提交的流程
            var repo = flowRepo.Orm.Select<UserFlowEntity, UserEntity>();
            var entity = repo.Where(x => x.t1.Id == id).First(x => new { x.t1, x.t2 });
            if (entity == null) throw new LogicException(ExceptionCode.Failed, "流程不存在");

            //原始流程
            //var flow = flowRepo.Where(x => x.Id == entity.t1.WorkflowId).First();
            //if (flow == null) throw new LogicException(ExceptionCode.Failed, "原始流程不存在");

            var dto = Mapper.Map<UserFlowModelDto>(entity.t1);

            dto.Requested = entity.t2.RealName;
            if (dto.Type == "issue")
            {
                var issue = issueRepo.Where(x => x.UserFlowId == id).First();
                dto.Detail = Mapper.Map<IssueDto>(issue);
            }

            var files = attachmentRepo.Where(x => x.DataId == id).ToList().Select(x => Mapper.Map<FileOutputDto>(x)).ToList();
            dto.Files = files.ToArray();

            //审批记录
            var records = auditRepo.Select
                .From<UserEntity>((l, u) => l.LeftJoin(a => a.CreatedBy == u.Id))
                .Where(x => x.t1.UserWorkflowId == entity.t1.Id)
                .ToList(x => new { x.t1, x.t2 });
            dto.Records = records.Select(x =>
            {
                var dto = Mapper.Map<AuditListDto>(x.t1);
                dto.Files = attachmentRepo.Where(x => x.DataId == dto.Id && x.Type == "approve").ToList().Select(x => Mapper.Map<FileOutputDto>(x)).ToArray();
                if (x.t2 != null)
                    dto.Operator = x.t2.RealName;
                return dto;
            }).ToList();

            //节点
            var nodes = nodeRepo.Where(x => x.UserFlowId == id)
                        .ToList(x => x)
                        .Select(x => Mapper.Map<WorkflowNodeDto>(x))
                        .ToList();
            dto.Nodes = nodes;

            var send = nodes.Where(x => x.NodeType == NodeActionEnums.Send).FirstOrDefault();
            if (send != null)
            {
                var uids = send.UserIds.StringToArray<int>();
                var unames = send.Handler.StringToArray<string>();
                dto.SetUsers(uids, unames);
            }
            //当前用户是否可以审批，当前节点和节点审批用户是否相同
            var canApproval = false;
            //不能是首节点，发起人节点
            for (int i = 1; i < nodes.Count && !canApproval; i++)
            {
                var node = nodes[i];
                if (node.Status == WorkflowNodeStatus.Default || node.Status == WorkflowNodeStatus.Approving)
                {
                    canApproval = node.UserIds.StringToArray<int>().Minus(node.AuditUserIds.StringToArray<int>()) == uid;
                    break;
                }
            }
            dto.CanApproval = canApproval;

            //是否有权限查看
            var allUserIds = nodes.Select(x => x.UserIds).Where(x => !string.IsNullOrEmpty(x)).ArrayToString().StringToArray<int>().Distinct().ToList();
            if (!canApproval && !allUserIds.Contains(uid))
                throw new LogicException("您没有访问此流程的权限。");
            return dto;
        }

        public bool AuditFlow(AuditSubmitDto dto)
        {
            DataValidate(dto);

            var userflow = Repository.Get(dto.FlowId);
            if (userflow == null)
                throw new LogicException(ExceptionCode.Failed, "用户流程不存在，无法审核。");

            //节点状态是否正确
            var node = nodeRepo.Where(x => x.UserFlowId == dto.FlowId && (x.Status == WorkflowNodeStatus.Default || x.Status == WorkflowNodeStatus.Approving)).First();
            if (node == null)
                throw new LogicException("该流程无可用节点，无法审核");
            var uid = CurrentUser.Value.Id;
            if (!node.UserIds.StringToArray<int>().Contains(uid))
                throw new LogicException("当前不是您的审核节点，可能流程已变更，请刷新查看最新状态");

            //插入审批记录
            var entity = Mapper.Map<WorkFlowAuditEntity>(dto);
            auditRepo.Insert(entity);
            //保存附件
            dto.Files.SaveTo(entity.Id, "approve", attachmentRepo);

            //更新节点数据,审批通过
            if (dto.Result)
            {
                node.AddAuditUsers(uid);
                //TODO:会签、依次审批时，不能立即设置完成。
                if (node.IsCompleted())
                    node.Completed();
                else if (node.ExamineMode == ExamineModeEnums.AllOf)
                    node.Approving();
                nodeRepo.Update(node);

                eventBus.Trigger(new WorkflowEventData()
                {
                    Title = userflow.Title,
                    DataId = dto.FlowId,
                    UserIds = new int[] { userflow.CreatedBy },
                    Action = WorkflowActionEnums.Pass,
                });

            }
            else
            {
                //驳回
                //TODO:驳回直接驳回上一步或驳回至顶层，默认驳回至第一步
                var nodes = nodeRepo.Where(x => x.UserFlowId == dto.FlowId).ToList();
                //foreach (var item in nodes)
                bool flag = true;
                int resetCount = 0;
                for (int i = nodes.Count() - 1; i >= 0; i--)//倒序，从后面往前
                {
                    var item = nodes[i];
                    if (dto.Back == 0)//驳回至发起人
                        item.Reset();
                    else
                    {
                        //重置审批节点数量
                        if (resetCount == dto.Back) break;
                        //找到后可以进行节点重置
                        item.Reset();
                        if (flag && item.NodeId == node.NodeId)
                        {
                            flag = false;
                            continue;
                        }
                        //上一级必须是审核人或发起人才行
                        if (item.NodeType == NodeActionEnums.Approver || item.NodeType == NodeActionEnums.Promoter)
                            resetCount++;
                    }

                    userflow.SetCurrentNode(item);
                    if (item.NodeType == NodeActionEnums.Promoter)
                        userflow.Status = UserFlowStatus.Saved;
                }
                Repository.Update(userflow);
                nodeRepo.Update(nodes);

                eventBus.Trigger(new WorkflowEventData
                {
                    Title = userflow.Title,
                    DataId = dto.FlowId,
                    UserIds = new int[] { userflow.CreatedBy },
                    Action = WorkflowActionEnums.Reject
                });

            }

            ExecuteNode(dto.FlowId);

            return true;
        }


        //static int num = 1;
        static object locker = new object();


        /// <summary>
        /// 生成流水号
        /// </summary>
        public string GenerateNo()
        {
            lock (locker)//防止多线程入侵导致重复问题
            {
                var repo = Repository.Orm.Select<IDGeneratorEntity>();
                var item = repo.Where(x => x.Date == DateTime.Now.Date && x.Type == "workflow").First();
                if (item == null)
                {
                    item = new IDGeneratorEntity();
                    item.CreateNew("workflow");
                }
                var id = item.Next().GetId();

                var upset = Repository.Orm.InsertOrUpdate<IDGeneratorEntity>();
                upset.SetSource(item);
                upset.ExecuteAffrows();

                return id;
            }
        }

        /// <summary>
        /// 撤销审批
        /// </summary>
        public bool CancelMine(int id)
        {
            var uid = CurrentUser.Value.Id;
            var entity = Repository.Where(x => x.Id == id && x.CreatedBy == uid).First();
            if (entity == null) throw new LogicException("该流程不是由您发起。");
            entity.ResetStatus();
            var result = Repository.Update(entity) > 0;
            eventBus.Trigger(LogEventData.OperationData(OperationActionEnum.Revoke, result, "我的流程"));

            return result;
        }

        public List<FileListDto> GetMyFiles(FileQueryDto query)
        {
            var uid = CurrentUser.Value.Id;

            var lst = fileRepo
                 .Where(x => x.CreatedBy == uid)
                 .WhereIf(!string.IsNullOrEmpty(query.Type), x => x.RootPath == "/" + query.Type)
                 .WhereIf(!string.IsNullOrEmpty(query.Name), x => x.FileName.Contains(query.Name))
                 .ToList()
                 .Select(x => Mapper.Map<FileListDto>(x))
                 .ToList()
                 ;

            return lst;
        }

        /// <summary>
        /// 催办
        /// </summary>
        public bool Urging(int id)
        {
            var node = nodeRepo.Get(id);
            var ufEntity = Repository.Get(node.UserFlowId);

            var u = node.UserIds.StringToArray<int>().Minus(node.AuditUserIds.StringToArray<int>());

            eventBus.Trigger(new WorkflowEventData
            {
                Action = WorkflowActionEnums.Urging,
                Title = ufEntity.Title,
                DataId = ufEntity.Id,
                UserIds = new int[] { u },
                Content = $"请尽快审批流程[{ufEntity.Title}]-[{node.Handler}]",
            });

            return true;
        }
    }
}
