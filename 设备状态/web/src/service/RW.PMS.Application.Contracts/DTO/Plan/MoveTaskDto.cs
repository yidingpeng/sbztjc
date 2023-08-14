using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Plan
{
    public class MoveTaskDto
    {
        public Guid From { get; set; }
        public Guid Dest { get; set; }
    }
}
