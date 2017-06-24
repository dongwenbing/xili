using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MB.GameUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
          //  AreaRegistration.RegisterAllAreas();
          //  FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
          //  RouteConfig.RegisterRoutes(RouteTable.Routes);  更换路由配置
            MB.Controlles.SetUp.MBRoutes.RegisterRoutes(RouteTable.Routes);
         //   BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
