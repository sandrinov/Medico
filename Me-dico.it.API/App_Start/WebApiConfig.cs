using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Me_dico.it.API
{
    public static class WebApiConfig
    {
        //public static HttpConfiguration Register()
        //{
        //    var config = new HttpConfiguration();

        //    // Web API routes
        //    config.MapHttpAttributeRoutes();
        //    config.Routes.MapHttpRoute(
        //             name: "DefaultRouting",
        //    routeTemplate: "api/{controller}/{id}",
        //         defaults: new { id = RouteParameter.Optional }
        //    );

        //    config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

        //    var json = config.Formatters.JsonFormatter;
        //    json.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
        //    json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

        //    return config;
        //}

        internal static void Register(HttpConfiguration config)
        {
            //config.EnableCors();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                     name: "DefaultRouting",
            routeTemplate: "api/{controller}/{id}",
                 defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

        }
    }
}