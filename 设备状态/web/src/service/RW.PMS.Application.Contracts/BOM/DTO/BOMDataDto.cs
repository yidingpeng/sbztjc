using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.BOM.DTO
{
    public class BOMDataDto
    {
        public BOMTableDto Table { get; set; }

        public List<BOMItemDto> Items { get; set; }

        public List<CommentListDto> Comments { get; set; }
    }
}
