using AutoMapper;
using RW.PMS.Application.Contracts.DTO.Plan.Log;
using RW.PMS.Application.Contracts.Plan;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Plan;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Plan
{
    public class PlanLogService : CrudApplicationService<PlanLogEntity, int>, IPlanLogService
    {
        public PlanLogService(IDataValidatorProvider dataValidator,
                              IRepository<PlanLogEntity, int> repository,
                              IMapper mapper,
                              Lazy<ICurrentUser> currentUser) : base(dataValidator, repository, mapper, currentUser)
        {
        }

        public int AddLog(AddPlanLogDto log)
        {
            var entity = this.Insert(log);
            return entity.Id;
        }

        public int ClearLog()
        {
            return Repository.Delete(x => true);
        }

        public List<PlanLogListDto> GetLogs(PlanLogSearchDto search)
        {
            return Repository.Where(x => true)
                .OrderByDescending(x => x.Id)
                .ToList()
                .Select(x => Mapper.Map<PlanLogListDto>(x))
                .ToList();
        }
    }
}
