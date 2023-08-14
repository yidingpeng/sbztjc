using System;
using System.ComponentModel;
using System.Linq;

namespace RW.PMS.CrossCutting.Extensions
{
    /// <summary>
    ///     枚举扩展
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 获取Description
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum e)
        {
            var descriptionAttribute =
                e.GetType().GetField(e.ToString())?.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
            return (descriptionAttribute as DescriptionAttribute)?.Description;
        }
    }
}
