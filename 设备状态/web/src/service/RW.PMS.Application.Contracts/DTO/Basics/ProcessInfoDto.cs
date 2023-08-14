using RW.PMS.Application.Contracts.DTO.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RW.PMS.Application.Contracts.DTO.Basics
{
    public class ProcessInfoDto : TreeList<ProcessInfoDto>
    {
        public int Id { get; set; }

        /// <summary>
        ///     工序编号
        /// </summary>
        public string pcsNo { get; set; }

        /// <summary>
        ///     工序名称
        /// </summary>
        public string pcsName { get; set; }

        /// <summary>
        ///     父级工序
        /// </summary>
        public int? ParentId { get; set; }
        public string pcsParentTxt { get; set; }

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
        public string remark { get; set; }
    }
}
