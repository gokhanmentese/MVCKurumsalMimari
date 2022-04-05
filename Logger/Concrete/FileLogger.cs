using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace EGYS.Logger
{
   
    public class FileLogger : IEGYSLogger
    {
        private  string _libraryName;
        protected readonly object lockObj = new object();

        public FileLogger()
        {
            _libraryName = @"E:\Log\log.json";
        }

        public IEGYSLogger successor { get; set; } = new EventLogger();

        public bool IsFatalEnabled => true;

        public bool IsWarnEnabled => true;

        public bool IsInfoEnabled => true;

        public bool IsDebugEnabled => true;

        public bool IsErrorEnabled => true;


        public void Debug(object message)
        {
            Log(message);
        }

        public void Debug(object message, Exception exception)
        {
            Log(message);
        }

        public void Error(object message)
        {
            try
            {
                Log(message);
            }
            catch (Exception)
            {
                if (successor != null)
                {
                    successor.Error(message);
                }
            }
        }

        public void Error(object message, Exception exception)
        {
            try
            {
                Log(message);
            }
            catch (Exception)
            {
                if (successor != null)
                {
                    successor.Error(message, exception);
                }
            }
        }

        public void Fatal(object message)
        {
            Log(message);
        }

        public void Fatal(object message, Exception exception)
        {
            Log(message);
        }

        public void Info(object message, Exception exception)
        {
            try
            {
                Log(message);
            }
            catch (Exception)
            {
                if (successor != null)
                {
                    successor.Info(message, exception);
                }
            }
        }

        public void Info(object message)
        {
            try
            {
                Log(message);
            }
            catch (Exception)
            {
                if (successor != null)
                {
                    successor.Info(message);
                }
            }
        }

        public void Warn(object message)
        {
            try
            {
                Log(message);
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
                Log(message);
            }
            catch (Exception)
            {
                if (successor != null)
                {
                    successor.Warn(message,exception);
                }
            }
        }

        public void Log(object logMessage)
        {
            try
            {
                lock (lockObj)
                {
                    //string fileName = DateTime.Now.ToString().Replace(".", "").Replace(":", "");
                    //string filepath = libraryname+"\\" + fileName + ".txt";

                    //if (!Directory.Exists(_libraryName))
                    //    Directory.CreateDirectory(_libraryName);

                    //var logEvent = new SerializableLogEvent(loggingEvent);
                    var json = JsonConvert.SerializeObject(logMessage, Formatting.Indented);

                   // File.WriteAllText(@"D:\JsonIslemlerim\Kullaniciler.json", json);

                    FileInfo fileInfo = new FileInfo(_libraryName);

                    using (StreamWriter streamWriter = fileInfo.AppendText())
                    {
                        try
                        {
                            //string newLog = "-------------------------------\r\n";
                            //newLog += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n";
                            //newLog += logMessage.LogType.ToString() + "\r\n";
                            //newLog += logMessage.Message;

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
                throw;
            }
        }

    }

    public class FileLogger2 : IEGYSLogger2
    {
        private string _libraryName;

        public FileLogger2()
        {
            _libraryName = @"E:\Log\log.json";
        }

        public override void Log(object logMessage)
        {
            try
            {
                lock (lockObj)
                {
                    //string fileName = DateTime.Now.ToString().Replace(".", "").Replace(":", "");
                    //string filepath = libraryname+"\\" + fileName + ".txt";

                    //if (!Directory.Exists(_libraryName))
                    //    Directory.CreateDirectory(_libraryName);

                    //var logEvent = new SerializableLogEvent(loggingEvent);
                    var json = JsonConvert.SerializeObject(logMessage, Formatting.Indented);

                    // File.WriteAllText(@"D:\JsonIslemlerim\Kullaniciler.json", json);

                    FileInfo fileInfo = new FileInfo(_libraryName);

                    using (StreamWriter streamWriter = fileInfo.AppendText())
                    {
                        try
                        {
                            //string newLog = "-------------------------------\r\n";
                            //newLog += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\r\n";
                            //newLog += logMessage.LogType.ToString() + "\r\n";
                            //newLog += logMessage.Message;

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
    }
}
