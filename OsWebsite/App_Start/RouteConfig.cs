using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OsWebsite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
            routes.MapRoute(
            name: "Home",
            url: "home",
            defaults: new { controller = "Home", action = "Index"},
            namespaces: new[] { "OsWebsite.Controllers" }
            );
            RouteTable.Routes.MapPageRoute("URL", "{Tag}", "~/URL.aspx");
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OsWebsite.Controllers" }
            );

        }
    }
}
