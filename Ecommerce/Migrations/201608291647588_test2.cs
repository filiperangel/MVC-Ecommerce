namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Carts");
            AddColumn("dbo.Carts", "Id", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.Carts", new[] { "UserId", "Id" });
            DropColumn("dbo.Carts", "ItemNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "ItemNumber", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Carts");
            DropColumn("dbo.Carts", "Id");
            AddPrimaryKey("dbo.Carts", new[] { "UserId", "ItemNumber" });
        }
    }
}
