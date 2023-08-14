using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.BOM.DTO
{
    public class CommentResult
    {
        public int BomId { get; set; }

        public string Node { get; set; }
        public bool Result { get; set; }
        public string Comment { get; set; }
    }
}
