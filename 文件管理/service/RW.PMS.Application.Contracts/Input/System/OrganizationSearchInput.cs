using RW.PMS.Application.Contracts.DTO;
using RW.PMS.Domain.ValueObject;

namespace RW.PMS.Application.Contracts.Input.System
{
    public class OrganizationSearchInput : PagedQuery
    {
        public int? ParentId { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrganizationName { get; set; }

        /// <summary>
        /// 机构类型
        /// </summary>
        public OrganizationType OrganizationType { get; set; }
    }
}