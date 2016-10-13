namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookCoverImagePath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "CoverImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "CoverImagePath");
        }
    }
}
