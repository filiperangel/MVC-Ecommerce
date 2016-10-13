namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCartItemNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "ItemNumber", c => c.Int(nullable: false));
            CreateIndex("dbo.Carts", "ItemNumber", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Carts", new[] { "ItemNumber" });
            DropColumn("dbo.Carts", "ItemNumber");
        }
    }
}
