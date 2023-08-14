using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.WorkerService
{
    public class user
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int id { get; set; }
        public string name { get; set; }

      
    }
}
