using AutoMapper;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Application.Contracts.Project;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Project;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Project
{
    public class ContractDetailService :CrudApplicationService<ContractDetailEntity, int>, IContractDetailService
    {
        public ContractDetailService(IDataValidatorProvider dataValidator, IRepository<ContractDetailEntity, int> repository,
            IMapper mapper, Lazy<ICurrentUser> currentUser) : base(dataValidator, repository, mapper, currentUser)
        {
        }
        /// <summary>
        /// 根据父级ID获取子集项目数据
        /// </summary>
        /// <param name="PID"></param>
        /// <returns></returns>
        public List<ContractDetailDto> GetByPIDList(int? PID)
        {
            var list = Repository.Select
                .From<ProjectBasicsEntity>((a, b) => a
                .LeftJoin(a => a.ProjectId == b.ProjectCode))
                .Where(x => x.t1.PID == PID)
                .ToList(a => new
                {
                    all = a.t1,
                    project = a.t2,
                })
                .Select(t =>
                {
                    var mapp = Mapper.Map<ContractDetailDto>(t.all);
                    mapp.ProjectName = t.project != null ? t.project.ProjectName : "";
                    return mapp;
                }).ToList();
            return list;
        }
    }
}
