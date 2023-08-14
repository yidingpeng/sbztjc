using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Basics
{  
    /// <summary>
    ///     产品信息
    /// </summary>
    public class ProductionDto
    {
        public int Id { get; set; }
        public string Pname { get; set; }
        public string PCode { get; set; }
        public string Size { get; set; }
        public string Category { get; set; }
        public int Pstatus { get; set; }
        public string remark { get; set; }

        public string status { get { return Pstatus == 0 ? "启用":"禁用"; } }
    }
}
