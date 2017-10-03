using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using VlogRoom.Services.Common.Contracts;

namespace VlogRoom.Web.Common.ActionFilters
{
    public class LoggerFilter : IExceptionFilter
    {
        private readonly ILoggerService logger;

        public LoggerFilter(ILoggerService logger)
        {
            this.logger = logger;
        }

        public void OnException(ExceptionContext filterContext)
        {
            logger.Log(
                string.Format("Exception occured on route {0} with message '{1}' and stack trace {2}",
                filterContext.HttpContext.Request.RawUrl,
                filterContext.Exception.Message,
                filterContext.Exception.StackTrace));
        }
    }
}
