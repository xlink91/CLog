using CLogManagement.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CLogManagement.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.DependencyResolver =
                new DefaultDependencyResolver(UnityContainerCreator.Create());
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
