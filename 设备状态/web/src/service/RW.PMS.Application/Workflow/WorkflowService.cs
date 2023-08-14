using AutoMapper;
using FreeSql;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Log;
using RW.PMS.Application.Contracts.DTO.Message;
using RW.PMS.Application.Contracts.DTO.Organization;
using RW.PMS.Application.Contracts.DTO.Workflow;
using RW.PMS.Application.Contracts.DTO.Workflow.Converter;
using RW.PMS.Application.Contracts.Message;
using RW.PMS.Application.Contracts.Service;
using RW.PMS.Application.Contracts.System;
using RW.PMS.Application.Event;
using RW.PMS.Application.System;
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
    public class WorkflowService : CrudApplicationService<WorkflowEntity, int>, IWorkflowService
    {
        IEventBus eventBus;
        IDictionaryService dictService;

        public WorkflowService(IDataValidatorProvider dataValidator,
                               IRepository<WorkflowEntity, int> repository,
                               IMapper mapper,
                               Lazy<ICurrentUser> currentUser,
                               IEventBus eventBus,
                               IDictionaryService dictService
                               )
            : base(dataValidator, repository, mapper, currentUser)
        {
            this.eventBus = eventBus;
            this.dictService = dictService;
        }

        public bool Add(AddWorkflowDto input)
        {
            var model = this.Insert(input);
            bool result = model != null && model.Id > 0;
            //logService.AddOperationLog(result, "添加流程成功", $"用户[{CurrentUser.Value.RealName}]添加了流程[{input.Title}].");
            eventBus.Trigger(LogEventData.OperationData(OperationActionEnum.Insert, result, "流程"));
            return model != null;
        }

        public PagedResult<WorkflowListDto> GetPagedList(WorkflowQueryDto input)
        {
            var lst = Repository.Where(x => true)
                .WhereIf(!string.IsNullOrEmpty(input.Type), x => x.Type == input.Type)
                .WhereIf(!string.IsNullOrEmpty(input.Title), x => x.Title.Contains(input.Title))
                .WhereIf(input.Enabled != null, x => x.Enabled == input.Enabled.Value)
                .Count(out long total)
                .Page(input.PageNo, input.PageSize)
                .ToList()
                .Select(x =>
                {
                    var dto = Mapper.Map<WorkflowListDto>(x);
                    dto.TypeKey = dto.Type;
                    dto.Type = dictService.GetCacheValue("WorkflowType", dto.Type);
                    return dto;
                })
                .ToList();
            return new PagedResult<WorkflowListDto>(lst, total);
            //throw new NotImplementedException();
        }

        public bool Modify(int id, EditWorkflowDto input)
        {
            var entity = Mapper.Map<WorkflowEntity>(input);
            var c = this.Update(id, entity);
            var result = c > 0;
            eventBus.Trigger(LogEventData.OperationData(OperationActionEnum.Update, result, "流程"));
            return result;
        }

        public bool Remove(int id)
        {
            var c = this.Delete(id);
            var result = c > 0;
            eventBus.Trigger(LogEventData.OperationData(OperationActionEnum.Delete, result, "流程"));
            return result;
        }



        public EditWorkflowDto GetFlow(int id)
        {
            var entity = Repository.Get(id);
            if (entity == null) throw new Exception($"无法找到流程[{id}]");
            var dto = Mapper.Map<EditWorkflowDto>(entity);
            var node = BaseFlowModel.ToModel(dto.WorkFlowData);

            var send = BaseFlowModel.FindNode<SendNode>(node);
            if (send != null)
                dto.AllowUserSend = send.UserSelectFlag;

            return dto;
        }
    }
}
