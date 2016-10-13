namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCartItemNumber2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Carts", new[] { "ItemNumber" });
            DropPrimaryKey("dbo.Carts");
            AddPrimaryKey("dbo.Carts", new[] { "UserId", "ItemNumber" });
            AddForeignKey("dbo.Carts", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "UserId", "dbo.AspNetUsers");
            DropPrimaryKey("dbo.Carts");
            AddPrimaryKey("dbo.Carts", "UserId");
            CreateIndex("dbo.Carts", "ItemNumber", unique: true);
            AddForeignKey("dbo.Carts", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
