using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Probelem
{
    /// <summary>
    /// 问题反馈信息表
    /// </summary>
    [Table(Name = "pro_problem_feedback", OldName = "ProjectProblemfeedback")]

    public class ProblemfeedbackEntity : FullEntity
    {
        /// <summary>
        ///     项目ID
        /// </summary>
        public int pt_ID { get; set; }

        /// <summary>
        ///     问题类型ID
        /// </summary>
        public int ProblemTypeID { get; set; }

        /// <summary>
        ///     描述（判定原因）
        /// </summary>
        [MaxLength(500)]
        public string pfb_ExceptionDesc { get; set; }

        /// <summary>
        ///     用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     发送地址
        /// </summary>
        public int addressId { get; set; }

        /// <summary>
        ///     要求完成时间
        /// </summary>
        public DateTime FeedbackTime { get; set; }

        /// <summary>
        ///     实际开始时间
        /// </summary>
        public DateTime EstimatedSettlementDate { get; set; }

        /// <summary>
        ///     解决人员(接收人)
        /// </summary>
        public int SolutionId { get; set; }

        /// <summary>
        ///     实际解决时间
        /// </summary>
        public DateTime ActualSettlementDate { get; set; }

        /// <summary>
        ///     原因分析
        /// </summary>
        [MaxLength(500)]
        public string CauseAnalysis { get; set; }

        /// <summary>
        ///     改善措施
        /// </summary>
        [MaxLength(500)]
        public string Improvement { get; set; }

        /// <summary>
        ///     影响时间
        /// </summary>
        public string InfluenceDate { get; set; }

        /// <summary>
        ///     处理动态
        /// </summary>
        public string DealWithDynamic { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
        /// <summary>
        /// 原因类型
        /// </summary>
        public string ReasonType { get; set; }

        /// <summary>
        ///  问题描述
        /// </summary>
        [MaxLength(500)]
        public string ProblemDescription { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime PlanStartTime { get; set; }
        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime PlanEndTime { get; set; }

        /// <summary>
        /// 是否合格
        /// </summary>
        public int IsQualified { get; set; }

        /// <summary>
        /// 不合格原因
        /// </summary>
        public string UnqualifiedReason { get; set; }

        /// <summary>
        /// 问题来源
        /// </summary>
        public string Source { get; set; }
    }
}
