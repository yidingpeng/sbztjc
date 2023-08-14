using RW.PMS.Domain.Entities.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public interface IStageService : ICrudApplicationService<ConfigureStageEntity, int>
    {
        /// <summary>
        /// 获取项目模板的配置阶段
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <returns></returns>
        List<ConfigureStageDto> GetList(int TemplateId);
    }
}
