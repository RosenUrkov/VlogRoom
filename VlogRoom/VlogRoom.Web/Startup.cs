﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VlogRoom.Web.Startup))]
namespace VlogRoom.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
