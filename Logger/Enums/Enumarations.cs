using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGYS.Logger
{
    public enum EventViewerSources
    {
        WindowsServices = 1,
        Plugin = 2,
        EGYS = 3
    }

    public enum EventViewerLogNames
    {
        WindowsServicesLog = 1,
        PluginLog = 2,
        WebLog = 3
    }

    public enum LogType
    {
        Warning = 0,
        Error = 1,
        Information = 2,
        Critical = 3,
        Verbose = 4
    }

    public enum LogTarget
    {
        None=0,
        File=1, 
        Database=2, 
        EventLog=3,
        All=4
    }
}
