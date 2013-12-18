using Microsoft.Data.Edm;
using MobSalesSrv.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;

namespace MobSalesSrv
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Routes.MapODataRoute("odata", "odata", GetEdmModel());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.EnableQuerySupport();
           //config.EnableSystemDiagnosticsTracing();
           
        }
        public static IEdmModel GetEdmModel()
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Route>("Routes");
            builder.EntitySet<Customer>("Customers");
            builder.EntitySet<ProductType>("ProductTypes");
            builder.EntitySet<Product>("Products");
            builder.EntitySet<Order>("Orders");
            builder.EntitySet<OrderDetails>("OrderDetails");

            builder.Namespace = "MobSalesSrv.Models";
            return builder.GetEdmModel();
        }
    }
}
