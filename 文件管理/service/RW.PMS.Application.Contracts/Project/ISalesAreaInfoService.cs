using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Application.Contracts.DTO.Project;
using RW.PMS.Domain.Entities.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Project
{
    public interface ISalesAreaInfoService : ICrudApplicationService<bd_SalesAreaInfoEntity, int>
    {
        /// <summary>
        /// 获取市场片区分页数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResult<bd_SalesAreaInfoDto> GetPagedList(bd_SalesAreaInfoSearchDto input);

        /// <summary>
        /// 判断市场片区编号是否存在
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool IsExist(bd_SalesAreaInfoDto input);

        /// <summary>
        /// 获取市场片区下拉框
        /// </summary>
        /// <returns></returns>
        List<SalesAreaInfoSelectDto> GetSalesAreaSelect();
    }
}
