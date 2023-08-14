using Microsoft.AspNetCore.Authorization;

namespace RW.PMS.CrossCutting.Security.Authorization
{
    /// <summary>
    /// 基于模块的授权验证
    /// </summary>
    public class ModuleAuthorizationRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// 基于模块的授权验证
        /// </summary>
        /// <param name="module">操作</param>
        /// <param name="operation">模块</param>
        public ModuleAuthorizationRequirement(string module, OperationType operation)
        {
            Module = module;
            Operation = operation;
        }

        /// <summary>
        /// 操作
        /// </summary>
        public OperationType Operation { get; set; }
        
        /// <summary>
        /// 模块
        /// </summary>
        public string Module { get; set; }
    }
}
