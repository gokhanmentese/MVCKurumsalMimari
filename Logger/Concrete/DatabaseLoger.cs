using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGYS.Logger
{
    public class DatabaseLoger : IEGYSLogger
    {
        protected readonly object lockObj = new object();
        private readonly string _connectionString;

        public DatabaseLoger()
        {
            _connectionString = "Data Source=srv;Initial Catalog=EGYS;User ID=egysadmin;Password=asd123!;";
        }

        public bool IsFatalEnabled => true;

        public bool IsWarnEnabled => true;

        public bool IsInfoEnabled => true;

        public bool IsDebugEnabled => true;

        public bool IsErrorEnabled => true;

        public IEGYSLogger successor { get; set; } = new FileLogger();

        public void Debug(object message)
        {
            Log(message, LogType.Verbose);
        }

        public void Debug(object message, Exception exception)
        {
            Log(message, LogType.Verbose);
        }

        public void Error(object message)
        {
            Log(message, LogType.Error);
        }

        public void Error(object message, Exception exception)
        {
            Log(message, LogType.Error);
        }

        public void Fatal(object message)
        {
            Log(message, LogType.Critical);
        }

        public void Fatal(object message, Exception exception)
        {
            Log(message, LogType.Critical);
        }

        public void Info(object message, Exception exception)
        {
            Log(message, LogType.Information);
        }

        public void Info(object message)
        {
            Log(message, LogType.Information);
        }

        public void Warn(object message)
        {
            try
            {
                Log(message, LogType.Warning);
            }
            catch (Exception)
            {
                if (successor != null)
                {
                    successor.Warn(message);
                }
            }
        }

        public void Warn(object message, Exception exception)
        {
            try
            {
                Log(message, LogType.Warning);
            }
            catch (Exception)
            {
                if (successor != null)
                {
                    successor.Warn(message,exception);
                }
            }
        }

        private void Log(object logMessage, LogType logType)
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
                throw ex;
            }
        }
    }
}
