namespace LibaryDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeCreditNumberType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "CreditNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "CreditNumber", c => c.Int(nullable: false));
        }
    }
}
