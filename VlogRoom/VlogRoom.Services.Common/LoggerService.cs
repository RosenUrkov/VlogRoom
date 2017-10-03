using log4net;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VlogRoom.Services.Common.Contracts;

namespace VlogRoom.Services.Common
{
    public class LoggerService : ILoggerService
    {
        private readonly ILog logger;

        public LoggerService()
        {
            this.logger = LogManager.GetLogger(typeof(LoggerService));
        }

        public void Log(string message)
        {
            this.logger.Error(message);
        }
    }
}
