using System;

namespace RW.PMS.Application.Contracts.Output.Message
{
    public class MessageReceiverOutput
    {
        public int Id { get; set; }

        public string RealName { get; set; }

        public DateTime RemindTime { get; set; }

        public bool IsRead { get; set; }

        public DateTime? ReadTime { get; set; }
    }
}