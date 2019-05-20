namespace LibaryDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeBookTypes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "ISBN", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "ISBN", c => c.Int(nullable: false));
        }
    }
}
