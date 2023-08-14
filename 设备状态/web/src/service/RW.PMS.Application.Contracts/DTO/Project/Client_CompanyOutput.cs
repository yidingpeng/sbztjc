using RW.PMS.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class Client_CompanyOutput
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
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
        /// 客户级别文本
        /// </summary>
        public string ClientRankText { get; set; }
        /// <summary>
        /// 合作业务
        /// </summary>
        public int CooperativeBusiness { get; set; }
        /// <summary>
        /// 合作业务文本
        /// </summary>
        public string CooperativeBusinessText { get; set; }

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
        /// 公司总经理名字
        /// </summary>
        public string GMName { get; set; }

        /// <summary>
        /// 公司副总经理名字
        /// </summary>
        public string DeputyGMName { get; set; }

        /// <summary>
        /// 市场片区名
        /// </summary>
        public string MarketAreaName { get; set; }

        /// <summary>
        /// 营销负责人名字
        /// </summary>
        public string PersonInChargeName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
