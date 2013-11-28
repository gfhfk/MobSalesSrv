namespace MobSalesSrv.Migrations
{
    using MobSalesSrv.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MobSalesSrv.Models.MobSalesSrvContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(MobSalesSrv.Models.MobSalesSrvContext context)
        {
            context.Routes.AddOrUpdate(
                r => r.RouteName,
                new Route { RouteName = "No route" },
                new Route { RouteName = "Monday" },
                new Route { RouteName = "Tuesday" },
                new Route { RouteName = "Wendnesday" },
                new Route { RouteName = "thursday" },
                new Route { RouteName = "Friday" },
                new Route { RouteName = "Saurday" }
            );
            context.SaveChanges();

            foreach (Route rout in context.Routes)
            {
                var customers = new Customer[10];
                for (int i = 0; i < customers.Length; i++)
                {
                    customers[i] = new Customer
                    {
                        CustomerName = "Customer" + rout.RouteID + "_" + i,
                        Address = "NY -The best city!",
                        Comment = "It doesnt matter what do you think about Mobile WEb Application!",
                        RouteID = rout.RouteID
                    };

                }
                context.Customers.AddOrUpdate(
                   c => c.CustomerName,
                customers
                );
            }

        }
    }
}
