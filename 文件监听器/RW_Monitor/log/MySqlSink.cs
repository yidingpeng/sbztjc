using MySql.Data.MySqlClient;
using Serilog.Events;
using Serilog.Sinks.PeriodicBatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW_Monitor.log
{
   public class MySqlSink : PeriodicBatchingSink
    {
        private readonly string _connectionString;
        private readonly string _tableName;

        public MySqlSink(string connectionString, string tableName)
          : base(batchSizeLimit: 1000, period: TimeSpan.FromSeconds(5))
        {
            _connectionString = connectionString;
            _tableName = tableName;
        }
        protected override void EmitBatch(IEnumerable<LogEvent> events)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                foreach (var logEvent in events)
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $"INSERT INTO {_tableName} (LogDate, LogThread, LogLevel, LogLogger, LogMessage, LogActionClick, RequestType,Path,CreatedBy,CreatedAt,LastModifiedBy,LastModifiedAt,IsDeleted) " +
                                              $"VALUES (@LogDate, @LogThread, @LogLevel, @LogLogger, @LogMessage, @LogActionClick, @RequestType,@Path,@CreatedBy,@CreatedAt,@LastModifiedBy,@LastModifiedAt,@IsDeleted)";
                        var s = logEvent.Properties.ContainsKey("Account");


                        if (logEvent.Properties.TryGetValue("CustomLogEvent", out var logEventPropertyValue))
                        {

                            if (logEventPropertyValue is StructureValue structureValue)
                            {
                                var customLog = new CustomLogEvent();
                                foreach (var property in structureValue.Properties)
                                {
                                    var propertyName = property.Name;
                                    var propertyValue = property.Value;

                                    switch (propertyName)
                                    {
                                        case "LogDate":
                                            customLog.LogDate = propertyValue.ToString();
                                            break;
                                        case "LogThread":
                                            customLog.LogThread = propertyValue.ToString();
                                            break;
                                        case "LogLevel":
                                            customLog.LogLevel = propertyValue.ToString();
                                            break;
                                        case "LogLogger":
                                            customLog.LogLogger = propertyValue.ToString();
                                            break;
                                        case "LogMessage":
                                            customLog.LogMessage = propertyValue.ToString();
                                            break;
                                        case "LogActionClick":
                                            customLog.LogActionClick = propertyValue.ToString();
                                            break;
                                        case "RequestType":
                                            customLog.RequestType = propertyValue.ToString();
                                            break;
                                        case "Path":
                                            customLog.Path = propertyValue.ToString();
                                            break;
                                        case "CreatedBy":
                                            customLog.CreatedBy = Convert.ToInt32( propertyValue.ToString());
                                            break;
                                        case "CreatedAt":
                                            customLog.CreatedAt = propertyValue.ToString();
                                            break;
                                        case "LastModifiedBy":
                                            customLog.LastModifiedBy = Convert.ToInt32(propertyValue.ToString());
                                            break;
                                        case "LastModifiedAt":
                                            customLog.LastModifiedAt = propertyValue.ToString();
                                            break;
                                        case "IsDeleted":
                                            customLog.IsDeleted = Convert.ToByte(propertyValue.ToString());
                                            break;
                                    }
                                }

                                command.Parameters.AddWithValue("@LogDate", customLog.LogDate.Replace("\"", ""));
                                command.Parameters.AddWithValue("@LogThread", customLog.LogThread.Replace("\"", ""));
                                command.Parameters.AddWithValue("@LogLevel", customLog.LogLevel);
                                command.Parameters.AddWithValue("@LogLogger", customLog.LogLogger.Replace("\"", ""));
                                command.Parameters.AddWithValue("@LogMessage", customLog.LogMessage.Replace("\"", ""));
                                command.Parameters.AddWithValue("@LogActionClick", customLog.LogActionClick.Replace("\"", ""));
                                command.Parameters.AddWithValue("@RequestType", customLog.RequestType.Replace("\"", ""));
                                command.Parameters.AddWithValue("@Path", customLog.Path.Replace("\"", ""));
                                command.Parameters.AddWithValue("@CreatedBy", customLog.CreatedBy);
                                command.Parameters.AddWithValue("@CreatedAt", customLog.CreatedAt.Replace("\"", ""));
                                command.Parameters.AddWithValue("@LastModifiedBy", customLog.LastModifiedBy);
                                command.Parameters.AddWithValue("@LastModifiedAt", customLog.LastModifiedAt.Replace("\"", ""));
                                command.Parameters.AddWithValue("@IsDeleted", customLog.IsDeleted);
                            }
                        }

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
