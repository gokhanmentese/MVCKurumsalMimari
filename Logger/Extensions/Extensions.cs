using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGYS.Logger
{
     static class Extensions
    {
        public static EventLogEntryType ToEventLogEntryType(this LogType s)
        {
            EventLogEntryType returnVal = EventLogEntryType.Information;
            switch (s)
            {
                case LogType.Information:
                    returnVal = EventLogEntryType.Information;
                    break;
                case LogType.Error:
                    returnVal = EventLogEntryType.Error;
                    break;
                case LogType.Critical:
                    returnVal = EventLogEntryType.Warning;
                    break;
                case LogType.Warning:
                    returnVal = EventLogEntryType.Warning;
                    break;
                default:
                    break;
            }
            return returnVal;
        }
    }
}
