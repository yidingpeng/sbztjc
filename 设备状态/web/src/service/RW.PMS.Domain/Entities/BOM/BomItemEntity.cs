using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.BOM
{
    /// <summary>
    /// BOM项清单
    /// BOM项清单无法删除，保留每个版本的真实数据。
    /// 当发布第二版时，已有的BOM信息无法修改，只能进行逻辑删除。
    /// 增量表，不是每次添加一个版本都会增加全部的数据。
    /// </summary>
    [Table(Name = "`rw.esb`.`pdm_bom_items`")]
    public class BomItemEntity : IEntity<int>, ISoftDelete
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }
        /// <summary>
        /// 主bom表
        /// </summary>
        public int BomId { get; set; }
        /// <summary>
        /// BOM项序号，一般是自动排序（问题：A版和B版顺序不一样时怎么办，改还是不改？）
        /// </summary>
        [Column(IsIgnore = true)]
        [Obsolete("暂时取消此字段")]
        public string OrderIndex { get; set; }
        [Column(OldName = "OrderIndex")]
        public string Order { get; set; }
        public int Level { get; set; }
        /// <summary>
        /// 父编码
        /// </summary>
        public string Parent { get; set; }
        /// <summary>
        /// 物料编码，不同版本下，可能存在相同物料编码的项，可能是删除，也可能是增加。
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 需求数量,最低为1
        /// </summary>
        public int Count { get; set; } = 1;
        /// <summary>
        /// 是否送检
        /// </summary>
        public bool IsInspect { get; set; }
        /// <summary>
        /// 单项需求日期，建议不能超过总BOM的日期。
        /// </summary>
        public DateTime? RequiredTime { get; set; }
        /// <summary>
        /// 送检单位
        /// </summary>
        public string Inspector { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 版本号，原则上是可以自动增加的，但为了跨版本升级，还是可以单独使用
        /// 如VA.0、VB.0、VC.0...VZ.0、VA.1、VB.1...
        /// 
        /// </summary>
        public string VersionText { get; set; }
        /// <summary>
        /// 版本序号，从1开始
        /// </summary>
        public int VersionIndex { get; set; } = 1;
        /// <summary>
        /// 移除时的版本号(如B.0)，那么查看C.0的BOM表时，该项标记为已删除。
        /// 查看A.0或B.0时，该项显示正常。
        /// </summary>
        public string RemoveVersion { get; set; }
        /// <summary>
        /// 移除时的版本号，为0时，表示未移除。
        /// BOM项无法真正移除，也不能修改，只能在某个版本下添加新项和删除项。
        /// </summary>
        public int RemoveVersionIndex { get; set; }
        /// <summary>
        /// 创建时间，不同批次的时间将不同
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 删除时间
        /// </summary>
        [Column(Position = -2)]
        public DateTime? DeletedTime { get; set; }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        [Column(Position = -1)]
        public bool IsDeleted { get; set; }
    }
}
