using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Workflow
{
    /// <summary>
    /// NodeId生成器，自动生成Node及编号顺序
    /// </summary>
    public class NodeIdGenerator
    {
        public NodeIdGenerator(string name)
        {
            this.NodeName = name;
        }

        public string NodeName { get; set; }

        public int Index { get; set; } = 1;

        public override string ToString()
        {
            return $"{NodeName}{Index}";
        }

        /// <summary>
        /// 流转到下一个流程ID，返回当前实例
        /// </summary>
        public NodeIdGenerator Next()
        {
            Index++;
            return this;
        }
    }
}
