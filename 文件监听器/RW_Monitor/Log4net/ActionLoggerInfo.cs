namespace RW_Monitor.Log4net
{
    public class ActionLoggerInfo
    {
        public string RequestType { get; set; }
        public string Path { get; set; }
        public string Message { get; set; }

        public ActionLoggerInfo(string requesttype, string path, string message)
        {
            this.RequestType = requesttype;
            this.Path = path;
            this.Message = message;
        }
    }
}
