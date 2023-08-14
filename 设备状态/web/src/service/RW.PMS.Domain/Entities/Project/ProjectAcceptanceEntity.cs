using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Project
{
    /// <summary>
    /// 项目交付信息
    /// </summary>
    [Table(Name = "pro_acceptance", OldName = "ProjectAcceptance")]

    public class ProjectAcceptanceEntity : FullEntity
    {
        /// <summary>
        /// 项目id
        /// </summary>
        public int ProjectID { get; set; }
        /// <summary>
        /// 设备id
        /// </summary>
        public int DeviceID { get; set; }
        /// <summary>
        /// 验收类别
        /// </summary>
        public int AcceptCategory { get; set; }
        /// <summary>
        /// 验收日期
        /// </summary>
        public DateTime AcceptDate { get; set; }
        /// <summary>
        /// 验收人员
        /// </summary>
        public int Acceptancer { get; set; }
        /// <summary>
        /// 验收数量
        /// </summary>
        public int? Qty { get; set; }
        /// <summary>
        /// 竣工日期
        /// </summary>
        public DateTime CompletedDate { get; set; }
        /// <summary>
        /// 验收结论
        /// </summary>
        public int AcceptResult { get; set; }
        /// <summary>
        /// 最终整改完成日期
        /// </summary>
        public DateTime FinalAbarbeitungDate { get; set; }

        /// <summary>
        /// 签字确认附件
        /// </summary>
        public int SignConfirmFile { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}
