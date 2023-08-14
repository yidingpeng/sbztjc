using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.DTO.Message;

public class MessageSendInputDto
{
    public string Type { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int[] UserIds { get; set; }
    public int DataId { get; set; }
}
