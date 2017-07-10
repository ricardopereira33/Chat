using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Chat.App_Start.Connection;

namespace Chat
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Connection c = new Connection();
            Thread server = new Thread(new ThreadStart(c.Connect));
            server.Start();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
