using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class ProjectDeviceDetailsView
    {
        public int Id { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string DeviceNo { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// 项目信息ID
        /// </summary>
        public int ProjectID { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 产品系列
        /// </summary>
        public int ProductLine { get; set; }
        /// <summary>
        /// 产品系列
        /// </summary>
        public string ProductLineName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Qty { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string DeviceUnit { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal DevicePrice { get; set; }
        /// <summary>
        /// 备注--设备主要功能
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 设备使用地点
        /// </summary>
        public string DevicePlaceOfUse { get; set; }

        /// <summary>
        /// 要求交付日期
        /// </summary>
        public string DeliverDate { get; set; }
    }
}
