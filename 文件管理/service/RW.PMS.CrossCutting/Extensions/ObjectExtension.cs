using System;
using System.ComponentModel;
using System.Globalization;
using System.Text.Json;

namespace RW.PMS.CrossCutting.Extensions
{
    /// <summary>
    /// Object扩展方法
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// 类型转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static T To<T>(this object obj, T defValue = default)
            where T : struct
        {
            if (obj == null)
            {
                return defValue;
            }

            if (typeof(T) == typeof(Guid) || typeof(T) == typeof(TimeSpan))
            {
                return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(obj.ToString());
            }
            if (typeof(T).IsEnum)
            {
                if (Enum.IsDefined(typeof(T), obj))
                {
                    return (T)Enum.Parse(typeof(T), obj.ToString());
                }

                throw new ArgumentException($"Enum type undefined '{obj}'.");
            }

            return (T)Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture);
        }

        public static string ToJson(this object obj, JsonSerializerOptions serializerSettings = default)
        {
            return obj == null ? null : JsonSerializer.Serialize(obj, serializerSettings);
        }
    }
}
