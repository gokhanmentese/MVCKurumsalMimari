using EGYS.Logger;
using System;

namespace UserSynch
{
    class Program
    {
        static void Main(string[] args)
        {
            IEGYSLogger logger = LogManager.GetLogger(LogTarget.All);

            logger.Info(
                new LogMessage 
                { 
                    Message="Test",
                    Location= "static void Main(string[] args)",
                    LogType=LogType.Information,
                    Type=typeof(Program)
                }
            );
        }
    }
}
