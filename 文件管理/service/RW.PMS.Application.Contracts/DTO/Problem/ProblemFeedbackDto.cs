using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Problem
{
    public class ProblemFeedbackDto
    {
        public int? Id { get; set; }

        /// <summary>
        ///     项目ID
        /// </summary>
        public int pt_ID { get; set; }

        /// <summary>
        ///     项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        ///     项目编号
        /// </summary>
        public string ProjectCode { get; set; }

        /// <summary>
        ///     主项目编号
        /// </summary>
        public string PprojectCode { get; set; }

        /// <summary>
        ///     问题类型ID
        /// </summary>
        public int ProblemTypeID { get; set; }

        /// <summary>
        ///     问题类型
        /// </summary>
        public string ProblemTypeText { get; set; }

        /// <summary>
        ///     例外描述
        /// </summary>
        public string pfb_ExceptionDesc { get; set; }

        /// <summary>
        ///     用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        ///     发送地址
        /// </summary>
        public string addressId { get; set; }

        /// <summary>
        ///     反馈时间
        /// </summary>
        public DateTime? FeedbackTime { get; set; }

        /// <summary>
        ///     预计解决时间
        /// </summary>
        public DateTime? EstimatedSettlementDate { get; set; }

        /// <summary>
        ///     解决人员
        /// </summary>
        public int SolutionId { get; set; }

        /// <summary>
        ///     解决人员
        /// </summary>
        public string SolutionName { get; set; }

        /// <summary>
        ///     用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     实际解决时间
        /// </summary>
        public DateTime? ActualSettlementDate { get; set; }

        /// <summary>
        ///     原因分析
        /// </summary>
        public string CauseAnalysis { get; set; }

        /// <summary>
        ///     改善措施
        /// </summary>
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
        ///     处理动态名称
        /// </summary>
        public string DealWithDynamicTxt { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public dynamic PicId { get; set; }

        /// <summary>
        ///  反馈图片数组字符串
        /// </summary>
        public string imgFilesId { get; set; }

        /// <summary>
        /// 存储图片数组
        /// </summary>
        public int[] imgFeilsArr { get; set; }

        /// <summary>
        /// 原因类型
        /// </summary>
        public string ReasonType { get; set; }

        /// <summary>
        ///  问题描述
        /// </summary>
        public string ProblemDescription { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        public DateTime? PlanStartTime { get; set; }
        /// <summary>
        /// 计划结束时间
        /// </summary>
        public DateTime? PlanEndTime { get; set; }

        /// <summary>
        /// 是否合格
        /// </summary>
        public int IsQualified { get; set; }

        /// <summary>
        /// 不合格原因
        /// </summary>
        public string UnqualifiedReason { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 问题来源
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModifiedAt { get; set; }

    }
}
