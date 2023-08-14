using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using RW.PMS.Domain.ValueObject;

namespace RW.PMS.Domain.Entities.System
{
    // 机构实体
    [Table(Name = "sys_organization", OldName = "Organization")]
    public class OrganizationEntity : FullEntity, ITree<int>, IOrderable
    {
        /// <summary>
        /// 机构唯一编号，可以为空，但不建议为空
        /// 数据同步时，将使用此编号
        /// </summary>
        public string OrgNo { get; set; }

        /// <summary>
        /// 上级Id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 上级部门名称（冗余，数据库不存在）
        /// </summary>
        [Column(IsIgnore = true)]
        public string ParentName { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        [MaxLength(50)]
        public string OrganizationName { get; set; }

        /// <summary>
        /// 机构编码
        /// </summary>
        [MaxLength(50)]
        public string OrganizationCode { get; set; }

        /// <summary>
        /// 部门负责人的用户ID
        /// </summary>
        public int? Leader { get; set; }

        /// <summary>
        /// 领导姓名
        /// </summary>
        [Column(IsIgnore = true)]
        public string LeaderName { get; set; }

        /// <summary>
        /// 机构类型
        /// </summary>
        [Column(MapType = typeof(int))]
        public OrganizationType OrganizationType { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [MaxLength(255)]
        public string Description { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderIndex { get; set; }
        public int Sort { get; set; }
    }
}