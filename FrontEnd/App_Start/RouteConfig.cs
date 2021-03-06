﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FrontEnd
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

          /*  routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Aldea", id = UrlParameter.Optional }
            );*/

            routes.MapRoute(
                name: "Default",
                url: "{tenant}/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "LoginFacebook", id = UrlParameter.Optional, tenant = "tenant" }
            );
        }
    }
}
