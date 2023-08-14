using log4net.Core;
using log4net.Layout.Pattern;

namespace RW_Monitor.Log4net
{
    public class ActionConverter:PatternLayoutConverter
    {
        protected override void Convert(System.IO.TextWriter writer, LoggingEvent loggingEvent)
        {
            var actionInfo = loggingEvent.MessageObject as ActionLoggerInfo;

            if (actionInfo == null)
            {
                writer.Write("");
            }
            else
            {
                switch (this.Option.ToLower())
                {
                    case "requesttype":
                        writer.Write(actionInfo.RequestType);
                        break;
                    case "path":
                        writer.Write(actionInfo.Path);
                        break;
                    case "message":
                        writer.Write(actionInfo.Message);
                        break;
                    default:
                        writer.Write("");
                        break;
                }
            }
        }
    }
}
