using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Plan
{
    public class EditTaskFiledDto
    {
        public Guid Id { get; set; }
        public string FiledName { get; set; }
        public object Value { get; set; }
    }
}
