using AspNetMvcPlugins.App_Start;
using System.Web.Mvc;
using System.Web.Routing;

namespace AspNetMvcPlugins
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            AutofacRegistrator.Init();
        }
    }
}
