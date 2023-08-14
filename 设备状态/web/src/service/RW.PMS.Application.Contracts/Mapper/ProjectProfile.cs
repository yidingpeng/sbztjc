using AutoMapper;
using RW.PMS.Application.Contracts.DTO.Plan;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Input.BaseInfo;
using RW.PMS.Domain.Entities.Plan;
using RW.PMS.Domain.Entities.Project;
using RW.PMS.Domain.Entities.System;

namespace RW.PMS.Application.Contracts.Mapper
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Contract_InfoDto, Contract_InfoEntity>();
            //合同表
            CreateMap<Contract_InfoEntity, Contract_InfoDto>();
            CreateMap<ContractInfoFilesListDto, ContractInfoFilesEntity>();
            //合同子表
            CreateMap<ContractDetailEntity, ContractDetailDto>();
            CreateMap<ContractDetailDto, ContractDetailEntity>();

            CreateMap<Project_DeliveryDto, Project_DeliveryEntity>();
            CreateMap<Project_DeliveryEntity, Project_DeliveryDto>();

            CreateMap<ProjectBasicsEntity, ProjectBasicsListDto>();
            CreateMap<ProjectBasicsListDto, ProjectBasicsEntity>();
            CreateMap<ProjectBasicsEntity, ProjectBasicsView>();
            CreateMap<ProjectBasicsView, ProjectBasicsEntity>();
            //客户公司信息
            CreateMap<Client_CompanyEntity, Client_CompanyOutput>();
            CreateMap<Client_CompanySearchDto, Client_CompanyEntity>();
            CreateMap<Client_CompanyDto, Client_CompanyEntity>();
            CreateMap<Client_CompanyEntity, Client_CompanyDto>();
            CreateMap<DeleteCli_PayKeys, Client_CompanyEntity>();
            CreateMap<Client_CompanyDto, DictionaryEntity>();
            CreateMap<DictionaryEntity, Client_CompanyOutput>();
            //开票回款
            CreateMap<Payment_CollectionEntity, Payment_CollectionOutput>().ForMember(t => t.ReturnDate, opt => opt.MapFrom(s => s.ReturnDate.ToString("yyyy-MM-dd")));
            CreateMap<Payment_CollectionSearchDto, Payment_CollectionEntity>();
            CreateMap<Payment_CollectionDto, Payment_CollectionEntity>();
            //项目质保金信息
            CreateMap<ProjectRetMoneyEntity, ProjectRetMoneyOutput>();
            CreateMap<ProjectRetMoneySearchDto, ProjectRetMoneyEntity>();
            CreateMap<ProjectRetMoneyDto, ProjectRetMoneyEntity>();
            //联系人信息
            CreateMap<ContactsEntity, ContactListDto>();
            CreateMap<ContactListDto, ContactsEntity>();

            //项目联系人信息
            CreateMap<Project_ContactsInfo, projectcontactsListDto>();
            CreateMap<projectcontactsListDto, Project_ContactsInfo>();
			//项目设备信息
            CreateMap<ProjectDeviceDetailsEntity, ProjectDeviceDetailsView>();
            CreateMap<ProjectDeviceDetailsView, ProjectDeviceDetailsEntity>();

            //任务信息
            CreateMap<TaskProcessInfoEntity, TaskProcessInfoView>();
            CreateMap<TaskProcessInfoView, TaskProcessInfoEntity>();
            //市场片区
            CreateMap<bd_SalesAreaInfoDto, bd_SalesAreaInfoEntity>();
            CreateMap<bd_SalesAreaInfoEntity, bd_SalesAreaInfoDto>();

            //项目开票
            CreateMap<ProjectInvoicingDto, ProjectInvoicingEntity>();
            CreateMap<ProjectInvoicingEntity, ProjectInvoicingDto>();

            //项目交付文件
            CreateMap<ProjectDeliveryFileDto, ProjectDeliveryFileEntity>();
            CreateMap<ProjectDeliveryFileEntity, ProjectDeliveryFileDto>();

            //项目验收
            CreateMap<ProjectAcceptanceDto, ProjectAcceptanceEntity>();
            CreateMap<ProjectAcceptanceEntity, ProjectAcceptanceDto>();

            //营销销售数据
            CreateMap<ProjectBasicsEntity, SellBasicsDataView>();

            //回款计划
            CreateMap<PaymentCollectionPlanEntity, PaymentCollectionPlanDto>();
            CreateMap<PaymentCollectionPlanDto, PaymentCollectionPlanEntity>();

            //项目模板
            CreateMap<projectTemplateEntity, projectTemplateView>();
            CreateMap<projectTemplateView, projectTemplateEntity>();

            //项目模板阶段
            CreateMap<projectTemplatePhaseEntity, projectTemplatePhaseView>();
            CreateMap<projectTemplatePhaseView, projectTemplatePhaseEntity>();

            //项目动态
            CreateMap<ProjectDynamicEntity, ProjectDynamicView>();
            CreateMap<ProjectDynamicView, ProjectDynamicEntity>();

            //项目WBS计划
            CreateMap<WbsPlanEntity, WbsPlanDto>();
            CreateMap<WbsPlanDto, WbsPlanEntity>();

            //项目WBSTabs计划主表
            CreateMap<WbsTabsEntity, WbsTabsDto>();
            CreateMap<WbsTabsDto, WbsTabsEntity>();

            //配置阶段
            CreateMap<ConfigureStageEntity, ConfigureStageDto>();
            CreateMap<ConfigureStageDto, ConfigureStageEntity>();

            //项目计划名
            CreateMap<PlanEntity, PlanDto>();
            CreateMap<PlanDto, PlanEntity>();

            //项目计划明细
            //CreateMap<TaskEntity, TaskDto>();
            //CreateMap<TaskDto, TaskEntity>();
        }
    }
}
