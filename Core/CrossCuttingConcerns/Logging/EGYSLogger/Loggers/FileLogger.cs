using EGYS.Logger;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging.EGYSLogger.Loggers
{
    public class FileLogger : LoggerServiceBase
    {
        public FileLogger() : base(LogTarget.File)
        {
        }
    }
}
