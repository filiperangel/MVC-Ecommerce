namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Carts");
            AddColumn("dbo.Carts", "ItemNumber", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Carts", new[] { "UserId", "ItemNumber" });
            DropColumn("dbo.Carts", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "Id", c => c.Long(nullable: false, identity: true));
            DropPrimaryKey("dbo.Carts");
            DropColumn("dbo.Carts", "ItemNumber");
            AddPrimaryKey("dbo.Carts", "Id");
        }
    }
}
