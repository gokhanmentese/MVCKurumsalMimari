using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGYS.Logger.Concrete
{
    public class AllLogger : IEGYSLogger
    {
        private string _libraryName;
        private readonly string _connectionString;
        private EventViewerSources _eventViewerSources;
        private EventViewerLogNames _eventViewerLogNames;
        protected readonly object lockObj = new object();

        public AllLogger()
        {
            _libraryName = @"E:\Log\log.json";
            _connectionString = "Data Source=srv;Initial Catalog=EGYS;User ID=egysadmin;Password=asd123!;";
            _eventViewerSources = EventViewerSources.EGYS;
            _eventViewerLogNames = EventViewerLogNames.WebLog;
        }

        public bool IsFatalEnabled => true;

        public bool IsWarnEnabled => true;

        public bool IsInfoEnabled => true;

        public bool IsDebugEnabled => true;

        public bool IsErrorEnabled => true;

        public IEGYSLogger successor { get; set; }

        public void Debug(object message)
        {
            AllLog(message, LogType.Verbose);
        }

        public void Debug(object message, Exception exception)
        {
            AllLog(message, LogType.Verbose);
        }

        public void Error(object message)
        {
            AllLog(message, LogType.Error);
        }

        public void Error(object message, Exception exception)
        {
            AllLog(message, LogType.Error);
        }

        public void Fatal(object message)
        {
            AllLog(message, LogType.Critical);
        }

        public void Fatal(object message, Exception exception)
        {
            AllLog(message, LogType.Critical);
        }

        public void Info(object message, Exception exception)
        {
            AllLog(message, LogType.Information);
        }

        public void Info(object message)
        {
            AllLog(message, LogType.Information);
        }

        public void Warn(object message)
        {
            AllLog(message, LogType.Warning);
        }

        public void Warn(object message, Exception exception)
        {
            AllLog(message,LogType.Warning);
        }

        public void AllLog(object message, LogType  logType)
        {
            LogFile(message,logType);
            LogDatabase(message, logType);
            LogEvent(message, logType);
        }

        public void LogFile(object logMessage, LogType logType)
        {
            try
            {
                lock (lockObj)
                {
                    var json = JsonConvert.SerializeObject(logMessage, Formatting.Indented);

                    // File.WriteAllText(@"D:\JsonIslemlerim\Kullaniciler.json", json);

                    FileInfo fileInfo = new FileInfo(_libraryName);

                    using (StreamWriter streamWriter = fileInfo.AppendText())
                    {
                        try
                        {
                            streamWriter.WriteLine(json);
                            streamWriter.WriteLine("\r\n-------------------------------\r\n");
                        }
                        catch (Exception ex)
                        {
                            streamWriter.Flush();
                            streamWriter.Dispose();
                            streamWriter.Close();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void LogDatabase(object logMessage, LogType logType)
        {
            try
            {
                lock (lockObj)
                {
                    var json = JsonConvert.SerializeObject(logMessage, Formatting.Indented);

                    string sql = @"INSERT INTO Logs ([Detail],[Date],[Audit]) VALUES (@logDetail,@log_date,@audit)";

                    using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                        {
                            if (sqlCommand.Connection.State == System.Data.ConnectionState.Closed)
                                sqlCommand.Connection.Open();

                            sqlCommand.Parameters.AddWithValue("@logDetail", json);
                            sqlCommand.Parameters.AddWithValue("@log_date", DateTime.Now);
                            sqlCommand.Parameters.AddWithValue("@audit", logType.ToString());

                            sqlCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void LogEvent(object logMessage, LogType logType)
        {
            try
            {
                lock (lockObj)
                {
                    var json = JsonConvert.SerializeObject(logMessage, Formatting.Indented);

                    EventLog eventLog = new EventLog();
                    if (!EventLog.SourceExists(_eventViewerSources.ToString()))
                    {
                        EventLog.CreateEventSource(_eventViewerSources.ToString(), _eventViewerLogNames.ToString());
                    }

                    eventLog.Source = _eventViewerSources.ToString();

                    eventLog.WriteEntry(json, logType.ToEventLogEntryType());
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
