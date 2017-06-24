using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MB.Controlles.SetUp
{
    public static class MBRoutes
    {

        public static void RegisterRoutes(RouteCollection  routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //default   routes  UrlParameter
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
            //defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
             defaults: new { controller = "Weixin", action = "WeixinSdk", id = UrlParameter.Optional }
            //  defaults: new { controller = "Weixin", action = "Demo", id = UrlParameter.Optional }
            );

        }

    }
}
