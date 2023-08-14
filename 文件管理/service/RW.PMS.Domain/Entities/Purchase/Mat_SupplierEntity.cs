using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Purchase
{
    /// <summary>
    /// 外协供应商表
    /// </summary>
    [Table(Name = "mat_supplier")]
    public class Mat_SupplierEntity: FullEntity
    {
        /// <summary>
        /// 物料属性
        /// </summary>
        public string MatProperty { get; set; }
        /// <summary>
        /// 供应商编码
        /// </summary>
        public string SupCode { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupName { get; set; }
        /// <summary>
        /// 主要供货品牌
        /// </summary>
        public string SupplyBrand { get; set; }
        /// <summary>
        /// 主要供货类别
        /// </summary>
        public string SupplyType { get; set; }
        /// <summary>
        /// 公司性质
        /// </summary>
        public string CompanyType { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string SupPrincipal { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string ContactDetails { get; set; }
        /// <summary>
        /// 供应商地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        
    }
}
