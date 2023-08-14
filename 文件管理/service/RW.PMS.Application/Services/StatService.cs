using AutoMapper;
using RW.PMS.Application.Contracts.DTO.Stat;
using RW.PMS.Application.Contracts.Service;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.EventBus;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.Stat;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Entities.Workflow;
using RW.PMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Services
{
    public class StatService : CrudApplicationService<StatDataEntity, int>, IStatService
    {
        readonly IRepository<UserEntity, int> userRepo;
        readonly IRepository<UserFlowEntity, int> userFlowRepo;
        readonly IEventBus _eventBus;

        public StatService(
            IDataValidatorProvider dataValidator,
            IRepository<StatDataEntity, int> repository,
            IRepository<UserEntity, int> userRepo,
            IRepository<UserFlowEntity, int> userFlowRepo,
            IMapper mapper,
            IEventBus eventBus,
        Lazy<ICurrentUser> currentUser) : base(dataValidator, repository, mapper, currentUser)
        {
            this.userRepo = userRepo;
            this.userFlowRepo = userFlowRepo;
            this._eventBus = eventBus;
        }

        public int GetStat(DateTime now, string type, string key)
        {
            var dic = GetStats(now, type);
            if (dic.ContainsKey(key))
                return dic[key];
            return 0;
        }

        public Dictionary<string, int> GetStats(DateTime date, string type)
        {
            var dic = Repository
                .Where(x => x.Date == date.Date && x.Type == type)
                .ToList()
                .ToDictionary(x => x.Key, x => x.Value);
            if (type == "User")
            {
                dic["Total"] = (int)userRepo.Select.Count();
            }
            return dic;
        }

        public UserStatDto GetUserStat()
        {
            var now = DateTime.Now.Date;
            //var dic = GetStats(DateTime.Now, "User");
            int weekday = (int)DateTime.Now.DayOfWeek;
            if (weekday == 0) weekday = 6;
            else weekday -= 1;
            var week = DateTime.Now.Date.AddDays(weekday);
            return new UserStatDto
            {
                Total = (int)userRepo.Select.Count(),
                ActiveToday = (int)Repository.Where(x => x.Date == now && x.Type == "User" && x.Key == "LoginCount").Sum(x => x.Value),
                ActiveWeek = (int)Repository.Where(x => x.Date >= week && x.Type == "User" && x.Key == "LoginCount").Sum(x => x.Value)
            };
        }

        public WorkflowStatDto GetWorkflowStat()
        {
            var now = DateTime.Now.Date;

            return new WorkflowStatDto
            {
                Total = (int)userFlowRepo.Select.Count(),
                Added = (int)Repository.Where(x => x.Date == now && x.Type == "Workflow" && x.Key == "WorkflowCount").Sum(x => x.Value),
                Executed = (int)Repository.Where(x => x.Date == now && x.Type == "Workflow" && x.Key == "WorkflowExecuted").Sum(x => x.Value)
            };
        }

        public bool SetStat(DateTime date, string type, string key, int value = 1)
        {
            var entity = Repository.Where(x => x.Date == date.Date && x.Type == type && x.Key == key).First();
            if (entity == null)
            {
                entity = new StatDataEntity
                {
                    Date = date.Date,
                    Type = type,
                    Key = key,
                };
            }
            entity.Value += value;
            var data = Repository.InsertOrUpdate(entity);
            return data.Id > 0;
        }

    }
}
