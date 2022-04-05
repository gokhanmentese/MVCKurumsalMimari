using EGYS.Logger;
using Newtonsoft.Json;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGYS.Logger
{
    public class EventLogger : IEGYSLogger
    {
        protected readonly object lockObj = new object();
        private EventViewerSources _eventViewerSources;
        private EventViewerLogNames _eventViewerLogNames;

        public EventLogger()
        {
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
            SaveLog(message, EventLogEntryType.FailureAudit);
        }

        public void Debug(object message, Exception exception)
        {
            SaveLog(message, EventLogEntryType.FailureAudit);
        }

        public void Error(object message)
        {
            SaveLog(message, EventLogEntryType.Error);
        }

        public void Error(object message, Exception exception)
        {
            SaveLog(message, EventLogEntryType.Error);
        }

        public void Fatal(object message)
        {
            SaveLog(message, EventLogEntryType.Error);
        }

        public void Fatal(object message, Exception exception)
        {
            SaveLog(message, EventLogEntryType.Error);
        }

        public void Info(object message, Exception exception)
        {
            SaveLog(message, EventLogEntryType.Information);
        }

        public void Info(object message)
        {
            SaveLog(message, EventLogEntryType.Information);
        }


        public void Warn(object message)
        {
            SaveLog(message, EventLogEntryType.Warning);
        }

        public void Warn(object message, Exception exception)
        {
            SaveLog(message,EventLogEntryType.Warning);
        }

        private void SaveLog(object logMessage, EventLogEntryType logType)
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

                    eventLog.WriteEntry(json, logType); 
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
