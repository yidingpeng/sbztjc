using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Project
{
    public class Client_CompanyView
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 单位编号
        /// </summary>
        public string CompanyCode { get; set; }
        public dynamic Code { get; set; }
    }
}
