using RW.PMS.Domain.Entities.Material;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.BOM.DTO
{
    public class BOMItemDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 主bom表
        /// </summary>
        public int BomId { get; set; }
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

        public pdm_material Material { get; set; }
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


    }
}
