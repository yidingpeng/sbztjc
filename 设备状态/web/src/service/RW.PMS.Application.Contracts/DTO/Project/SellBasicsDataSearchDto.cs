using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class SellBasicsDataSearchDto : PagedQuery
    {
        public int Id { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 项目分类
        /// </summary>
        public int ProjectClass { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public int BusinessType { get; set; }
        /// <summary>
        /// 重要等级
        /// </summary>
        public int UrgentGrade { get; set; }
        /// <summary>
        /// 客户公司
        /// </summary>
        public int ClientCompany { get; set; }
        /// <summary>
        /// 项目接收日期
        /// </summary>
        public DateTime ProjectReceiveDate { get; set; }
        /// <summary>
        /// 合同交货日期
        /// </summary>
        public DateTime ContractDeliveryDate { get; set; }
        /// <summary>
        /// 预计使用日期
        /// </summary>
        public DateTime ExpectedUseDate { get; set; }
        /// <summary>
        /// 管理方式
        /// </summary>
        public int ProManagementStyle { get; set; }
        /// <summary>
        /// 合同信息表ID
        /// </summary>
        public int ContractID { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int ProState { get; set; }

        /// <summary>
        /// 已到期质保金是否大于0
        /// </summary>
        public int IsPeriodMoneyZero { get; set; }
        /// <summary>
        /// 项目已回款比
        /// </summary>
        public int ProReturnedScale { get; set; }
        /// <summary>
        /// 市场片区
        /// </summary>
        public string marketArea { get; set; }
        /// <summary>
        /// 营销经理
        /// </summary>
        public string PersonInChargeName { get; set; }
        /// <summary>
        /// 项目经理
        /// </summary>
        public string ProjectManager { get; set; }
        /// <summary>
        /// 产品经理
        /// </summary>
        public string ProductManager { get; set; }
        /// <summary>
        /// 分管领导
        /// </summary>
        public string LeadersManager { get; set; }
    }
}
