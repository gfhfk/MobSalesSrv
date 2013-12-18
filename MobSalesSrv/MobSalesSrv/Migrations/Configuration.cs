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
            
            // routes
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
            context.SaveChanges();
            
            
            context.ProductTypes.AddOrUpdate(
              p => p.ProductTypeName,
                new ProductType { ProductTypeName = "Proudct type 1" },
                new ProductType { ProductTypeName = "Proudct type 2" },
                new ProductType { ProductTypeName = "Proudct type 3" },
                new ProductType { ProductTypeName = "Proudct type 4" },
                new ProductType { ProductTypeName = "Proudct type 5" }
            );
            context.SaveChanges();

            foreach (var productType in context.ProductTypes)
            {
                var products = new Product[20 + 2 * productType.ProductTypeID % 10];
                for (int i = 0; i < products.Length; i++)
                {
                    context.Products.AddOrUpdate(
                         p => p.ProductName,
                         new Product
                         {
                             ProductName = "Product name " + productType.ProductTypeID + "_" + i,
                             ProductTypeID = productType.ProductTypeID,
                             Price = 9.99M,
                         });

                }
            }
            context.SaveChanges();
            
            foreach (var customer in context.Customers)
            {
                for (int i = 0; i < customer.CustomerID % 10; i++)
                {

                    context.Orders.AddOrUpdate(
                        o => new { o.CustomerID, o.Date },
                        new Order
                        {
                            Date = DateTime.Today.AddDays(-i),
                            CustomerID = customer.CustomerID,
                            DeliveryDate = DateTime.Today.AddDays(-i + 1),
                            Comment = "Some very important information for good selling...",
                            TotalSum = 10

                        });

                }
            }
            context.SaveChanges();
            var tproducts = context.Products.ToArray();
            var rnd = new Random();

            foreach (var order in context.Orders)
            {
                var orderDetails = new OrderDetails[rnd.Next(2, 7)];
                for (int i = 0; i < orderDetails.Length; i++)
                {

                    context.OrderDetails.AddOrUpdate(od => new { od.OrderID, od.ProductID },

                        new OrderDetails
                        {
                            OrderID = order.OrderID,
                            ProductID = tproducts[rnd.Next(tproducts.Length)].ProductID,
                            Quantity = rnd.Next(100)
                        });

                }
            }
            context.SaveChanges();

        }
    }
}
