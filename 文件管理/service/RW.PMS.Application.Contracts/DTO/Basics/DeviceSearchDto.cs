using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Basics
{
    public class DeviceSearchDto : PagedQuery
    {
        public int? Id { get; set; }

        ///     设备名称
        /// </summary>
        public string devName { get; set; }
        /// <summary>
        ///     设备编码
        /// </summary>
        public string devNo { get; set; }
       
    }
}
