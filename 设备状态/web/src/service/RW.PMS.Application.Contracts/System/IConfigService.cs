using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Configuration;
using RW.PMS.Application.Contracts.Input.System;
using RW.PMS.Application.Contracts.Output.System;
using RW.PMS.Domain.Entities.System;
using System.Collections.Generic;

namespace RW.PMS.Application.Contracts.System
{
    public interface IConfigService : ICrudApplicationService<ConfigEntity, int>
    {
        PagedResult<ConfigListDto> GetPagedList(ConfigSearchDto input);

        ConfigListDto GetConfig(string code);

        List<ConfigTypeDto> GetConfigTypes();

        List<ConfigListDto> GetConfigs(string configType);

        /// <summary>
        /// 通过参数编码查询系统配置值
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        ConfigListDto GetConfigCode(string code);
    }
}