using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO
{
    /// <summary>
    /// 键值对泛型实体
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class KeyValueDto<TKey, TValue>
    {
        public KeyValueDto()
        {

        }

        public KeyValueDto(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
        }

        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }

    /// <summary>
    /// 键值对实体
    /// </summary>
    public class KeyValueDto : KeyValueDto<string, string>
    {
        public KeyValueDto()
        {

        }

        public KeyValueDto(string key, string value) : base(key, value)
        {
        }
    }
}
