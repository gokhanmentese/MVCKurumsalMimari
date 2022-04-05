using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EGYS.Logger.Core
{
    public class LoggingEvent : ISerializable
    {
        public const string HostNameProperty = "log4net:HostName";
        public const string IdentityProperty = "log4net:Identity";
        public const string UserNameProperty = "log4net:UserName";

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public LoggingEvent(LogMessage data)
        {

        }
    }
}
