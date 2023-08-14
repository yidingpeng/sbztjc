using FreeSql.DataAnnotations;
using RW.PMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Domain.Entities.Orders
{
    [Table(Name = "SprayingAmount")]
    public class SprayingAmountEntity : IEDRoomJiaoZhunTimeEntityntity<int>
    {
      
       

            public string oAxleModel { get; set; }
            public string sprayingParameter { get; set; }

        
    }
}
