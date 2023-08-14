using AutoMapper;
using RW.PMS.Application.Contracts.DTO.Project;
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
    public class StageService : CrudApplicationService<ConfigureStageEntity, int>, IStageService
    {
        public StageService(IDataValidatorProvider dataValidator,
           IRepository<ConfigureStageEntity, int> repository, IMapper mapper, Lazy<ICurrentUser> currentUser) : base(
           dataValidator, repository, mapper, currentUser)
        {

        }
        /// <summary>
        /// 获取项目模板的配置阶段
        /// </summary>
        /// <param name="TemplateId"></param>
        /// <returns></returns>
        public List<ConfigureStageDto> GetList(int TemplateId)
        {
            var list = Repository
                        .Where(o => o.TemplateId == TemplateId)
                        .OrderBy(o => o.Sort)
                        .ToList()
                        .Select(x => Mapper
                        .Map<ConfigureStageDto>(x))
                        .ToList();
            return list;
        }

        //public bool SortEdit(List<ConfigureStageDto> list)
        //{
        //    try
        //    {
        //        if (list.Count == 2)
        //        {
        //            int sort1,
        //                sort2;
        //            sort1 = list[0].Sort;
        //            sort2 = list[1].Sort;
        //            list[0].Sort = sort2;
        //            list[1].Sort = sort1;
        //            Repository.Update(list[0]);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
