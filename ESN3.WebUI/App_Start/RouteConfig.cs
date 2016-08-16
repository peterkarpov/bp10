using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ESN3.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: null,
                url: "",
                defaults: new { controller = "Profile", action = "Index" }
            );

            //routes.MapRoute(
            //    name: null,
            //    url: "{controller}",
            //    defaults: new { action = "Index" }
            //);

            //routes.MapRoute(
            //    name: null,
            //    url: "Authentication/{action}",
            //    defaults: new { controller = "Authentication" }
            //);

            //routes.MapRoute(
            //    name: "profile",
            //    url: "{login}/{controller}",
            //    defaults: new { action = "Index", login = UrlParameter.Optional, page = 1 }
            //);

            //routes.MapRoute(
            //    name: null,
            //    url: "{login}/{controller}/Page{page}",
            //    defaults: new { action = "Index", login = UrlParameter.Optional, },
            //    constraints: new { page = @"\d+" }
            //);




            //routes.MapRoute(
            //    name: null,
            //    url: "Page{page}",
            //    defaults: new { controller = "Game", action = "List", category = (string)null },
            //    constraints: new { page = @"\d+" }
            //);

            routes.MapRoute(null, "{controller}/{action}");
        }
    }
}
