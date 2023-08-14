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
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Project
{
    public class ProjectDeviceDetailsService:CrudApplicationService<ProjectDeviceDetailsEntity, int>, IProjectDeviceDetailsService
    {
        public ProjectDeviceDetailsService(IDataValidatorProvider dataValidator,
            IRepository<ProjectDeviceDetailsEntity, int> repository,
            IMapper mapper, Lazy<ICurrentUser> currentUser) : base(dataValidator, repository, mapper, currentUser)
        {
            
        }
        /// <summary>
        /// 项目设备信息分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<ProjectDeviceDetailsView> PagedList(ProjectDeviceDetailsSearchDto input)
        {
            var list = Repository.Orm.Select<ProjectDeviceDetailsEntity,ProjectBasicsEntity, DictionaryEntity>()
                .LeftJoin(a1 => a1.t1.ProjectID == a1.t2.Id)
                .LeftJoin(a2 => a2.t1.ProductLine == a2.t3.Id)
                .WhereIf(input.DeviceName.NotNullOrWhiteSpace(), a => a.t1.DeviceName.Contains(input.DeviceName))
                .WhereIf(input.DeviceNo.NotNullOrWhiteSpace(), a => a.t1.DeviceNo .Contains(input.DeviceNo ))
                .WhereIf(input.ProjectCode .NotNullOrWhiteSpace(), a => a.t2.ProjectCode .Contains(input.ProjectCode ))
                .Where(a => a.t1.IsDeleted == false)
                .OrderByDescending(a => a.t1.Id)
                .Count(out var total)
                .Page(input.PageNo, input.PageSize)
                .ToList(t => new
                {
                    project = t.t1,
                    Dictionary1 = t.t2,
                    Dictionary2 = t.t3,
                }).Select(t =>
                {
                    var Output = Mapper.Map<ProjectDeviceDetailsView>(t.project);
                    Output.ProjectName = (t.Dictionary1 == null) ? "" : t.Dictionary1.ProjectName;
                    Output.ProjectCode = (t.Dictionary1 == null) ? "" : t.Dictionary1.ProjectCode;
                    Output.ProductLineName = (t.Dictionary2 == null) ? "" : t.Dictionary2.DictionaryText;
                    return Output;
                }).ToList();
            return new PagedResult<ProjectDeviceDetailsView>(Mapper.Map<List<ProjectDeviceDetailsView>>(list), total);
        }

        /// <summary>
        /// 根据项目ID查询设备信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ProjectDeviceDetailsView> GetByProIDList(int ProjectID)
        {
            var list = Repository.Orm.Select<ProjectDeviceDetailsEntity, ProjectBasicsEntity, DictionaryEntity>()
                .LeftJoin(a1 => a1.t1.ProjectID == a1.t2.Id)
                .LeftJoin(a2 => a2.t1.ProductLine == a2.t3.Id)
                .WhereIf(ProjectID > 0, a => a.t1.ProjectID== ProjectID)
                .Where(a => a.t1.IsDeleted == false)
                .OrderByDescending(a => a.t1.Id)
                .ToList(t => new
                {
                    project = t.t1,
                    Dictionary1 = t.t2,
                    Dictionary2 = t.t3,
                }).Select(t =>
                {
                    var Output = Mapper.Map<ProjectDeviceDetailsView>(t.project);
                    Output.ProjectName = (t.Dictionary1 == null) ? "" : t.Dictionary1.ProjectName;
                    Output.ProductLineName = (t.Dictionary2 == null) ? "" : t.Dictionary2.DictionaryText;
                    return Output;
                }).ToList();
            return new List<ProjectDeviceDetailsView>(list);
        }

        /// <summary>
        /// 根据多个项目ID查询所有设备
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<ProjectDeviceDetailsView> GetIdsList(int[] intarrs)
        {
            var list = Repository.Orm.Select<ProjectDeviceDetailsEntity, ProjectBasicsEntity, DictionaryEntity>()
                .LeftJoin(a1 => a1.t1.ProjectID == a1.t2.Id)
                .LeftJoin(a2 => a2.t1.ProductLine == a2.t3.Id)
                .Where(a =>  intarrs.Contains(a.t1.ProjectID))
                .ToList(t => new
                {
                    project = t.t1,
                    Dictionary1 = t.t2,
                    Dictionary2 = t.t3,
                }).Select(t =>
                {
                    var Output = Mapper.Map<ProjectDeviceDetailsView>(t.project);
                    Output.ProjectCode = (t.Dictionary1 == null) ? "" : t.Dictionary1.ProjectCode;
                    Output.ProjectName = (t.Dictionary1 == null) ? "" : t.Dictionary1.ProjectName;
                    Output.ProductLineName = (t.Dictionary2 == null) ? "" : t.Dictionary2.DictionaryText;
                    return Output;
                }).ToList();
            return new List<ProjectDeviceDetailsView>(list);
        }

        /// <summary>
        /// 项目设别信息新增
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Insert(ProjectDeviceDetailsView input)
        {
            var entity = Mapper.Map<ProjectDeviceDetailsEntity>(input);

            base.Insert(entity);
            return true;
        }

        /// <summary>
        /// 项目设备信息修改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Update(ProjectDeviceDetailsView input)
        {
            base.Update(input.Id, input);
            return true;
        }
        /// <summary>
        /// 设备编号唯一
        /// </summary>
        /// <param name="ProjectCode"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool ProOnly(string DeviceNo, int Id)
        {
            var list = Repository.Select.Where(t => t.DeviceNo.Equals(DeviceNo) && t.Id != Id).ToList();
            return list.Count > 0;
        }
    }
}
