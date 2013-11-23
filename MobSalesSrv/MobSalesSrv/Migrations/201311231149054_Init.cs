namespace MobSalesSrv.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        RouteID = c.Int(nullable: false, identity: true),
                        RouteName = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.RouteID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Routes");
        }
    }
}
