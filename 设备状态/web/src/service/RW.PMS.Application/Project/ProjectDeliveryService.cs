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
using System.Linq;

namespace RW.PMS.Application.BaseInfo
{
    public class ProjectDeliveryService: CrudApplicationService<Project_DeliveryEntity, int>, IProjectDeliveryService
    {
        public ProjectDeliveryService(IDataValidatorProvider dataValidator,
            IRepository<Project_DeliveryEntity, int> repository, IMapper mapper, Lazy<ICurrentUser> currentUser) : base(
            dataValidator, repository, mapper, currentUser)
        {

        }

        #region 项目交付信息
        /// <summary>
        /// 项目交付分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<Project_DeliveryDto> GetPagedList(Project_DeliverySearchDto input)
        {
            string a = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string[] baseUrl = a.Split("\\bin");
            var list = Repository.Select.From<ProjectBasicsEntity, DictionaryEntity, FileEntity>((a, b, c, f) => a
                 .LeftJoin(a1 => a1.ProjectID == b.Id)
                 .LeftJoin(a1 => a1.AcceptanceCertificate == f.Id)
                 .LeftJoin(a2 => a2.AcceptancePhase == c.Id))
                .WhereIf(input.projectName.NotNullOrWhiteSpace(), (a, b, c, f) => b.ProjectName.Contains(input.projectName))
                .WhereIf(input.DeliveryCode.NotNullOrWhiteSpace(), (a, b, c, f) => a.DeliveryCode.Contains(input.DeliveryCode))
                 .OrderByDescending((a, b, c, f) => a.Id).Where((a, b, c, f) => a.IsDeleted == false)
                .Count(out var total).Page(input.PageNo, input.PageSize).ToList((a, b, c, f) => new
                {
                    delivery = a,
                    projectName = b.ProjectName,
                    projectCode=b.ProjectCode,
                    ProReceiveDate=b.ProjectReceiveDate,
                    AcceptancePhaseName = c.DictionaryText,
                    path = f.RelativePath,
                    filename = f.FileName
                }).Select(t =>
                {
                    var Output = Mapper.Map<Project_DeliveryDto>(t.delivery);
                    Output.ProjectName = t.projectName;
                    Output.AcceptancePhaseName = t.AcceptancePhaseName;
                    Output.fileName = t.filename;
                    Output.ProjectCode = t.projectCode;
                    Output.ProjectReceiveDate = t.ProReceiveDate;
                    if (t.path.NotNullOrWhiteSpace())
                    {
                        Output.AcceptanceCertificateUrl = baseUrl[0] + t.path;
                    }
                    else
                    {
                        Output.AcceptanceCertificateUrl = t.path;
                    }
                    return Output;
                }).ToList();
            return new PagedResult<Project_DeliveryDto>(list, total);
        }

        /// <summary>
        /// 根据ID获取交付信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<Project_DeliveryDto> GetByIdList(int Id)
        {
            string a = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string[] baseUrl = a.Split("\\bin");
            var list = Repository.Select.From<ProjectBasicsEntity, DictionaryEntity, FileEntity>((a, b, c, f) => a
                 .LeftJoin(a1 => a1.ProjectID == b.Id)
                 .LeftJoin(a1 => a1.AcceptanceCertificate == f.Id)
                 .LeftJoin(a2 => a2.AcceptancePhase == c.Id))
                .WhereIf(Id>0, a=>a.t1.ProjectID==Id)
                .ToList((a, b, c, f) => new
                {
                    delivery = a,
                    projectName = b.ProjectName,
                    projectCode = b.ProjectCode,
                    AcceptancePhaseName = c.DictionaryText,
                    path = f.RelativePath,
                    filename = f.FileName
                }).Select(t =>
                {
                    var Output = Mapper.Map<Project_DeliveryDto>(t.delivery);
                    Output.ProjectName = t.projectName;
                    Output.AcceptancePhaseName = t.AcceptancePhaseName;
                    Output.fileName = t.filename;
                    Output.ProjectCode = t.projectCode;
                    if (t.path.NotNullOrWhiteSpace())
                    {
                        Output.AcceptanceCertificateUrl = baseUrl[0] + t.path;
                    }
                    else
                    {
                        Output.AcceptanceCertificateUrl = t.path;
                    }
                    return Output;
                }).ToList();
            return new List<Project_DeliveryDto>(list);
        }

        public bool IsExist(Project_DeliveryDto input)
        {
            var exist = Repository.Where(t => t.DeliveryCode == input.DeliveryCode).ToOne();
            if (exist == null) return false;
            if (input.Id.HasValue)
            {
                return input.Id.Value != exist.Id;
            }
            return true;
        }
        #endregion
    }
}
