using EGYS.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGYS.Logger
{
    public interface IEGYSLogger
    {
        IEGYSLogger successor { get; set; }

        bool IsFatalEnabled { get; }
        bool IsWarnEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsDebugEnabled { get; }
        bool IsErrorEnabled { get; }

        void Error(object message);
        void Error(object message, Exception exception);

        void Fatal(object message);
        void Fatal(object message, Exception exception);

        void Info(object message, Exception exception);
        void Info(object message);

        void Warn(object message);
        void Warn(object message, Exception exception);

        void Debug(object message);
        void Debug(object message, Exception exception);
    }

    public abstract class IEGYSLogger2
    {
        protected readonly object lockObj = new object();

        public bool IsFatalEnabled { get; }
        public bool IsWarnEnabled { get; }
        public bool IsInfoEnabled { get; }
        public bool IsDebugEnabled { get; }
        public bool IsErrorEnabled { get; }

        public void Error(object message)
        {
            Log(message);
        }
        public void Error(object message, Exception exception)
        {
            Log(message);
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
            Log(message);
        }
        public void Info(object message)
        {
            Log(message);
        }

        public void Warn(object message)
        {
            Log(message);
        }
        public void Warn(object message, Exception exception)
        {
            Log(message);
        }

        public void Debug(object message)
        {
            Log(message);
        }
        public void Debug(object message, Exception exception)
        {
            Log(message);
        }

        public abstract void Log(object logMessage);
    }
}
