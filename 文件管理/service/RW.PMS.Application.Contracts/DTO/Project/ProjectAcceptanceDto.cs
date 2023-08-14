using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class ProjectAcceptanceDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 项目id
        /// </summary>
        public int ProjectID { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 业主单位名称
        /// </summary>
        public string OwnerUnitName { get; set; }

        /// <summary>
        /// 设备id
        /// </summary>
        public int DeviceID { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public string DeviceNo { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }

        /// <summary>
        /// 验收类别
        /// </summary>
        public int AcceptCategory { get; set; }

        /// <summary>
        /// 验收类别名称
        /// </summary>
        public string AcceptCategoryName { get; set; }

        /// <summary>
        /// 验收日期
        /// </summary>
        public DateTime AcceptDate { get; set; }

        /// <summary>
        /// 验收人员
        /// </summary>
        public int Acceptancer { get; set; }

        /// <summary>
        /// 验收人员名称
        /// </summary>
        public string AcceptancerName { get; set; }

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
        /// 验收结论名称
        /// </summary>
        public string AcceptResultTxt { get; set; }

        /// <summary>
        /// 最终整改完成日期
        /// </summary>
        public DateTime FinalAbarbeitungDate { get; set; }

        /// <summary>
        /// 签字确认附件
        /// </summary>
        public int SignConfirmFile { get; set; }

        /// <summary>
        /// 签字确认附件地址
        /// </summary>
        public string SignConfirmFileUrl { get; set; }

        /// <summary>
        /// 签字确认附件名称
        /// </summary>
        public string fileName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
