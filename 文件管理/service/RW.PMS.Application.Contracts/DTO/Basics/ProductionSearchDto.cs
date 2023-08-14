using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Basics
{
    public class ProductionSearchDto : PagedQuery
    {
        public int? Id { get; set; }
        public string PCode { get; set; }
        public string pname { get; set; }
    }
}
