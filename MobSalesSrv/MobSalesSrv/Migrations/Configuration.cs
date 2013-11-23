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
            AutomaticMigrationsEnabled = false;
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

        }
    }
}
