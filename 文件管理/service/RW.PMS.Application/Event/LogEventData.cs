using RW.PMS.Application.Contracts.DTO.Log;
using RW.PMS.CrossCutting.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.PMS.Application.Event
{
    public class LogEventData : EventData
    {
        public LogEventData()
        {

        }

        public LogEventData(LogTypeEnum type, bool result, string message, string desc)
        {
            this.Type = type;
            this.Result = result;
            this.Message = message;
            this.Desc = desc;
        }

        public LogTypeEnum Type { get; set; }
        public bool Result { get; set; }
        public string Message { get; set; }
        public string Desc { get; set; }

        public static LogEventData LoginData(bool result, string message, string desc)
        {
            return new LogEventData(LogTypeEnum.Login, result, message, desc);
        }

        public static LogEventData OperationData(string action, bool result, string title)
        {
            string message = "{0}" + (result ? "成功" : "失败");
            string desc = $"用户[#user#] {{0}} {title} 。";
            string actionText = action;
            message = string.Format(message, actionText);
            desc = string.Format(desc, actionText);
            return new LogEventData(LogTypeEnum.Operation, result, message, desc);
        }

        public static LogEventData OperationData(OperationActionEnum action, bool result, string title)
        {
            string actionText = "未知操作";
            switch (action)
            {
                case OperationActionEnum.Insert: actionText = "增加"; break;
                case OperationActionEnum.Delete: actionText = "删除"; break;
                case OperationActionEnum.Update: actionText = "修改"; break;
                case OperationActionEnum.Clear: actionText = "清空"; break;
                case OperationActionEnum.Revoke: actionText = "撤回";break;
                case OperationActionEnum.Cancel: actionText = "取消"; break;
                default: break;
            }
            return OperationData(actionText, result, title);
        }
    }

    public enum OperationActionEnum
    {
        Insert,
        Delete,
        Update,
        Clear,
        /// <summary>
        /// 撤回
        /// </summary>
        Revoke,
        /// <summary>
        /// 取消
        /// </summary>
        Cancel,
    }
}
