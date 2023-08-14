using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Material
{
    public class BOMListDto
    {
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
        /// 材料
        /// </summary>
        public string material { get; set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        public string CurrentState { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string applicant { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string ApplicationTime { get; set; }

        /// <summary>
        /// 校对者
        /// </summary>
        public string proofreader { get; set; }

        /// <summary>
        /// 校对日期
        /// </summary>
        public string ProofreaderDate { get; set; }

        /// <summary>
        /// 审核者
        /// </summary>
        public string Inspector { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public string InspectorData { get; set; }

        /// <summary>
        /// 批准者
        /// </summary>
        public string Approver { get; set; }

        /// <summary>
        /// 批准时间
        /// </summary>
        public string ApproverData { get; set; }

        /// <summary>
        /// 驳回原因
        /// </summary>
        public string ReasonRejection { get; set; }


        /// <summary>
        /// 同步时间
        /// </summary>
        public string SynchronisedTime { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        public string ProjectCode { get; set; }
    }
}
