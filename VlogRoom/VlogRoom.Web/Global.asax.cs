using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using VlogRoom.Data;
using VlogRoom.Web.App_Start;

namespace VlogRoom.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // create the database on the application start
            Database.SetInitializer<MsSqlDbContext>(null);

            log4net.Config.XmlConfigurator.Configure();
            AutoMapperConfig.Initialize(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
