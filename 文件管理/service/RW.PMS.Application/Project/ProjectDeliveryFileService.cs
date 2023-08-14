using AutoMapper;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Project;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace RW.PMS.Application.BaseInfo
{
    public class ProjectDeliveryFileService : CrudApplicationService<ProjectDeliveryFileEntity, int>, IProjectDeliveryFileService
    {
        private readonly IContract_InfoService _contract_InfoService;

        public ProjectDeliveryFileService(IDataValidatorProvider dataValidator, IRepository<ProjectDeliveryFileEntity, int> repository,IContract_InfoService contract_InfoService,
            IMapper mapper, Lazy<ICurrentUser> currentUser) : base(dataValidator, repository, mapper, currentUser)
        {
            _contract_InfoService = contract_InfoService;
        }

        public PagedResult<ProjectDeliveryFileDto> GetPagedList(ProjectDeliveryFileSearchDto input)
        {
            var List = Repository.Select.From<Project_DeliveryEntity, ProjectBasicsEntity, ProjectDeviceDetailsEntity, DictionaryEntity>((df, d, b, dd, dic) => df
                 .LeftJoin(t => t.DeliveryID == d.Id)
                 .LeftJoin(t => t.ProjectID == b.Id)
                 .LeftJoin(t => t.DeviceID == dd.Id)
                 .LeftJoin(t => t.DeliveryType == dic.Id))
                .WhereIf(input.ProjectCode.NotNullOrWhiteSpace(), t => t.t3.ProjectCode.Contains(input.ProjectCode))
                .WhereIf(input.ProjectName.NotNullOrWhiteSpace(), t => t.t3.ProjectName.Contains(input.ProjectName))
                .WhereIf(input.DeliveryNo.NotNullOrWhiteSpace(), t => t.t2.DeliveryCode.Contains(input.DeliveryNo))
                .WhereIf(input.DeviceCode.NotNullOrWhiteSpace(), t => t.t4.DeviceNo.Contains(input.DeviceCode))
                .WhereIf(input.DeliveryType != 0, t => t.t5.Id == input.DeliveryType)
                .OrderByDescending(t => t.t1.Id)
                .Count(out var total)
                .Page(input.PageNo, input.PageSize)
                .ToList((df, d, b, dd, dic) => new
                {
                    MainEntity = df,
                    deliCode = d.DeliveryCode,
                    ProCode = b.ProjectCode,
                    ProName = b.ProjectName,
                    DevCode = dd.DeviceNo,
                    deliType = dic.DictionaryText,
                })
                .Select(t =>
                {
                    var info = Mapper.Map<ProjectDeliveryFileDto>(t.MainEntity);
                    info.DeliveryNo = t.deliCode;
                    info.ProjectCode = t.ProCode;
                    info.ProjectName = t.ProName;
                    info.DeviceCode = t.DevCode;
                    info.DeliveryTypeText = t.deliType;
                    return info;
                }).ToList();

            return new PagedResult<ProjectDeliveryFileDto>(Mapper.Map<List<ProjectDeliveryFileDto>>(List), total);
        }
        public List<ProjectDeliveryFileDto> GetByIdList(int Id)
        {
            var List = Repository.Select.From<Project_DeliveryEntity, ProjectBasicsEntity, ProjectDeviceDetailsEntity, DictionaryEntity>((df, d, b, dd, dic) => df
                 .LeftJoin(t => t.DeliveryID == d.Id)
                 .LeftJoin(t => t.ProjectID == b.Id)
                 .LeftJoin(t => t.DeviceID == dd.Id)
                 .LeftJoin(t => t.DeliveryType == dic.Id))
                .Where(t=>t.t1.ProjectID==Id)
                .ToList((df, d, b, dd, dic) => new
                {
                    MainEntity = df,
                    deliCode = d.DeliveryCode,
                    ProCode = b.ProjectCode,
                    ProName = b.ProjectName,
                    DevCode = dd.DeviceNo,
                    deliType = dic.DictionaryText,
                })
                .Select(t =>
                {
                    var info = Mapper.Map<ProjectDeliveryFileDto>(t.MainEntity);
                    info.DeliveryNo = t.deliCode;
                    info.ProjectCode = t.ProCode;
                    info.ProjectName = t.ProName;
                    info.DeviceCode = t.DevCode;
                    info.DeliveryTypeText = t.deliType;
                    return info;
                }).ToList();

            return new List<ProjectDeliveryFileDto>(List);
        }

        public bool IsExist(ProjectDeliveryFileDto input)
        {
            var exist = Repository.Where(t => t.ProjectID == input.ProjectID && t.DeliveryID == input.DeliveryID && t.DeviceID == input.DeviceID && t.DeliveryType == input.DeliveryType).ToOne();
            if (exist == null) return false;
            if (input.Id.HasValue)
            {
                return input.Id.Value != exist.Id;
            }
            return true;
        }

    }
}
