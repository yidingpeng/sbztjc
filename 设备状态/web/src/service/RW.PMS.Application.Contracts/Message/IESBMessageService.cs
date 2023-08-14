using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Contracts.Message
{
    public interface IESBMessageService
    {
        /// <summary>
        /// 直接发送消息到OA，并指定回调的URL
        /// </summary>
        string SendToUser(string returnUrl, string title, params string[] users);
    }
}
