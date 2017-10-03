using System.Web;
using System.Web.Mvc;
using VlogRoom.Services.Common.Contracts;
using VlogRoom.Web.Common;
using VlogRoom.Web.Common.ActionFilters;
using VlogRoom.Web.Common.Attributes;

namespace VlogRoom.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LoggerFilter(ServiceLocator.Provider.GetService<ILoggerService>()));
        }
    }
}
