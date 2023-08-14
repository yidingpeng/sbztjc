using System;

namespace RW.PMS.CrossCutting.Exceptions
{
    /// <summary>
    ///     业务逻辑异常
    /// </summary>
    public class LogicException : ApplicationException
    {
        /// <summary>
        ///     错误编码
        /// </summary>
        public readonly ExceptionCode Code;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code">错误代码</param>
        public LogicException(ExceptionCode code) : base(string.Empty)
        {
            Code = code;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="code">错误代码</param>
        /// <param name="message">错误信息</param>
        public LogicException(ExceptionCode code, string message) : base(message)
        {
            Code = code;
        }

        public LogicException(string message) : base(message)
        {
            Code = ExceptionCode.Failed;
        }
    }
}