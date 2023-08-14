using Magicodes.ExporterAndImporter.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Orders
{
    public class ExportTemplate
    {
        [ExporterHeader(DisplayName = "id")]
        public int id { get; set; }

        [ExporterHeader(DisplayName = "编号")]

        public string AxleNumber { get; set; }

        [ExporterHeader(DisplayName = "型号")]
        public string AxleModel { get; set; }

        [ExporterHeader(DisplayName = "RFID")]
        public string RFID { get; set; }

        [ExporterHeader(DisplayName = "状态")]
        public string CurrentState { get; set; }

        [ExporterHeader(DisplayName = "创建时间")]
        public DateTime CreationTime { get; set; }

        [ExporterHeader(DisplayName = "操作人")]
        public string Operator { get; set; }

        [ExporterHeader(DisplayName = "是否删除")]
        public string IsDeleted { get; set; }
        
    }
    
}
