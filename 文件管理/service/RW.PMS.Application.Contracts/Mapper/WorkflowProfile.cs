using AutoMapper;
using RW.PMS.Application.Contracts.DTO.Workflow;
using RW.PMS.Application.Contracts.DTO.Workflow.Converter;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Entities.Workflow;
using RW.PMS.Domain.Entities.Workflow.Issue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Mapper
{
    public class WorkflowProfile : Profile
    {
        public WorkflowProfile()
        {
            //工作流主表
            CreateMap<WorkflowEntity, WorkflowListDto>()
                .ForMember(x => x.CreateTime, opt => opt.MapFrom(a => a.CreatedAt))
                .ForMember(x => x.UpdateTime, opt => opt.MapFrom(a => a.LastModifiedAt))
                .ForMember(x => x.WorkFlowNode, opt => opt.MapFrom(a => GetFlowModel(a.WorkFlowData)))
                ;
            CreateMap<AddWorkflowDto, WorkflowEntity>()
                ;
            CreateMap<EditWorkflowDto, WorkflowEntity>()
                ;
            CreateMap<WorkflowEntity, EditWorkflowDto>()
                ;
            //用户工作流表
            CreateMap<UserFlowEntity, UserFlowListDto>()
                .ForMember(x => x.CreateTime, opt => opt.MapFrom(a => a.CreatedAt))
                ;
            CreateMap<AddUserFlowDto, UserFlowEntity>()
                .ForMember(x => x.Status, opt => opt.MapFrom(a => GetStatus(a.AutoPublish)))
                ;
            CreateMap<EditUserFlowDto, UserFlowEntity>()
                .ForMember(x => x.Status, opt => opt.MapFrom(a => GetStatus(a.AutoPublish)))
                ;
            CreateMap<UserFlowEntity, UserFlowModelDto>()
                .ForMember(x => x.CreateTime, opt => opt.MapFrom(a => a.CreatedAt))
                .ForMember(x => x.FlowData, opt => opt.MapFrom(a => PraseNode(a.FlowData)))
                ;
            CreateMap<IssueFeedbackEntity, IssueDto>()
                //.ForMember(x => x.Desc, opt => opt.MapFrom(a => GetHtmlString(a.Desc)))
                ;
            CreateMap<IssueDto, IssueFeedbackEntity>();
            CreateMap<WorkFlowAuditEntity, AuditListDto>()
                .ForMember(x => x.Time, opt => opt.MapFrom(a => a.CreatedAt))
                ;
            CreateMap<AuditSubmitDto, WorkFlowAuditEntity>()
                .ForMember(x => x.UserWorkflowId, opt => opt.MapFrom(a => a.FlowId))
                ;
            CreateMap<WorkFlowNodeEntity, WorkflowNodeDto>();

        }

        JsonNode PraseNode(string text)
        {
            return JsonNode.Parse(text);
        }

        string ToJson(JsonNode node)
        {
            return node.ToJsonString();
        }

        UserFlowStatus GetStatus(bool auto)
        {
            if (auto)
                return UserFlowStatus.Approving;
            return UserFlowStatus.Saved;
        }

        BaseFlowModel GetFlowModel(string text)
        {
            return BaseFlowModel.ToModel(text);
        }

        string GetFlowModelText(BaseFlowModel model)
        {
            return BaseFlowModel.ToText(model);
        }

        //string GetHtmlString(string str)
        //{
        //    var code = HtmlEncoder.Default.Encode(str);
        //    return code;

        //    HtmlString html = new HtmlString(str);
        //    return html.ToString();
        //}

    }
}
