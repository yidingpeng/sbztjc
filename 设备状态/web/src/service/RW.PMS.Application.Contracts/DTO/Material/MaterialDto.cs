using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Material
{
    public class MaterialDto
    {
        public int? Id { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 代号
        /// </summary>
        public string CodeName { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string Specific { get; set; }

        /// <summary>
        /// 技术参数
        /// </summary>
        public string requirements { get; set; }

        /// <summary>
        /// 材质
        /// </summary>
        public string material { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string brand { get; set; }

        /// <summary>
        /// 规则路径
        /// </summary>
        public string RulePath { get; set; }

        /// <summary>
        /// 物料来源
        /// </summary>
        public string MaterialSource { get; set; }

        /// <summary>
        /// 基本单位
        /// </summary>
        public int Unit { get; set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        public string CurrentState { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public string OperatingType { get; set; }

        /// <summary>
        /// 停用状态
        /// </summary>
        public string disabled { get; set; }

        /// <summary>
        /// 停用原因
        /// </summary>
        public string Tyreason { get; set; }

        /// <summary>
        /// 申请人
        /// </summary>
        public string applicant { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public string ApplicationTime { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        public string editor { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public string EditDate { get; set; }

        /// <summary>
        /// 审核者
        /// </summary>
        public string Inspector { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public string InspectorData { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// 参考价
        /// </summary>
        public decimal ReferencePrice { get; set; }

        /// <summary>
        /// 采购周期
        /// </summary>
        public string PurchasingCycle { get; set; }

        /// <summary>
        /// 重要度
        /// </summary>
        public string Importance { get; set; }

        /// <summary>
        /// 物料类别
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 采购单位
        /// </summary>

        public string PurchasingUnit { get; set; }

        /// <summary>
        /// ERP状态
        /// </summary>
        public string ErpState { get; set; }

        /// <summary>
        /// ERP更新者
        /// </summary>
        public string ErpEditer { get; set; }

        /// <summary>
        /// ERP更新时间
        /// </summary>
        public string ErpEditTime { get; set; }

        /// <summary>
        /// 物料原码
        /// </summary>
        public string MaterialOriginalCode { get; set; }

        /// <summary>
        /// 同步时间
        /// </summary>
        public string SynchronisedTime { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        public string ProjectCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    }
}
