using System;
using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Domain.Entities.Basics
{
    /// <summary>
    ///     工序基础表
    /// </summary>
    [Table(Name = "base_processInfo")]
    public class ProcessInfoEntity : FullEntity, ITree<int>
    {

        /// <summary>
        ///     工序编号
        /// </summary>
        [MaxLength(150)]
        public string pcsNo { get; set; }

        /// <summary>
        ///     工序名称
        /// </summary>
        [MaxLength(150)]
        public string pcsName { get; set; }

        /// <summary>
        ///     父级工序
        /// </summary>
        [Column(OldName = "pcsParentID")]
        public int ParentId { get; set; }
     
        /// <summary>
        ///     是否是总装/总拆工序
        /// </summary>
        public int isFinishGW { get; set; }

        /// <summary>
        ///     启用状态：0：保存；1:下发；-1：禁用
        /// </summary>
        public int usingFlag { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        [MaxLength(255)]
        public string remark { get; set; }
    }
}
