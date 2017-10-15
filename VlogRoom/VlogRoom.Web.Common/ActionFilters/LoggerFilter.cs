using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using VlogRoom.Services.Common.Contracts;
using VlogRoom.Web.Common.Constants;

namespace VlogRoom.Web.Common.ActionFilters
{
    public class LoggerFilter : IExceptionFilter
    {
        private readonly ILoggerService logger;

        public LoggerFilter(ILoggerService logger)
        {
            Guard.WhenArgument(logger, "logger").IsNull().Throw();
            this.logger = logger;
        }

        public void OnException(ExceptionContext filterContext)
        {
            logger.Log(
                string.Format(GlobalConstants.LoggingTemplate,
                filterContext.HttpContext.Request.RawUrl,
                filterContext.Exception.Message,
                filterContext.Exception.StackTrace));
        }
    }
}
