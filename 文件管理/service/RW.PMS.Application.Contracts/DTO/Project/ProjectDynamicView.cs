using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class ProjectDynamicView 
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 动态类型
        /// </summary>
        public int dyType { get; set; }
        /// <summary>
        /// 动态类型名
        /// </summary>
        public string dyTypeName { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        public int projectID { get; set; }
        /// <summary>
        /// 项目名
        /// </summary>
        public string projectName { get; set; }
        /// <summary>
        /// 操作内容
        /// </summary>
        public string operationContent { get; set; }
        /// <summary>
        /// attr1
        /// </summary>
        public string attr1 { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreatedBy { get; set; }
        /// <summary>
        /// 创建人名
        /// </summary>
        public string CreatedByName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
