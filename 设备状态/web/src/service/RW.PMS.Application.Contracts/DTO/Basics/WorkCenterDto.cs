using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Basics
{
    /// <summary>
    ///     工位（工作中心）基础信息表
    /// </summary>
    public class WorkCenterDto
    {
        public int Id { get; set; }

        /// <summary>
        ///     工作中心编码
        /// </summary>
        public string workCode { get; set; }
        /// <summary>
        ///     工作中心名称
        /// </summary>
        public string workName { get; set; }
        /// <summary>
        ///     工作中心类型
        /// </summary>
        public int workType { get; set; }
        public string workTypeTxt { get; set; }
        /// <summary>
        ///     状态，0:启用，1：禁用	
        /// </summary>
        public int gwStatus { get; set; }
        /// <summary>
        ///     工位信息终端IP
        /// </summary>
        public string gwIP { get; set; }
        /// <summary>
        ///     工位MAC地址
        /// </summary>
        public string gwMac { get; set; }
        /// <summary>
        ///     父级工作中心
        /// </summary>
        public int? atAreaID { get; set; }
        public string atAreaTxt { get; set; }
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
        public string remark { get; set; }
    }
}
