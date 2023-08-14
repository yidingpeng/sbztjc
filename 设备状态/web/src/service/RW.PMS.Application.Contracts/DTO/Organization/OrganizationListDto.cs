using RW.PMS.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Organization
{
    public class OrganizationListDto : TreeList<OrganizationListDto>
    {
        public OrganizationListDto()
        {

        }

        public int Id { get; set; }

        public int ParentId { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 机构编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 部门负责人的用户ID
        /// </summary>
        public int? Leader { get; set; }

        /// <summary>
        /// 负责人名称
        /// </summary>
        public string LeaderName { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 机构类型
        /// </summary>
        public int? OrganizationType { get; set; }

        /// <summary>
        /// 机构类型名称
        /// </summary>
        public string OrganizationTypeName { get; set; }

        public OrganizationListDto SetLeader(Dictionary<int, string> users)
        {
            if (Children != null)
                foreach (var item in Children)
                {
                    item.SetLeader(users);
                }
            if (!Leader.HasValue)
                return this;
            try
            {
                LeaderName = users[Leader.Value];
            }
            catch (Exception)
            {
                LeaderName = null;
            }
            return this;
        }
        public OrganizationListDto SetOrgType(Dictionary<int, string> OrgType)
        {
            if (Children != null)
                foreach (var item in Children)
                {
                    item.SetOrgType(OrgType);
                }
            if (!OrganizationType.HasValue)
                return this;
            try
            {
                OrganizationTypeName = OrgType[OrganizationType.Value];
            }
            catch (Exception)
            {
                OrganizationTypeName = null;
            }
            return this;
        }
    }
}
