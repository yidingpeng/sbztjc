using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.CrossCutting.Extender
{
    /// <summary>
    /// 数字转换扩展类
    /// </summary>
    public static class ValueConvertExtender
    {
        /// <summary>
        /// 将任意类型转换成十进制数
        /// </summary>
        /// <returns>返回转换后的数值，失败返回0</returns>
        public static decimal ToDecimal(this object value)
        {
            decimal.TryParse(Convert.ToString(value), out decimal result);
            return result;
        }

        public static int ToInt32(this object value)
        {
            int.TryParse(Convert.ToString(value), out int result);
            return result;
        }

        public static double ToDouble(this object value)
        {
            double.TryParse(Convert.ToString(value), out double result);
            return result;
        }

        public static bool ToBool(this object value)
        {
            bool.TryParse(Convert.ToString(value), out bool result);
            return result;
        }

        public static DateTime ToDateTime(this object value)
        {
            DateTime.TryParse(Convert.ToString(value), out DateTime result);
            return result;
        }
    }
}
