using AutoMapper;
using RW.PMS.Application.Contracts.DTO.Organization;
using RW.PMS.Application.Contracts.DTO.Role;
using RW.PMS.Application.Contracts.DTO.Module;
using RW.PMS.Application.Contracts.DTO.User;
using RW.PMS.Application.Contracts.Input.System;
using RW.PMS.Application.Contracts.Output.System;
using RW.PMS.CrossCutting.Security.Encrypt;
using RW.PMS.Domain.Entities.System;
using System.Linq;
using RW.PMS.Application.Contracts.DTO.Configuration;
using RW.PMS.Application.Contracts.DTO.Log;
using RW.PMS.Application.Contracts.DTO.Notice;
using System;
using RW.PMS.Domain.Entities.Message;
using RW.PMS.Application.Contracts.DTO.Message;
using RW.PMS.Application.Contracts.DTO.File;
using RW.PMS.Domain.Entities.Project;

namespace RW.PMS.Application.Contracts.Mapper
{
    public class SystemProfile : Profile
    {
        public SystemProfile()
        {
            //************导航菜单
            CreateMap<ModuleDetailDto, ModuleEntity>();
            CreateMap<ModuleEntity, ModuleListDto>();
            CreateMap<ModuleEntity, ModuleWithOperationDto>();
            CreateMap<ModuleEntity, RouterOutput>()
                .ForMember(t => t.Name, opt => opt.MapFrom(s => s.ModuleName))
                .ForPath(t => t.Meta.Title, opt => opt.MapFrom(s => s.Title))
                .ForPath(t => t.Meta.Hidden, opt => opt.MapFrom(s => s.Hidden))
                .ForPath(t => t.Meta.Icon, opt => opt.MapFrom(s => s.Icon));
            CreateMap<OperationEntity, OperationListDto>();
            CreateMap<OperationEntity, OperationDto>();

            //************角色映射
            CreateMap<RoleEntity, RoleListDto>()
                .ForMember(t => t.Role, opt => opt.MapFrom(s => s.RoleName))
                .ForMember(t => t.BtnRolesCheckedList, opt => opt.MapFrom(s => ToArray(s.BtnRolesCheckedList)))
                ;
            CreateMap<RoleDetailDto, RoleEntity>()
                .ForMember(t => t.RoleName, opt => opt.MapFrom(s => s.Role))
                .ForMember(t => t.BtnRolesCheckedList, opt => opt.MapFrom(s => ToArrayString(s.BtnRolesCheckedList)))
                ;

            //************用户映射
            CreateMap<UserDetailDto, UserEntity>()
                .ForMember(t => t.RealName, opt => opt.MapFrom(s => s.Fullname))
                .ForMember(t => t.Password, opt => opt.MapFrom(s => Md5(s.Password)))//将前端上传的密码直接使用md5加密映射，是否职责不单一了？
                ;
            CreateMap<UserListDto, UserEntity>()
                .ForMember(t => t.RealName, opt => opt.MapFrom(s => s.Fullname))
                ;
            CreateMap<UserEntity, UserListDto>()
                .ForMember(t => t.Fullname, opt => opt.MapFrom(s => s.RealName))
                .ForMember(t => t.OrgId, opt => opt.Ignore());
            CreateMap<UserListDto, UserExtenderEntity>();
            CreateMap<UserExtenderEntity, UserListDto>();
            CreateMap<UserinfoDto, UserExtenderEntity>();
            CreateMap<PersonalInputDto, UserExtenderEntity>();
            CreateMap<UserExtenderEntity, PersonalInfoDto>();
            CreateMap<UserEntity, PersonalUserDto>();


            CreateMap<OperationInput, OperationEntity>();
            CreateMap<OperationEntity, OperationOutput>();

            //************系统配置
            CreateMap<ConfigEntity, ConfigListDto>()
                .ForMember(t => t.Code, opt => opt.MapFrom(s => s.ConfigCode))
                .ForMember(t => t.Type, opt => opt.MapFrom(s => s.ConfigType))
                .ForMember(t => t.Desc, opt => opt.MapFrom(s => s.Description))
                .ForMember(t => t.Value, opt => opt.MapFrom(s => s.ConfigValue))
                ;
            CreateMap<ConfigListDto, ConfigEntity>()
                .ForMember(t => t.ConfigCode, opt => opt.MapFrom(s => s.Code))
                .ForMember(t => t.ConfigType, opt => opt.MapFrom(s => s.Type))
                .ForMember(t => t.Description, opt => opt.MapFrom(s => s.Desc))
                .ForMember(t => t.ConfigValue, opt => opt.MapFrom(s => s.Value))
                ;

            //************组织机构，部门
            CreateMap<OrganizationEntity, OrganizationListDto>()
                .ForMember(t => t.Name, opt => opt.MapFrom(s => s.OrganizationName))
                .ForMember(t => t.Code, opt => opt.MapFrom(s => s.OrganizationCode))
                .ForMember(t => t.Desc, opt => opt.MapFrom(s => s.Description))
                ;
            CreateMap<OrganizationDetailDto, OrganizationEntity>()
                .ForMember(t => t.OrganizationName, opt => opt.MapFrom(s => s.Name))
                .ForMember(t => t.OrganizationCode, opt => opt.MapFrom(s => s.Code))
                .ForMember(t => t.Description, opt => opt.MapFrom(s => s.Desc))
                ;

            //******************字典管理
            CreateMap<DictionaryEntity, DictionaryOutput>()
                .ForMember(t => t.HaveChild, opt => opt.MapFrom(s => s.ParentId == 0));
            CreateMap<DictionaryInput, DictionaryEntity>();
            CreateMap<DictionaryEntity, DictionaryView>();


            //****************日志管理
            CreateMap<LogEntity, LogOutputDto>();
            CreateMap<LogOutputDto, LogEntity>();
            CreateMap<LogInputDto, LogOutputDto>()
                .ForMember(t => t.ExecuteResult, opt => opt.MapFrom(s => s.Message));

            //************系统通知
            CreateMap<SystemNoticeEntity, NoticeOutputDto>()
                .ForMember(t => t.Time, opt => opt.MapFrom(s => s.CreatedAt))
                .ForMember(t => t.Users, opt => opt.MapFrom(s => ToArray(s.Users)))
                .ForMember(t => t.UpdateTime, opt => opt.MapFrom(s => s.LastModifiedAt))
                ;
            CreateMap<NoticeEditDto, SystemNoticeEntity>()
                .ForMember(t => t.Users, opt => opt.MapFrom(s => ToArrayString(s.Users)));

            //************个人消息
            CreateMap<MessageContentEntity, MessageContentOutputDto>()
                .ForMember(t => t.Time, opt => opt.MapFrom(s => s.CreatedAt))
                ;
            CreateMap<MessageSendInputDto, MessageContentEntity>();
            CreateMap<MessageContentEntity, MessageTopOutput>()
                .ForMember(t => t.Time, opt => opt.MapFrom(s => s.CreatedAt));
            CreateMap<MessageContentEntity, MessageDetailOutputDto>()
                .ForMember(t => t.Time, opt => opt.MapFrom(s => s.CreatedAt));

            //
            CreateMap<FileEntity, FileListDto>().ForMember(t => t.UploadTime, opt => opt.MapFrom(s => s.CreatedAt));

            CreateMap<FileEntity, FileModelDto>();
            CreateMap<ContractInfoFilesEntity, FileModelDto>();
            CreateMap<pro_basics_filesEntity, FileModelDto>();
        }

        static string[] ToArray(string arr)
        {
            if (string.IsNullOrEmpty(arr))
                return null;
            return arr.Split(',');
        }

        static string ToArrayString(string[] arr)
        {
            if (arr == null || arr.Length == 0) return null;
            return arr.Aggregate((a, b) => a + "," + b);
        }

        static string ToArrayString<T>(T[] arr)
        {
            if (arr == null || arr.Length == 0) return null;
            return arr.Select(x => Convert.ToString(x)).Aggregate((a, b) => a + "," + b);
        }


        static string Md5(string text)
        {
            return EncryptProvider.Md5(text);
        }
    }
}
