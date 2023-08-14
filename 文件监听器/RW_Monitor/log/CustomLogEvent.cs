using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW_Monitor.log
{
   public  class CustomLogEvent
    {
        [JsonProperty("LogDate")]
        public string LogDate { get; set; }
        [JsonProperty("LogThread")]
        public string LogThread { get; set; }
        [JsonProperty("LogLevel")]
        public string LogLevel { get; set; }
        [JsonProperty("LogLogger")]
        public string LogLogger { get; set; }
        [JsonProperty("LogMessage")]
        public string LogMessage { get; set; }
        [JsonProperty("LogActionClick")]
        public string LogActionClick { get; set; }
        [JsonProperty("RequestType")]
        public string RequestType { get; set; }
        [JsonProperty("Path")]
        public string Path { get; set; }
        [JsonProperty("CreatedBy")]
        public int CreatedBy { get; set; }
        [JsonProperty("CreatedAt")]
        public string CreatedAt { get; set; }

        [JsonProperty("LastModifiedBy")]
        public int LastModifiedBy { get; set; }

        [JsonProperty("LastModifiedAt")]
        public string LastModifiedAt { get; set; }

        [JsonProperty("IsDeleted")]
        public byte IsDeleted { get; set; }
    }
}
