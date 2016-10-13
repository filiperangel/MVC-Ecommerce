namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderOrderItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Long(nullable: false),
                        ItemNumber = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Total = c.Double(nullable: false),
                        Book_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.Id, t.ItemNumber })
                .ForeignKey("dbo.Books", t => t.Book_Id)
                .ForeignKey("dbo.Orders", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.Book_Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        OrderStatus = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateFinished = c.DateTime(),
                        Total = c.Double(nullable: false),
                        TrackingNumber = c.String(),
                        OrderLog = c.String(),
                        BillingAddress = c.String(nullable: false),
                        ShippingAddress = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderItems", "Id", "dbo.Orders");
            DropForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.OrderItems", "Book_Id", "dbo.Books");
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.OrderItems", new[] { "Book_Id" });
            DropIndex("dbo.OrderItems", new[] { "Id" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
        }
    }
}
