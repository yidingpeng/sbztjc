using System.ComponentModel;

namespace RW.PMS.CrossCutting.Security.Authorization
{
    /// <summary>
    /// 操作类型
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        /// 查看
        /// </summary>
        View,

        /// <summary>
        /// 新增
        /// </summary>
        Add,

        /// <summary>
        /// 编辑
        /// </summary>
        Edit,

        /// <summary>
        /// 删除
        /// </summary>
        Delete,

        /// <summary>
        /// 授权
        /// </summary>
        Auth,

        /// <summary>
        /// 导出
        /// </summary>
        Download,
        /// <summary>
        /// 保存
        /// </summary>
        Save,
        /// <summary>
        /// 发布
        /// </summary>
        Issue,
        /// <summary>
        /// 详情
        /// </summary>
        Receiver,
        /// <summary>
        /// 同步权限
        /// </summary>
        Sync,
    }
}
