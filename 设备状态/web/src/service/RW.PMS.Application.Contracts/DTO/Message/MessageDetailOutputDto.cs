using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Message
{
    public class MessageDetailOutputDto : MessageTopOutput
    {
        public string Content { get; set; }
    }
}
