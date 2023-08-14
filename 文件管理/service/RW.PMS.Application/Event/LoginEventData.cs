using RW.PMS.CrossCutting.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Event
{
    public class LoginEventData : EventData
    {
        public string Username { get; set; }
        public string Realname { get; set; }
        public bool Result { get; set; }
        public DateTime? LastLogin { get; internal set; }
    }
}
