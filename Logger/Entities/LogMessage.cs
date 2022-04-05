using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGYS.Logger
{
    public interface ILogEntity
    {

    }

    public class LogMessage : ILogEntity
    {
        public string Message { get; set; }

        public Type Type { get; set; }

        public string Location { get; set; }

        public LogType  LogType { get; set; }
    }
}
