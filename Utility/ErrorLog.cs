using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Utility
{
    public sealed class ErrorLog
    {
        const string logger = "ILogger";
        private static readonly ILog _logger;
        private static ILog GetLogger(string name)
        {
            ILog log = LogManager.GetLogger(name);
            return log;
        }
        static ErrorLog()
        {
            _logger = GetLogger(logger);
        }

        public const int Info = 1;
        public const int Warn = 2;
        public const int Error = 3;
        public const int Fatal = 4;
        public const int Debug = 5;
        public static void Log(string message, int type = Error)
        {
            switch (type)
            {
                case 1:
                    _logger.InfoFormat(message);
                    break;
                case 2:
                    _logger.WarnFormat(message);
                    break;
                case 3:
                    _logger.ErrorFormat(message);
                    break;
                case 4:
                    _logger.FatalFormat(message);
                    break;
                case 5:
                    _logger.DebugFormat(message);
                    break;
            }
        }
    }
}
