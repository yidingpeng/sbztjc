using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Configuration;
using RW.PMS.Application.Contracts.Input.System;
using RW.PMS.Application.Contracts.Output.System;
using RW.PMS.Application.Contracts.System;
using RW.PMS.CrossCutting.DataValidator;
using RW.PMS.CrossCutting.Extensions;
using RW.PMS.CrossCutting.Security.User;
using RW.PMS.Domain.Entities.System;
using RW.PMS.Domain.Repositories;

namespace RW.PMS.Application.System
{
    public class ConfigService : CrudApplicationService<ConfigEntity, int>, IConfigService
    {
        public ConfigService(IDataValidatorProvider dataValidator,
                             IRepository<ConfigEntity, int> configRepository,
                             IMapper mapper,
                             Lazy<ICurrentUser> currentUser) :
            base(dataValidator, configRepository, mapper, currentUser)
        {
        }

        public PagedResult<ConfigListDto> GetPagedList(ConfigSearchDto input)
        {
            var list = Repository.Select.From<UserEntity>((c, u) => c.LeftJoin(t => t.CreatedBy == u.Id))
                .WhereIf(input.Key.NotNullOrWhiteSpace(), (r, u) => r.ConfigCode.Contains(input.Key))
                .Count(out var total).Page(input.PageNo, input.PageSize).ToList((c, u) => new
                {
                    Config = c,
                    CreatedUser = u.RealName
                }).Select(t =>
                {
                    var configListOutput = Mapper.Map<ConfigListDto>(t.Config);
                    configListOutput.LastModifyedUser = t.CreatedUser;
                    return configListOutput;
                }).ToList();
            return new PagedResult<ConfigListDto>(list, total);
        }

        public ConfigListDto GetConfig(string code) =>
            Mapper.Map<ConfigListDto>(Repository.Select.WhereIf(code.NotNullOrWhiteSpace(), r => r.ConfigCode.Contains(code)).ToList().FirstOrDefault());

        //public bool ConfigCodeExist(ConfigInput input)
        //{
        //    var exist = Repository.Where(t => t.ConfigCode == input.ConfigCode).ToOne();
        //    if (exist == null) return false; 
        //    if (input.Id.HasValue)
        //    {
        //        return input.Id.Value != exist.Id; 
        //    }
        //    return true;
        //}

        public List<ConfigTypeDto> GetConfigTypes()
        {
            return Repository.Select.GroupBy(x => x.ConfigType).Select(x => x.Key).ToList().Select(x => new ConfigTypeDto { Name = x }).ToList();
        }

        public List<ConfigListDto> GetConfigs(string configType)
        {
            return Repository.Select
                .Where(x => x.ConfigType == configType)
                .ToList()
                .Select(x => Mapper.Map<ConfigListDto>(x)).ToList();
        }
        public ConfigListDto GetConfigCode(string code) =>
            Mapper.Map<ConfigListDto>(Repository.Select.WhereIf(code.NotNullOrWhiteSpace(), r => r.ConfigCode ==code).ToList().FirstOrDefault());
    }
}