using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Basics
{
    public class ProductExtendDto
    {
        public int Id { get; set; }

        /// <summary>
        ///     产品型号ID
        /// </summary>
        public int PModelID { get; set; }
        public string PModelTxt { get; set; }
        /// <summary>
        ///     字段名称
        /// </summary>
        public string colName { get; set; }
        /// <summary>
        ///     界面列名称
        /// </summary>
        public string textName { get; set; }
        /// <summary>
        ///     列类型
        /// </summary>
        public int textType { get; set; }
        /// <summary>
        ///     界面列说明
        /// </summary>
        public string textMemo { get; set; }
        /// <summary>
        ///     字段值
        /// </summary>
        public string extendValue { get; set; }
    }
}
