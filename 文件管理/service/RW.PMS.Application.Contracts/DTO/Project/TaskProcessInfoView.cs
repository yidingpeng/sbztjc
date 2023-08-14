using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class TaskProcessInfoView: TreeList<TaskProcessInfoView>
    {
        public int Id { get; set; }
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
        public string ProjectClassName { get; set; }
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
        public string Remark { get; set; }

        /// <summary>
        /// 前置条件地址
        /// </summary>
        public string PdfUrl { get; set; }

        /// <summary>
        /// 输出文件地址
        /// </summary>
        public string WordUrl { get; set; }

        /// <summary>
        /// 前置条件名称
        /// </summary>
        public string filePdfName { get; set; }

        /// <summary>
        /// 输出文件名称
        /// </summary>
        public string fileWordName { get; set; }
        //HasChildren
        public bool HasChildren { get; set; }
    }
}
