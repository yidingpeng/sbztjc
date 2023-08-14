using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Model.WorkStep
{
   public class WorkStep : BaseModel
    {
      public  int id { get; set; }
       public string description { get; set; }
       public string stepVideo { get; set; }
    }
}
