using RW.PMS.Domain.ValueObject;
using System;
using FreeSql.DataAnnotations;
using System.ComponentModel.DataAnnotations;
namespace RW.PMS.Domain.Entities.Base
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuditable
    {
        /// <summary>
        /// 创建人
        /// </summary>
        int CreatedBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreatedAt { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        int LastModifiedBy { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        DateTime LastModifiedAt { get; set; }

  

    }
}