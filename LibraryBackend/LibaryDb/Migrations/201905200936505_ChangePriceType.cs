namespace LibaryDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePriceType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Price", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Price", c => c.Double(nullable: false));
        }
    }
}
