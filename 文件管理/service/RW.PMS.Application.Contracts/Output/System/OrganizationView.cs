using RW.PMS.Domain.ValueObject;

namespace RW.PMS.Application.Contracts.Output.System
{
    public class OrganizationView
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        /// <summary>
        ///     机构名称
        /// </summary>
        public string OrganizationName { get; set; }

        /// <summary>
        ///     机构编码
        /// </summary>
        public string OrganizationCode { get; set; }

        /// <summary>
        ///     机构类型
        /// </summary>
        public OrganizationType OrganizationType { get; set; }

        /// <summary>
        ///     排序
        /// </summary>
        public int Sort { get; set; }

        public string Description { get; set; }

        public dynamic Parent { get; set; }
    }
}