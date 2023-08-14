using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;

namespace RW.PMS.Domain.Entities.Purchase
{
    /// <summary>
    /// BOM订单子表
    /// </summary>
    [Table(Name = "mat_operate")]
    public class Mat_OperateEntity: FullEntity
    {
        /// <summary>
        /// BOMID
        /// </summary>
        public int BomId { get; set; }
        /// <summary>
        /// 父级编码
        /// </summary>
        public string ParentCode { get; set; }
        /// <summary>
        /// 子级编码
        /// </summary>
        public string SublevelCode { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// 要求到货时间
        /// </summary>
        public string YQArrivalTime { get; set; }
        /// <summary>
        /// 采购人员
        /// </summary>
        public string AssignStaff { get; set; }
        /// <summary>
        /// 预计到货时间
        /// </summary>
        public string YJArrivalTime { get; set; }
        /// <summary>
        /// 实际到货时间
        /// </summary>
        public string SJArrivalTime { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 成本价格
        /// </summary>
        public decimal CostPrice { get; set; }
        /// <summary>
        /// 图纸代号
        /// </summary>
        public string DrawingCode { get; set; }
        /// <summary>
        /// 预计完成时间
        /// </summary>
        public string YJFinishTime { get; set; }
        /// <summary>
        /// 实际完成时间
        /// </summary>
        public string SJFinishTime { get; set; }
        /// <summary>
        /// 预计发货时间
        /// </summary>
        public string YjShipTime { get; set; }
        /// <summary>
        /// 预计入库时间（供应商）
        /// </summary>
        public string GYSArrivalTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// 是否图纸
        /// </summary>
        public bool IsDrawing { get; set; }

        /// <summary>
        /// 标记 initial edit del
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 代号
        /// </summary>
        public string Designation { get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string Specification { get; set; }

        /// <summary>
        /// 技术参数
        /// </summary>
        public string TechnicalParameters { get; set; }

        /// <summary>
        /// 材质
        /// </summary>
        public string MaterialQuality { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public string Weight { get; set; }

        /// <summary>
        /// 送检单位
        /// </summary>
        public string SubmittedUnit { get; set; }

        /// <summary>
        /// 物料来源
        /// </summary>
        public string MatSource { get; set; }

        /// <summary>
        /// 下发时间
        /// </summary>
        public DateTime DistributionTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }
    }
}
