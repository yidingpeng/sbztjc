using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Post
{
    public class PostListDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string PostName { get; set; }
        /// <summary>
        /// 所属部门ID
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string OrgName { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderIndex { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }
    }
}
