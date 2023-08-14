using System.Collections.Generic;

namespace RW.PMS.Application.Contracts.DTO
{
    /// <summary>
    ///     树形列表基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TreeList<T> where T: TreeList<T>
    {
        public List<T> Children { get; set; }
    }
}
