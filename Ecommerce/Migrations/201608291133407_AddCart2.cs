namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCart2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Carts", new[] { "User_Id" });
            AlterColumn("dbo.Carts", "User_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Carts", "User_Id");
            AddForeignKey("dbo.Carts", "User_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Carts", new[] { "User_Id" });
            AlterColumn("dbo.Carts", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Carts", "User_Id");
            AddForeignKey("dbo.Carts", "User_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
