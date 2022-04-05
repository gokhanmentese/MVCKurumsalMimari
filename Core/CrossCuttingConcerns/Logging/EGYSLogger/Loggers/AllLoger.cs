using EGYS.Logger;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging.EGYSLogger.Loggers
{
    public class AllLogger : LoggerServiceBase
    {
        public AllLogger() : base(LogTarget.All)
        {
        }
    }
}
