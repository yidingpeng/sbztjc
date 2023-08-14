using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Integration
{
    /// <summary>
    /// 数据集成记录表
    /// </summary>
    [Table(Name = "itg_records")]
    public class IntegrationRecordEntity : FullEntity
    {
        /// <summary>
        /// 集成类型
        /// </summary>
        public IntegrationType Type { get; set; }

        /// <summary>
        /// 集成状态
        /// </summary>
        public IntegrationStatus Status { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 处理结束时间
        /// </summary>
        public DateTime FinishTime { get; set; }

        /// <summary>
        /// 导入用时
        /// </summary>
        public int Cost { get; set; }
    }

    /// <summary>
    /// 集成类别枚举
    /// </summary>
    public enum IntegrationType
    {
        /// <summary>
        /// 组织架构
        /// </summary>
        Organization,
        /// <summary>
        /// 流程
        /// </summary>
        Workflow,
        /// <summary>
        /// 消息
        /// </summary>
        Message,
    }

    /// <summary>
    /// 集成状态
    /// </summary>
    public enum IntegrationStatus
    {
        /// <summary>
        /// 就绪
        /// </summary>
        Ready = 0,
        /// <summary>
        /// 已执行
        /// </summary>
        Executed = 1,
        /// <summary>
        /// 执行失败
        /// </summary>
        Fail = -1,
    }
}
