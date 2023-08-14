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
    /// 任务信息表
    /// </summary>
    [Table(Name = "bd_taskprocess_info", OldName = "bd_TaskProcessInfo")]

    public class TaskProcessInfoEntity: FullEntity, ITree<int>
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 同级排序号
        /// </summary>
        public int SeqNo { get; set; }
        /// <summary>
        /// 项目类型
        /// </summary>
        public int ProjectClassID { get; set; }
        /// <summary>
        /// 任务编码
        /// </summary>
        public string TaskCode { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }
        /// <summary>
        /// 工作内容
        /// </summary>
        [MaxLength(1000)]
        public string WorkMemo { get; set; }
        /// <summary>
        /// 是否里程碑
        /// </summary>
        public string IsMilestone { get; set; }
        /// <summary>
        /// 是否关键任务
        /// </summary>
        public string IsKey { get; set; }
        /// <summary>
        /// 是否需评审
        /// </summary>
        public string IsReview { get; set; }
        /// <summary>
        /// 前置条件
        /// </summary>
        public string InputCondition { get; set; }
        /// <summary>
        /// 输出文件
        /// </summary>
        public string OutPutFile { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(1000)]
        public string Remark { get; set; }
    }
}
