using System;
using System.Linq;

namespace RW.PMS.CrossCutting.Extensions
{
    /// <summary>
    /// String扩展类
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 判断字符串是否为null或空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 判断字符串不为null或空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool NotNullOrEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 判断字符串是否为null或空白字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// 判断字符串不为null或空白字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool NotNullOrWhiteSpace(this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// 将字符串使用指定分隔符转成数组
        /// </summary>
        public static string[] StringToArray(this string arr, char split = ',')
        {
            if (string.IsNullOrEmpty(arr))
                return null;
            return arr.Split(split);
        }

        /// <summary>
        /// 将字符串数组合成为一个字符串
        /// </summary>
        public static string ArrayToString(this string[] arr, string connect = ",")
        {
            if (arr == null || arr.Length == 0) return null;
            return arr.Aggregate((a, b) => a + connect + b);
        }

        /// <summary>
        /// 将指定类型的数组组合成为一个字符串
        /// int[] a = new int[10];
        /// a.ArrayToString();
        /// </summary>
        public static string ArrayToString<T>(this T[] arr, string connect = ",")
        {
            if (arr == null || arr.Length == 0) return null;
            return arr.Select(x => Convert.ToString(x)).Aggregate((a, b) => a + connect + b);
        }
    }
}
