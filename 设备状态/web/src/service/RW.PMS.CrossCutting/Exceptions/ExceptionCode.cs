namespace RW.PMS.CrossCutting.Exceptions
{
    /// <summary>
    ///     错误代码
    /// </summary>
    public enum ExceptionCode
    {
        #region 用户相关异常

        /// <summary>
        ///     账号不存在
        /// </summary>
        AccountNotExist = 1001,

        /// <summary>
        ///     账户密码不正确
        /// </summary>
        PasswordError = 1002,

        #endregion

        #region 通用异常

        /// <summary>
        ///     插入失败
        /// </summary>
        InsertFailed = 9001,

        /// <summary>
        ///     更新失败
        /// </summary>
        UpdateFailed = 9002,

        /// <summary>
        ///     删除失败
        /// </summary>
        DeleteFailed = 9003,
        /// <summary>
        ///     编码出现重复
        /// </summary>
        CodeOnly = 6666,
        /// <summary>
        ///     参数错误
        /// </summary>
        ParamError = 9990,

        /// <summary>
        ///     默认错误
        /// </summary>
        Failed = 9999,

        #endregion

        #region 保养数据异常
        /// <summary>
        ///     数据已存在
        /// </summary>
        Repeat = 5999
        #endregion
    }
}