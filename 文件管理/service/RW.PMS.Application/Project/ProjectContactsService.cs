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
    public class ProjectContactsService : CrudApplicationService<Project_ContactsInfo, int>, IProjectContactsService
    {
        public ProjectContactsService(IDataValidatorProvider dataValidator,
            IRepository<Project_ContactsInfo, int> repository, IMapper mapper, Lazy<ICurrentUser> currentUser) : base(
            dataValidator, repository, mapper, currentUser)
        {

        }

        #region 项目联系人信息
        /// <summary>
        /// 项目联系人分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResult<projectcontactsListDto> GetPagedList(projectcontactsSearchDto input)
        {
            var list = Repository.Select.From<ProjectBasicsEntity, UserEntity, DictionaryEntity, OrganizationEntity, UserExtenderEntity>
                ((pc, pb, ce, de, oe,ue) => pc
                  .LeftJoin(t => t.PID == pb.Id)
                  .LeftJoin(t => t.ContactsID == ce.Id)
                  .LeftJoin(t => t.FZBKId == de.Id))
                .LeftJoin(t => t.t3.OrgId == t.t5.Id)
                .LeftJoin(t => t.t3.Id == t.t6.UserId)
                //.LeftJoin(t => t.t3.ContactsCategory == t.t6.Id)
                //.LeftJoin(t=>t.t3.CompanyId==t.t6.Id)
                .WhereIf(input.ContactsName.NotNullOrWhiteSpace(), (pc, pb, ce, de, oe,ue) => ce.Account.Contains(input.ContactsName))
                .WhereIf(input.projectCode.NotNullOrWhiteSpace(), (pc, pb, ce, de, oe, ue) => pb.ProjectCode.Contains(input.projectCode))
                .WhereIf(input.PName.NotNullOrWhiteSpace(), (pc, pb, ce, de, oe, ue) => pb.ProjectName.Contains(input.PName))
                .OrderByDescending((pc, pb, ce, de, oe,ue) => pc.Id).Where((pc, pb, ce, de, oe,ue) => pc.IsDeleted == false)
                .Count(out var total).Page(input.PageNo, input.PageSize).ToList(t => new
                {
                    Procontacts = t.t1,
                    proName = t.t2.ProjectName,
                    proCode = t.t2.ProjectCode,
                    conid=t.t3.Id,
                    ContactsName = t.t3.Account,
                    Phone=t.t6.Telnum,
                    sex=t.t6.Sex,
                    ChargeBrand = t.t4.DictionaryText,
                    DepName = t.t5.OrganizationName,
                    //contactsTypeName = t.t6.DictionaryText,
                    //cuCompany=t.t6.ClientFullName
                }).Select(t =>
                {
                    var output = Mapper.Map<projectcontactsListDto>(t.Procontacts);
                    output.PName = t.proName;
                    output.projectCode = t.proCode;
                    output.ContactsName = t.ContactsName;
                    output.Telephone1 = t.Phone;
                    output.FZBKName = t.ChargeBrand;
                    output.DeptName = t.DepName;
                    output.sex = t.sex;
                    output.Roles =string.Join(',',
                                Repository.Orm.Select<RoleEntity, UserRoleEntity>()
                                .LeftJoin(w => w.t1.Id == w.t2.RoleId)
                                .Where(w => w.t2.UserId == t.conid)
                                .ToList(a => a.t1.RoleName));
                    //output.CurCompany = t.cuCompany;
                    return output;
                }).ToList();
            return new PagedResult<projectcontactsListDto>(list, total);
        }

        public List<projectcontactsListDto> GetAllList()
        {
            var list = Repository.Select.From<ProjectBasicsEntity, UserEntity, DictionaryEntity, OrganizationEntity, UserExtenderEntity>
                ((pc, pb, ce, de, oe,ue) => pc
                  .LeftJoin(t => t.PID == pb.Id)
                  .LeftJoin(t => t.ContactsID == ce.Id)
                  .LeftJoin(t => t.FZBKId == de.Id))
                .LeftJoin(t => t.t3.OrgId == t.t5.Id)
                .LeftJoin(t => t.t3.Id == t.t6.UserId)
                .OrderByDescending((pc, pb, ce, de, oe,ue) => pc.Id)
                .ToList(t => new
                {
                    Procontacts = t.t1,
                    proName = t.t2.ProjectName,
                    proCode = t.t2.ProjectCode,
                    ContactsName = t.t3.Account,
                    Phone = t.t6.Telnum,
                    ChargeBrand = t.t4.DictionaryText,
                    DepName = t.t5.OrganizationName,
                    sex=t.t6.Sex,
                    //contactsTypeName = t.t6.DictionaryText,
                    //cuCompany = t.t6.ClientFullName
                }).Select(t =>
                {
                    var output = Mapper.Map<projectcontactsListDto>(t.Procontacts);
                    output.PName = t.proName;
                    output.projectCode = t.proCode;
                    output.ContactsName = t.ContactsName;
                    output.Telephone1 = t.Phone;
                    output.FZBKName = t.ChargeBrand;
                    output.DeptName = t.DepName;
                    output.sex = t.sex;
                    //output.ContactsTypeName = t.contactsTypeName;
                    //output.CurCompany = t.cuCompany;
                    return output;
                }).ToList();
            return new List<projectcontactsListDto>(list);
        }

        /// <summary>
        /// 根据项目ID查询项目联系人
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<projectcontactsListDto> GetByIdList(int Id)
        { 
            var list = Repository.Select.From<ProjectBasicsEntity, UserEntity, DictionaryEntity, OrganizationEntity, UserExtenderEntity>
                ((pc, pb, ce, de, oe, ue) => pc
                  .LeftJoin(t => t.PID == pb.Id)
                  .LeftJoin(t => t.ContactsID == ce.Id)
                  .LeftJoin(t => t.FZBKId == de.Id))
                .LeftJoin(t => t.t3.OrgId == t.t5.Id)
                .LeftJoin(t => t.t3.Id == t.t6.UserId)
                .OrderByDescending((pc, pb, ce, de, oe, ue) => pc.Id)
                .Where(t => t.t1.PID == Id)
                .ToList(t => new
                {
                    Procontacts = t.t1,
                    proName = t.t2.ProjectName,
                    proCode = t.t2.ProjectCode,
                    ContactsName = t.t3.Account,
                    Phone = t.t6.Telnum,
                    ChargeBrand = t.t4.DictionaryText,
                    DepName = t.t5.OrganizationName,
                    sex=t.t6.Sex,
                }).Select(t =>
                {
                    var output = Mapper.Map<projectcontactsListDto>(t.Procontacts);
                    output.PName = t.proName;
                    output.projectCode = t.proCode;
                    output.ContactsName = t.ContactsName;
                    output.Telephone1 = t.Phone;
                    output.FZBKName = t.ChargeBrand;
                    output.DeptName = t.DepName;
                    output.sex = t.sex;
                    return output;
                }).ToList();
            return new List<projectcontactsListDto>(list);
        }

        public List<projectcontactsListDto> GetConListById(int Id)
        {
            var list = Repository.Select.From<ProjectBasicsEntity, UserEntity, DictionaryEntity, OrganizationEntity, UserExtenderEntity>
                 ((pc, pb, ce, de, oe, ue) => pc
                   .LeftJoin(t => t.PID == pb.Id)
                   .LeftJoin(t => t.ContactsID == ce.Id)
                   .LeftJoin(t => t.FZBKId == de.Id))
                 .LeftJoin(t => t.t3.OrgId == t.t5.Id)
                 .LeftJoin(t => t.t3.Id == t.t6.UserId)
                 .OrderByDescending((pc, pb, ce, de, oe, ue) => pc.Id)
                 .Where(t => t.t1.Id == Id)
                 .ToList(t => new
                 {
                     Procontacts = t.t1,
                     proName = t.t2.ProjectName,
                     proCode = t.t2.ProjectCode,
                     ContactsName = t.t3.Account,
                     Phone = t.t6.Telnum,
                     ChargeBrand = t.t4.DictionaryText,
                     DepName = t.t5.OrganizationName,
                     sex=t.t6.Sex,
                 }).Select(t =>
                 {
                     var output = Mapper.Map<projectcontactsListDto>(t.Procontacts);
                     output.PName = t.proName;
                     output.projectCode = t.proCode;
                     output.ContactsName = t.ContactsName;
                     output.Telephone1 = t.Phone;
                     output.FZBKName = t.ChargeBrand;
                     output.DeptName = t.DepName;
                     output.sex = t.sex;
                     return output;
                 }).ToList();
            return new List<projectcontactsListDto>(list);
        }

        /// <summary>
        /// 添加项目联系人
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Insert(projectcontactsListDto input)
        {
            var entity = Mapper.Map<Project_ContactsInfo>(input);
            base.Insert(entity);
            return true;
        }
        /// <summary>
        /// 修改项目联系人
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool Update(projectcontactsListDto input)
        {
            base.Update(input.Id.Value, input);
            return true;
        }

        public bool IsExist(projectcontactsListDto input)
        {
            var exist = Repository.Where(t => t.PID == input.PID && t.FZBKId == input.FZBKId && t.ContactsID == input.ContactsID).ToOne();
            if (exist == null) return false;
            if (input.Id.HasValue)
            {
                return input.Id.Value != exist.Id;
            }
            return true;
        }
        /// <summary>
        /// 获取项目团队信息
        /// </summary>
        /// <returns></returns>
        public List<projectcontactsListDto> GetTeamMessagePush()
        {
            var list = Repository.Select.From<ProjectBasicsEntity, UserEntity>((a, b, c) => a
                .LeftJoin(a1 => a1.PID == b.Id)
                .LeftJoin(a1 => a1.ContactsID == c.Id))
                .OrderByDescending((a, b, c) => a.PID)
                .ToList(t => new
                {
                    team = t.t1,
                    proName = t.t2.ProjectName,
                    proCode = t.t2.ProjectCode,
                    AccountName = t.t3.Account,
                }).Select(t =>
                {
                    var output = Mapper.Map<projectcontactsListDto>(t.team);
                    output.PName = t.proName;
                    output.projectCode = t.proCode;
                    output.ContactsName = t.AccountName;
                    return output;
                }).ToList();
            return new List<projectcontactsListDto>(list);
        }
        #endregion
    }
}
