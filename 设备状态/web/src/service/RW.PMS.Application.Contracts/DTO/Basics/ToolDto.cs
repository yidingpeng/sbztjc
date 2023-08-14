using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Basics
{
    /// <summary>
    ///     设备基础信息表
    /// </summary>
    public class ToolDto
    {
        public int Id { get; set; }

        /// <summary>
        ///     工具编码
        /// </summary>
        public string toolCode { get; set; }
        /// <summary>
        ///     工具名称
        /// </summary>
        public string toolName { get; set; }
        /// <summary>
        ///     工具类别
        /// </summary>
        public int? toolClassID { get; set; }
        public string toolClassTxt { get; set; }
        /// <summary>
        ///     工具类型
        /// </summary>
        public int toolTypeID { get; set; }
        public string toolTypeTxt { get; set; }
        /// <summary>
        ///     规格型号
        /// </summary>
        public int? toolModel { get; set; }
        /// <summary>
        ///     品牌
        /// </summary>
        public string toolBrand { get; set; }
        /// <summary>
        ///     证书编号
        /// </summary>
        public string toolCertificate { get; set; }
        /// <summary>
        ///     购买日期
        /// </summary>
        public DateTime purchaseTime { get; set; }
        /// <summary>
        ///     生效时间
        /// </summary>
        public DateTime effectTime { get; set; }
        /// <summary>
        ///     失效时间
        /// </summary>
        public DateTime failuretime { get; set; }
        /// <summary>
        ///     装配时是否要录工具编码
        /// </summary>
        public int toolIsHasCode { get; set; }
        /// <summary>
        ///     状态
        /// </summary>
        public int usingFlag { get; set; }
        /// <summary>
        ///     备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public dynamic PicId { get; set; }

        /// <summary>
        ///  反馈图片数组字符串
        /// </summary>
        public string imgFilesId { get; set; }

        /// <summary>
        /// 存储图片数组
        /// </summary>
        public int[] imgFeilsArr { get; set; }
    }
}
