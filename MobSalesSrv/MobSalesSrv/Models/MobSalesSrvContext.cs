using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace MobSalesSrv.Models
{
    public class MobSalesSrvContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public MobSalesSrvContext() : base("name=MobSalesSrvContext")
        {
        }
         
        public System.Data.Entity.DbSet<MobSalesSrv.Models.Route> Routes { get; set; }
        public System.Data.Entity.DbSet<MobSalesSrv.Models.Customer> Customers { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }


        public override int SaveChanges()
        {
            DateTime now = DateTime.UtcNow;
            foreach (ObjectStateEntry entry in (this as IObjectContextAdapter).ObjectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified))
            {
                if (!entry.IsRelationship)
                {
                    IHasLastModified lastModified = entry.Entity as IHasLastModified;
                    if (lastModified != null)
                        lastModified.LastModified = now;
                }
            }

            return base.SaveChanges();
        }
    
    }
}
