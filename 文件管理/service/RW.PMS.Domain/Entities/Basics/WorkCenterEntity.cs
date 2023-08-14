using System;
using System.ComponentModel.DataAnnotations;
using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;

namespace RW.PMS.Domain.Entities.Basics
{
    /// <summary>
    ///     工位（工作中心）基础信息表
    /// </summary>
    [Table(Name = "base_WorkCenter")]
    public class WorkCenterEntity : FullEntity
    {
        /// <summary>
        ///     工作中心编码
        /// </summary>
        [MaxLength(150)]
        public string workCode { get; set; }
        /// <summary>
        ///     工作中心名称
        /// </summary>
        [MaxLength(150)]
        public string workName { get; set; }
        /// <summary>
        ///     工作中心类型
        /// </summary>
        public int workType { get; set; }
        /// <summary>
        ///     状态，0:启用，1：禁用	
        /// </summary>
        public int gwStatus { get; set; }
        /// <summary>
        ///     工位信息终端IP
        /// </summary>
        [MaxLength(150)]
        public string gwIP { get; set; }
        /// <summary>
        ///     工位MAC地址
        /// </summary>
        [MaxLength(50)]
        public string gwMac { get; set; }
        /// <summary>
        ///     父级工作中心
        /// </summary>
        public int atAreaID { get; set; }
        /// <summary>
        ///     是否有追溯系统
        /// </summary>
        public int isHasFollow { get; set; }
        /// <summary>
        ///     是否有装配管理系统
        /// </summary>
        public int isHasAms { get; set; }
        /// <summary>
        ///     是否有光影引导系统
        /// </summary>
        public int isHasGuangying { get; set; }
        /// <summary>
        ///     备注
        /// </summary>
        [MaxLength(255)]
        public string remark { get; set; }
    }
}
