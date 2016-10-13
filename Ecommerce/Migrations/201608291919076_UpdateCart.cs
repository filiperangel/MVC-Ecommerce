namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCart : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "UserId", "dbo.AspNetUsers");
            DropPrimaryKey("dbo.Carts");
            AddPrimaryKey("dbo.Carts", "UserId");
            AddForeignKey("dbo.Carts", "UserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Carts", "Id");
            DropColumn("dbo.Carts", "CreationDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Carts", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Carts", "Id", c => c.Long(nullable: false, identity: true));
            DropForeignKey("dbo.Carts", "UserId", "dbo.AspNetUsers");
            DropPrimaryKey("dbo.Carts");
            AddPrimaryKey("dbo.Carts", new[] { "UserId", "Id" });
            AddForeignKey("dbo.Carts", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
