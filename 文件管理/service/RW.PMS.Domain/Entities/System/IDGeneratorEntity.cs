using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.System
{
    /// <summary>
    /// ID 生成器
    /// 流水ID生成器，生成规则是每天0点重新编号
    /// </summary>
    [Table(Name = "sys_ids")]
    public class IDGeneratorEntity : Entity<int>
    {
        /// <summary>
        /// 类型(全小写) 该项可随意填写，不同类型的编号时序不同
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 编号模式，
        /// 按日：daily
        /// 按周：weekly
        /// 按月：monthly
        /// 按年：yearly
        /// 连续：ordinal
        /// </summary>
        public IdModeEnum Mode { get; set; }
        /// <summary>
        /// 编号规则的日期（天）
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 当前编号序号
        /// </summary>
        public int CurrentIndex { get; set; }

        static object locker = new object();
        /// <summary>
        /// 获取当天下一个流水号
        /// </summary>
        public IDGeneratorEntity Next()
        {
            //线程安全，保证多线程时，也不会出现重码
            lock (locker)
            {
                if (NeedCreateNew())
                    CreateNew(Type);
                else
                    CurrentIndex++;
                return this;
            }
        }

        bool NeedCreateNew()
        {
            if (Date == DateTime.MinValue) throw new Exception("该数据是一个全新数据，需先调用CreateNew方法");
            var now = DateTime.Now.Date;
            var weekValue = (int)now.DayOfWeek;
            //中国从周一开始
            if (weekValue == 0) weekValue = 7;
            return Mode switch
            {
                IdModeEnum.Daily => now > Date,
                IdModeEnum.Weekly => Date.BetweenEnd(now.AddDays(1 - weekValue), now.AddDays(7 - weekValue)),
                IdModeEnum.Monthly => now.Month > Date.Month,
                IdModeEnum.Yearly => now.Year > Date.Year,
                _ => false,
            };
        }

        /// <summary>
        /// 创建新的流程,编号归零，需调用Next方法
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IDGeneratorEntity CreateNew(string type)
        {
            this.Type = type;
            Date = DateTime.Now;
            CurrentIndex = 0;
            return this;
        }

        /// <summary>
        /// 获取当前ID
        /// </summary>
        /// <returns></returns>
        public string GetId()
        {
            return $"{Date:yyyyMMdd}{CurrentIndex:0000}";
        }
    }

    /// <summary>
    /// Id生成模式
    /// </summary>
    public enum IdModeEnum
    {
        /// <summary>
        /// 按日
        /// </summary>
        Daily,
        /// <summary>
        /// 按周
        /// </summary>
        Weekly,
        /// <summary>
        /// 按月
        /// </summary>
        Monthly,
        /// <summary>
        /// 按年
        /// </summary>
        Yearly,
        /// <summary>
        /// 连续的
        /// </summary>
        Ordinal

    }
}
