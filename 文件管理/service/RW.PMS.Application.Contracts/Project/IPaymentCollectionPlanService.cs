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
    public interface IPaymentCollectionPlanService : ICrudApplicationService<PaymentCollectionPlanEntity, int>
    {
        /// <summary>
        /// 根据合同id获取数据
        /// </summary>
        /// <param name="contractid"></param>
        /// <returns></returns>
        List<PaymentCollectionPlanDto> GetListByConId(int contractid);

        /// <summary>
        /// 判断是否存在已存在合同id和回款类型一样的数据
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool HasHhisItem(PaymentCollectionPlanDto item);

        /// <summary>
        /// 添加合同后把改合同id设置成其回款计划的Ct_Id
        /// </summary>
        /// <param name="CtId"></param>
        void UpdatePaymentCPlan(string newidstr, int CtId);

        /// <summary>
        /// 删除所有合同id为0的数据
        /// </summary>
        void DeleteAllCtIdZero(string newidstr);
    }
}
