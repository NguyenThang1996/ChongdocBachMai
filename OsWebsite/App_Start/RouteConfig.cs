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
            routes.MapRoute(
            name: "NOT404",
            url: "gio-hang",
            defaults: new { controller = "Cart", action = "Index" },
            namespaces: new[] { "OsWebsite.Controllers" }
            );
            routes.MapRoute(
            name: "Thank",
            url: "thank",
            defaults: new { controller = "Cart", action = "Thank" },
            namespaces: new[] { "OsWebsite.Controllers" }
            );
            routes.MapRoute(
            name: "Agency",
            url: "dang-ky-thanh-vien",
            defaults: new { controller = "Agency", action = "Index" },
            namespaces: new[] { "OsWebsite.Controllers" }
            );
           
            routes.MapRoute(
            name: "Search",
            url: "Search",
            defaults: new { controller = "Product", action = "Search" },
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
