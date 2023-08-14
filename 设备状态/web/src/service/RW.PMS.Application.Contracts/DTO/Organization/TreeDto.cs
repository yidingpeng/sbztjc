using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Organization
{
    public class TreeDto
    {
        public int ID { get; set; }

        public string Label { get; set; }

        public List<TreeDto> Children { get; set; }
    }
}
