using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TTPAPI.Models;

namespace TTPAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
            name: "Login",
            routeTemplate: "api/Login/Login",
            defaults: new { id = RouteParameter.Optional },
            constraints: null,
            handler: new MessageHandler()  // per-route message handler
        );

            config.MessageHandlers.Add(new MessageHandler());  // global message handler
        }
    }
}
