using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Basics
{
    public class MaterialDto
    {
        public int Id { get; set; }

        /// <summary>
        ///     物料编码
        /// </summary>
        public string MtlCode { get; set; }
        /// <summary>
        ///     物料/零件名称
        /// </summary>
        public string MtlName { get; set; }
        /// <summary>
        ///     物料/零件规格型号
        /// </summary>
        public string MtlModel { get; set; }
        /// <summary>
        ///     图号
        /// </summary>
        public string wlFigureNo { get; set; }
        /// <summary>
        ///     物料分类
        /// </summary>
        public int MtlClassID { get; set; }
        public string MtlClassTxt { get; set; }
        /// <summary>
        ///     物料类型
        /// </summary>
        public int MtlTypeID { get; set; }
        public string MtlTypeTxt { get; set; }
        /// <summary>
        ///     基本单位
        /// </summary>
        public int baseUnitID { get; set; }
        public string baseUnitTxt { get; set; }
        /// <summary>
        ///     重要度
        /// </summary>
        public int importance { get; set; }
        public string importanceTxt { get; set; }
        /// <summary>
        ///     装配时是否要录物料编码
        /// </summary>
        public int MtlIsHasCode { get; set; }
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
        ///     材质
        /// </summary>
        public int texture { get; set; }
        public string textureTxt { get; set; }
        /// <summary>
        ///     重量
        /// </summary>
        public int weight { get; set; }
        /// <summary>
        ///     尺寸
        /// </summary>
        public string size { get; set; }
        /// <summary>
        ///     状态
        /// </summary>
        public int MtlStatus { get; set; }
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
