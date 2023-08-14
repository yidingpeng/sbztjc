using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Orders
{
    [Table(Name = "AxleOrders")]
    public class OrdersEntity: IEDRoomJiaoZhunTimeEntityntity<int>
    {
        
        public string AxleNumber { get; set; }
        public string AxleModel { get; set; }

        public string RFID { get; set; }
        public string CurrentState { get; set; }
        public DateTime CreationTime { get; set; }
        public string Operator { get; set; }
        public string IsDeleted { get; set; }
    }
}
