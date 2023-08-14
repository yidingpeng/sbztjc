using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO
{
    public class ProblemFeedbackSerchDto : PagedQuery
    {
        /// <summary>
        /// 项目编码
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        ///     反馈时间
        /// </summary>
        public string FeedbackTime { get; set; }

        /// <summary>
        ///     发送地址
        /// </summary>
        public int addressId { get; set; }
        /// <summary>
        /// 问题类型
        /// </summary>
        public int ProblemTypeID { get; set; }

        /// <summary>
        ///     处理动态
        /// </summary>
        public string DealWithDynamic { get; set; }

        /// <summary>
        ///     解决人员
        /// </summary>
        public int SolutionId { get; set; }
    }
}
