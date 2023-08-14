using System;
using System.Data;
using FreeSql;

namespace RW.PMS.CrossCutting.AOP.Attributes
{
    /// <summary>
    /// 事务属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class TransactionAttribute : Attribute
    {
        /// <summary>
        /// 事务传播方式
        /// </summary>
        public Propagation Propagation { get; set; } = Propagation.Required;

        /// <summary>
        /// 事务隔离级别
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }
    }
}