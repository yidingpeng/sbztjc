using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Basics
{
    public class GjwlOpcPointDto
    {
        public int Id { get; set; }

        /// <summary>
        ///     工作中心ID
        /// </summary>
        public int gwID { get; set; }

        /// <summary>
        ///     工作中心名称 
        /// </summary>
        public string gwname { get; set; }

        /// <summary>
        ///     工具ID
        /// </summary>
        public int? gjID { get; set; }
        public string gjname { get; set; }

        /// <summary>
        ///     物料ID
        /// </summary>
        public int? wlID { get; set; }
        public string wlname { get; set; }
        /// <summary>
        ///     Opc点位类型ID
        /// </summary>
        public int opcTypeID { get; set; }

        /// <summary>
        ///     Opc点位类型编码
        /// </summary>
        public string opcTypeCode { get; set; }

        /// <summary>
        ///     设备名称
        /// </summary>
        public string opcDeviceName { get; set; }

        /// <summary>
        ///     Opc点位信息
        /// </summary>
        [MaxLength(150)]
        public string opcValue { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        [MaxLength(255)]
        public string remark { get; set; }

        public List<GjWlOpcType> opclist { get; set; }
    }

    public class GjWlOpcType
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
    }
}
