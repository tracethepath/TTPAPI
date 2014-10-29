using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;

namespace TTPAPI.Models
{
    public class Test
    {
        static void Main(string[] args)
        {

            ////var config = new HttpSelfHostConfiguration("http://localhost:8080/");



            //IHttpRoute route = config.Routes.CreateRoute(

            //    routeTemplate: "hello",

            //    defaults: new HttpRouteValueDictionary("route"),

            //    constraints: null,

            //    dataTokens: null,

            //   parameters: null,

            //    handler: new HelloWorldMessageHandler());

            //config.Routes.Add("HelloRoute", route);



            //config.Routes.MapHttpRoute(

            //  name: "default",

            // routeTemplate: "api/{controller}/{id}",

            //defaults: new { controller = "Home", id = RouteParameter.Optional });



            //var server = new HttpSelfHostServer(config);

            //server.OpenAsync().Wait();

            //Console.WriteLine("Listening on {0}...", config.BaseAddress);

            //Console.ReadLine();
            //server.CloseAsync().Wait();

        }
    }
}