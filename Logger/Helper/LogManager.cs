using EGYS.Logger.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGYS.Logger
{
    public static class LogManager
    {
        public static IEGYSLogger GetLogger(LogTarget target)
        {
             IEGYSLogger logger = null;

            switch (target)
            {
                case LogTarget.File:
                    logger =  new FileLogger();
                    break;
                case LogTarget.Database:
                    logger = new EGYS.Logger.DatabaseLoger();
                    break;
                case LogTarget.EventLog:
                    logger = new EventLogger();
                    break;
                case LogTarget.All:
                    logger = new AllLogger();
                    break;
                default:
                    throw new NotImplementedException();
            }

            return logger;
        }
    }
}
