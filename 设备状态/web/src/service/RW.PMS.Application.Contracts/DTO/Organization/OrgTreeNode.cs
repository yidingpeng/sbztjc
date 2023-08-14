using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Organization
{
    /// <summary>
    /// 组织架构树节点，用于流程审批选择组织架构
    /// </summary>
    public class OrgTreeNode
    {
        public OrgTreeNode()
        {
            Nodes = new List<OrgTreeNode>();
        }

        public OrgTreeNode(OrganizationListDto item)
        {
            this.Id = item.Id;
            this.Name = item.Name;
            this.Leader = item.LeaderName;
            this.LeaderUserId = item.Leader;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Leader { get; set; }
        public int? LeaderUserId { get; set; }
        public OrgTreeNode Parent { get; set; }
        public List<OrgTreeNode> Nodes { get; set; }

        /// <summary>
        /// 根据部门ID查看部门信息
        /// </summary>
        public OrgTreeNode FindById(int id)
        {
            if (this.Id == id) return this;
            else if (Nodes == null)
                return null;
            {
                foreach (var item in Nodes)
                {
                    var n = item.FindById(id);
                    if (n != null)
                        return n;
                }
                return null;
            }
        }

        public static OrgTreeNode GetTreeNode(IEnumerable<OrganizationListDto> list)
        {
            if (list.Count() > 0)
            {
                //多级，需要新增一个全部
                OrgTreeNode parent = new OrgTreeNode();
                parent.Id = 0;
                parent.Name = "全部";

                foreach (var item in list)
                {
                    var nparent = new OrgTreeNode(item);
                    nparent.Nodes = GetChild(nparent, item.Children);
                    parent.Nodes.Add(nparent);
                }

                return parent;
            }
            else
            {
                var item = list.Where(x => x.ParentId == 0).FirstOrDefault();
                var parent = new OrgTreeNode(item);
                parent.Nodes = GetChild(parent, list);
                return parent;
            }
        }

        static List<OrgTreeNode> GetChild(OrgTreeNode parent, IEnumerable<OrganizationListDto> list)
        {
            List<OrgTreeNode> items = new List<OrgTreeNode>();
            if (list == null) return items;
            foreach (var item in list)
            {
                if (item.ParentId == parent.Id)
                {
                    var node = new OrgTreeNode(item);
                    node.Parent = parent;
                    node.Nodes = GetChild(node, item.Children);
                    items.Add(node);
                }

                //return list.Where(x => x.ParentId == parent.Id).Select(x =>
                //{
                //    var node = new OrgTreeNode(x);
                //    node.Parent = parent;
                //    node.Nodes = GetChild(node, x.Children);
                //    return node;
                //}).ToList();
            }
            return items;
        }
    }
}
