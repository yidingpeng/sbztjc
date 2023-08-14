namespace RW.PMS.CrossCutting.Security.Authorization
{
    /// <summary>
    /// 权限检查
    /// </summary>
    public interface IPermissionChecker
    {
        /// <summary>
        ///     判断是否拥有权限
        /// </summary>
        /// <param name="module">模块</param>
        /// <param name="operation">操作</param>
        /// <returns></returns>
        bool IsGranted(string module, OperationType operation);
    }
}