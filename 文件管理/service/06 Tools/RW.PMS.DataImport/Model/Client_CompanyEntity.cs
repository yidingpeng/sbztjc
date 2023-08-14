using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Project
{
    /// <summary>
    /// 客户公司信息表
    /// </summary>
    [Table(Name = "Client_Company")]
    public class Client_CompanyEntity : FullEntity
    {
        /// <summary>
        /// 单位编号
        /// </summary>
        public string CompanyCode { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 客户全称
        /// </summary>
        public string ClientFullName { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 客户级别
        /// </summary>
        public int ClientRank { get; set; }
        /// <summary>
        /// 合作业务
        /// </summary>
        public int CooperativeBusiness { get; set; }

        /// <summary>
        /// 公司董事长
        /// </summary>
        public int CEO { get; set; }

        /// <summary>
        /// 公司总经理
        /// </summary>
        public int GM { get; set; }

        /// <summary>
        /// 公司副总经理
        /// </summary>
        public int DeputyGM { get; set; }

        /// <summary>
        /// 市场片区
        /// </summary>
        public int MarketArea { get; set; }

        /// <summary>
        /// 营销负责人
        /// </summary>
        public int PersonInCharge { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(500)]
        public string Remark { get; set; }
    }
}
