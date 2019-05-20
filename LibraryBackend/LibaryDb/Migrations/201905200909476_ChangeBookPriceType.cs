namespace LibaryDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeBookPriceType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Price", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Price", c => c.Int(nullable: false));
        }
    }
}
