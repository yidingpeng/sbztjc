using AutoMapper;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Project;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RW.PMS.Application.Project
{
	public class ProjectRetMoneyService : CrudApplicationService<ProjectRetMoneyEntity, int>, IProjectRetMoneyService
	{
		public ProjectRetMoneyService(IDataValidatorProvider dataValidator,
			IRepository<ProjectRetMoneyEntity, int> repository,
			IMapper mapper, Lazy<ICurrentUser> currentUser) : base(
				dataValidator, repository, mapper, currentUser)
		{
		}

        public PagedResult<ProjectRetMoneyOutput> GetPagedList(ProjectRetMoneySearchDto input)
        {
            string[] DateArray = input.WarrantyPeriodT.NotNullOrWhiteSpace() ? input.WarrantyPeriodT.Split("~") : null;
            var list = Repository.Select.From<ProjectBasicsEntity>((a, b) => a
                .LeftJoin(a1 => a1.ProjectID == b.Id))
                .WhereIf(input.ProjectName.NotNullOrWhiteSpace(), (a, b) => b.ProjectName.Contains(input.ProjectName))
                .WhereIf(input.ProjectCode.NotNullOrWhiteSpace(), (a, b) => b.ProjectCode .Contains(input.ProjectCode))
                .OrderByDescending((a, b) => a.Id).Where((a, b) => a.IsDeleted == false)
                .Count(out var total)
                //.Page(input.PageNo, input.PageSize)
                .ToList((a, b) => new
                {
                    contacts = a,
                    projectName = b.ProjectName,
                    projectcode = b.ProjectCode
                }).Select(t =>
                {
                    var Output = Mapper.Map<ProjectRetMoneyOutput>(t.contacts);
                    Output.ProjectName = t.projectName;
                    Output.ProjectCode = t.projectcode;
                    return Output;
                }).ToList();
            if (input.WarrantyPeriodT.NotNullOrWhiteSpace())
            {
                list=list.Where(a => Convert.ToDateTime(a.WarrantyPeriod.Split("~")[0].ToString()) >= Convert.ToDateTime(DateArray[0].ToString())&&
                Convert.ToDateTime(a.WarrantyPeriod.Split("~")[1].ToString()) < Convert.ToDateTime(DateArray[1].ToString()).AddDays(1)).ToList();
            }
            var IEnumerableList = list.Skip((input.PageNo - 1) * input.PageSize).Take(input.PageSize);
            //把IEnumerable类型转换成list类型
            var ResultList = new List<ProjectRetMoneyOutput>(IEnumerableList);
            return new PagedResult<ProjectRetMoneyOutput>(Mapper.Map<List<ProjectRetMoneyOutput>>(ResultList), total);
        }
        /// <summary>
        /// 根据Id获取质保金信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<ProjectRetMoneyOutput> GetByIdList(int Id)
		{
            var list = Repository.Select.From<ProjectBasicsEntity>((a, b) => a
                .LeftJoin(a1 => a1.ProjectID == b.Id))
                .Where((a, b) => a.IsDeleted == false && a.ProjectID == Id)
                .ToList((a, b) => new
                {
                    contacts = a,
                    projectName = b.ProjectName,
                    projectcode = b.ProjectCode
                }).Select(t =>
                {
                    var Output = Mapper.Map<ProjectRetMoneyOutput>(t.contacts);
                    Output.ProjectName = t.projectName;
                    Output.ProjectCode = t.projectcode;
                    return Output;
                }).ToList();
            return new List<ProjectRetMoneyOutput>(list);
        }

        public bool IsExist(ProjectRetMoneyDto input)
        {
            var exist = Repository.Where(t => t.ProjectID == Convert.ToInt32(input.ProjectID)).ToOne();
            if (exist == null) return false;
            if (input.Id.HasValue)
            {
                return input.Id.Value != exist.Id;
            }
            return true;
        }
    }
}
