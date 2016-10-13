namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCart2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carts", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.Carts", "DateTimeAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.Carts", "Book_Id", c => c.Int());
            CreateIndex("dbo.Carts", "Book_Id");
            AddForeignKey("dbo.Carts", "Book_Id", "dbo.Books", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "Book_Id", "dbo.Books");
            DropIndex("dbo.Carts", new[] { "Book_Id" });
            DropColumn("dbo.Carts", "Book_Id");
            DropColumn("dbo.Carts", "DateTimeAdded");
            DropColumn("dbo.Carts", "Quantity");
        }
    }
}
