using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class projectTemplateView
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 项目类型
        /// </summary>
        public int ProjectType { get; set; }
        /// <summary>
        /// 项目类型名称
        /// </summary>
        public string ProjectTypeName { get; set; }

        /// <summary>
        /// attr1
        /// </summary>
        public string Attr1 { get; set; }
        /// <summary>
        /// attr2
        /// </summary>
        public string Attr2 { get; set; }

        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// 状态 1启用  0停用
        /// </summary>
        public bool State { get; set; }

    }
}
