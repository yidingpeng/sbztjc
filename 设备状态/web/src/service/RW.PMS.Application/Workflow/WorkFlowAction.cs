using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace RW.PMS.Application.Workflow
{
    /// <summary>
    /// 工作流 执行判断
    /// 使用时，需注入几个委托，如获取用户，
    /// </summary>
    public class WorkflowActionEnum
    {
        /// <summary>
        /// 初始化流程操作
        /// </summary>
        /// <param name="node">流程节点</param>
        /// <param name="current">当前流程</param>
        public WorkflowActionEnum(JsonNode node, string current)
        {
            this.Node = node;
            this.Current = current;
        }

        public JsonNode Node { get; set; }
        public string Current { get; private set; }

        /// <summary>
        /// 获取用户的主管用户ID
        /// </summary>
        public delegate Func<int> GetSupervisor(int uid);

        /// <summary>
        /// 跳转到下一个环节，可获取下一个环节相关的用户
        /// </summary>
        /// <return>返回用户ID</return>
        int Next()
        {
            return 0;
        }



        /// <summary>
        /// 验证指定用户是否有权限
        /// </summary>
        bool Valid(int userId)
        {
            return false;
        }

        /// <summary>
        /// 指定用户是否可以审批
        /// </summary>
        internal bool CanApprovide(int uid)
        {
            throw new NotImplementedException();
        }
    }
}
