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
    public class ProjectAcceptanceService : CrudApplicationService<ProjectAcceptanceEntity, int>, IProjectAcceptanceService
    {
        private readonly IContract_InfoService _contract_InfoService;

        public ProjectAcceptanceService(IDataValidatorProvider dataValidator, IRepository<ProjectAcceptanceEntity, int> repository,IContract_InfoService contract_InfoService,
            IMapper mapper, Lazy<ICurrentUser> currentUser) : base(dataValidator, repository, mapper, currentUser)
        {
            _contract_InfoService = contract_InfoService;
        }

        public PagedResult<ProjectAcceptanceDto> GetPagedList(ProjectAcceptanceSearchDto input)
        {
            string a = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string[] baseUrl = a.Split("\\bin");
            var list = Repository.Select.From<ProjectBasicsEntity, ProjectDeviceDetailsEntity, DictionaryEntity, UserEntity, FileEntity,DictionaryEntity>(
                (acc, bas, dev, dic, cont,f,dic2) => acc
                 .LeftJoin(t => t.ProjectID == bas.Id)
                 .LeftJoin(t => t.DeviceID == dev.Id)
                 .LeftJoin(t => t.AcceptCategory == dic.Id)
                 .LeftJoin(t => t.Acceptancer == cont.Id)
                 .LeftJoin(t => t.SignConfirmFile == f.Id)
                 .LeftJoin(t => t.AcceptResult == dic2.Id))
                .WhereIf(input.ProjectCode.NotNullOrWhiteSpace(), t => t.t2.ProjectCode.Contains(input.ProjectCode))
                .WhereIf(input.ProjectName.NotNullOrWhiteSpace(), t => t.t2.ProjectName.Contains(input.ProjectName))
                .WhereIf(input.DeviceNo.NotNullOrWhiteSpace(), t => t.t3.DeviceNo.Contains(input.DeviceNo))
                .WhereIf(input.AcceptCategory != 0, t => t.t4.Id == input.AcceptCategory)
                .OrderByDescending(t => t.t1.Id)
                .Count(out var total)
                .Page(input.PageNo, input.PageSize)
                .ToList((acc, bas, dev, dic, cont,f, dic2) => new
                {
                    MainEntity = acc,
                    ProBaseCode = bas.ProjectCode,
                    ProBaseName=bas.ProjectName,
                    deviceNo = dev.DeviceNo,
                    deviceName=dev.DeviceName,
                    accCategory = dic.DictionaryText,
                    accAncer = cont.RealName,
                    fileName=f.FileName,
                    filePath=f.RelativePath,
                    ownerName= bas.OwnerUnit,
                    acceptResultTxt=dic2.DictionaryText,
                })
                .Select(t =>
                {
                    var info = Mapper.Map<ProjectAcceptanceDto>(t.MainEntity);
                    info.ProjectCode = t.ProBaseCode;
                    info.ProjectName = t.ProBaseName;
                    info.DeviceNo = t.deviceNo;
                    info.DeviceName = t.deviceName;
                    info.AcceptCategoryName = t.accCategory;
                    info.AcceptancerName = t.accAncer;
                    info.OwnerUnitName = t.ownerName;
                    info.fileName = t.fileName;
                    info.AcceptResultTxt = t.acceptResultTxt;
                    if (t.filePath.NotNullOrWhiteSpace())
                    {
                        info.SignConfirmFileUrl = baseUrl[0] + t.filePath;
                    }
                    else
                    {
                        info.SignConfirmFileUrl = t.filePath;
                    }
                    return info;
                }).ToList();
            return new PagedResult<ProjectAcceptanceDto>(Mapper.Map<List<ProjectAcceptanceDto>>(list), total);
        }
        public List<ProjectAcceptanceDto> GetByIdList(int Id)
        {
            string a = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string[] baseUrl = a.Split("\\bin");
            var list = Repository.Select.From<ProjectBasicsEntity, ProjectDeviceDetailsEntity, DictionaryEntity, UserEntity, FileEntity, DictionaryEntity>(
                (acc, bas, dev, dic, cont, f, dic2) => acc
                 .LeftJoin(t => t.ProjectID == bas.Id)
                 .LeftJoin(t => t.DeviceID == dev.Id)
                 .LeftJoin(t => t.AcceptCategory == dic.Id)
                 .LeftJoin(t => t.Acceptancer == cont.Id)
                 .LeftJoin(t => t.SignConfirmFile == f.Id)
                 .LeftJoin(t => t.AcceptResult == dic2.Id))
                .Where(t=>t.t1.ProjectID==Id)
                .OrderByDescending(t => t.t1.Id)
                .Count(out var total)
                .ToList((acc, bas, dev, dic, cont, f, dic2) => new
                {
                    MainEntity = acc,
                    ProBaseCode = bas.ProjectCode,
                    ProBaseName = bas.ProjectName,
                    deviceNo = dev.DeviceNo,
                    deviceName = dev.DeviceName,
                    accCategory = dic.DictionaryText,
                    accAncer = cont.RealName,
                    fileName = f.FileName,
                    filePath = f.RelativePath,
                    ownerName = bas.OwnerUnit,
                    acceptResultTxt = dic2.DictionaryText,
                })
                .Select(t =>
                {
                    var info = Mapper.Map<ProjectAcceptanceDto>(t.MainEntity);
                    info.ProjectCode = t.ProBaseCode;
                    info.ProjectName = t.ProBaseName;
                    info.DeviceNo = t.deviceNo;
                    info.DeviceName = t.deviceName;
                    info.AcceptCategoryName = t.accCategory;
                    info.AcceptancerName = t.accAncer;
                    info.OwnerUnitName = t.ownerName;
                    info.fileName = t.fileName;
                    info.AcceptResultTxt = t.acceptResultTxt;
                    if (t.filePath.NotNullOrWhiteSpace())
                    {
                        info.SignConfirmFileUrl = baseUrl[0] + t.filePath;
                    }
                    else
                    {
                        info.SignConfirmFileUrl = t.filePath;
                    }
                    return info;
                }).ToList();
            return new List<ProjectAcceptanceDto>(list);
        }

        public bool IsExist(ProjectAcceptanceDto input)
        {
            var exist = Repository.Where(t => t.ProjectID == input.ProjectID && t.DeviceID==input.DeviceID && t.AcceptCategory==input.AcceptCategory && t.AcceptDate==input.AcceptDate).ToOne();
            if (exist == null) return false;
            if (input.Id.HasValue)
            {
                return input.Id.Value != exist.Id;
            }
            return true;
        }

    }
}
