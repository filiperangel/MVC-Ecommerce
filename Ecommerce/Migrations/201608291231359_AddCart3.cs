namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCart3 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Carts", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.Carts", name: "IX_User_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Carts", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Carts", name: "UserId", newName: "User_Id");
        }
    }
}
