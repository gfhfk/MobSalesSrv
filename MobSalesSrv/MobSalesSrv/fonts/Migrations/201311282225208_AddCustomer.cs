namespace MobSalesSrv.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCustomer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false, maxLength: 50),
                        Address = c.String(maxLength: 150),
                        Comment = c.String(),
                        RouteID = c.Int(nullable: false),
                        LastModified = c.DateTime(),
                    })
                .PrimaryKey(t => t.CustomerID)
                .ForeignKey("dbo.Routes", t => t.RouteID, cascadeDelete: true)
                .Index(t => t.RouteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "RouteID", "dbo.Routes");
            DropIndex("dbo.Customers", new[] { "RouteID" });
            DropTable("dbo.Customers");
        }
    }
}
