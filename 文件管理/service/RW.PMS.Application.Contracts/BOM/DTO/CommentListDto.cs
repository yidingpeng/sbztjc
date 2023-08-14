using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.BOM.DTO
{
    public class CommentListDto
    {
        public int Index { get; set; }
        public string Node { get; set; }
        public string User { get; set; }
        public string Comment { get; set; }
        public DateTime CommentTime { get; set; }
        public bool Result { get; set; }
    }
}
