using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.CrossCutting.Extender
{
    public static class ArrayExtender
    {
        /// <summary>
        /// 将一个数组类型转换成字符串类型，并用指定符号拼接，通常使用‘,’拼接
        /// </summary>
        /// <typeparam name="T">可以是任意类型</typeparam>
        /// <param name="arr">数组对象</param>
        /// <param name="split">分隔符</param>
        /// <returns>拼接后的字符串</returns>
        public static string ArrayToString<T>(this IEnumerable<T> arr, string split = ",")
        {
            if (arr == null || arr.Count() == 0) return "";
            return arr.Select(x => Convert.ToString(x)).Aggregate((a, b) => a + split + b);
        }

        /// <summary>
        /// 将一个字符串使用指定的分隔符分隔成指定类型的数组
        /// </summary>
        /// <typeparam name="T">尽量使用基础类型，否则可能导致无法转换</typeparam>
        /// <param name="str">原始字符串</param>
        /// <param name="split">分隔符，默认为','</param>
        /// <returns></returns>
        public static T[] StringToArray<T>(this string str, string split = ",")
        {
            if (string.IsNullOrEmpty(str))
                return new T[0];
            var arr = str.Split(split);
            return arr.Select(x => (T)Convert.ChangeType(x, typeof(T))).ToArray();
        }

        /// <summary>
        /// （去重）从一个数组中，减去另外一个数组
        /// </summary>
        public static T Minus<T>(this T[] arr1, T[] arr2)
        {
            if (arr1 == null || arr2 == null) return default(T);
            for (int i = 0; i < arr1.Length; i++)
            {
                if (arr2.Length > i && arr1[i].Equals(arr2[i]))
                    continue;
                return arr1[i];
            }
            return default(T);
            //return arr1.Where(x => !arr2.Contains(x)).ToArray();
        }

        public static bool CompareArray<T>(this T[] arr1, T[] arr2)
        {
            if (arr1 == arr2) return true;
            if (arr1 == null || arr2 == null) return false;
            if (arr1.Length != arr2.Length) return false;
            Array.Sort(arr1);
            Array.Sort(arr2);
            for (int i = 0; i < arr1.Length; i++)
            {
                if (!arr1[i].Equals(arr2[i])) return false;
            }
            return true;
        }
    }
}
