using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGYS.Logger.Core
{
    public class LocationInfo
    {
        public LocationInfo(Type callerStackBoundaryDeclaringType)
        {

        }
        public LocationInfo(string className, string methodName, string fileName, string lineNumber)
        {

        }

        public string ClassName { get; }
        public string FileName { get; }
        public string LineNumber { get; }
        public string MethodName { get; }
        public string FullInfo { get; }
    }
}
