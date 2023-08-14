using System.Collections.Generic;

namespace RW.PMS.CrossCutting.Localization.Base
{
    /// <summary>
    ///     枚举列表
    /// </summary>
    public interface IEnumList
    {
        /// <summary>
        ///     获取枚举列表
        /// </summary>
        /// <returns></returns>
        IList<EnumItem> GetList<T>() where T : struct;
    }
}