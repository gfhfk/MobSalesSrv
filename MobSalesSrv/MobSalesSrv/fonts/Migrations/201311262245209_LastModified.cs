namespace MobSalesSrv.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LastModified : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Routes", "LastModified", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Routes", "LastModified");
        }
    }
}
