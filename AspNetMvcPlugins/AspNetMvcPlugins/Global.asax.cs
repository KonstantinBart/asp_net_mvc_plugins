﻿using System.Web.Mvc;
using System.Web.Routing;
using AspNetMvcPlugins.Infrastructure;

namespace AspNetMvcPlugins
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

			PluginsHelper.RegisterViews();
        }
    }
}
