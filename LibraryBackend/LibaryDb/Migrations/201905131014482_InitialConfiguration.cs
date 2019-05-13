namespace LibaryDb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialConfiguration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Author = c.String(),
                        ISBN = c.Int(nullable: false),
                        Publisher = c.String(),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.Int(nullable: false, identity: true),
                        Firstname = c.String(),
                        Lastname = c.String(),
                        Address = c.String(),
                        CreditNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.Lendings",
                c => new
                    {
                        LendingID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        StoredBookID = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        ActualReturnDate = c.DateTime(nullable: false),
                        StoredBook_StoreID = c.Int(),
                        StoredBook_BookID = c.Int(),
                    })
                .PrimaryKey(t => t.LendingID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.StoredBooks", t => new { t.StoredBook_StoreID, t.StoredBook_BookID })
                .Index(t => t.CustomerID)
                .Index(t => new { t.StoredBook_StoreID, t.StoredBook_BookID });
            
            CreateTable(
                "dbo.StoredBooks",
                c => new
                    {
                        StoreID = c.Int(nullable: false),
                        BookID = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StoreID, t.BookID })
                .ForeignKey("dbo.Books", t => t.BookID, cascadeDelete: true)
                .ForeignKey("dbo.Stores", t => t.StoreID, cascadeDelete: true)
                .Index(t => t.StoreID)
                .Index(t => t.BookID);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        StoreID = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.StoreID);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        SaleID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        BookID = c.Int(nullable: false),
                        StoreID = c.Int(nullable: false),
                        PaidPrice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SaleID)
                .ForeignKey("dbo.Books", t => t.BookID, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID)
                .Index(t => t.BookID);
            
            CreateTable(
                "dbo.SuppliedBooks",
                c => new
                    {
                        SupplierID = c.Int(nullable: false),
                        BookID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SupplierID, t.BookID })
                .ForeignKey("dbo.Books", t => t.BookID, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.SupplierID, cascadeDelete: true)
                .Index(t => t.SupplierID)
                .Index(t => t.BookID);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        SupplierID = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.SupplierID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SuppliedBooks", "SupplierID", "dbo.Suppliers");
            DropForeignKey("dbo.SuppliedBooks", "BookID", "dbo.Books");
            DropForeignKey("dbo.Sales", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.Sales", "BookID", "dbo.Books");
            DropForeignKey("dbo.Lendings", new[] { "StoredBook_StoreID", "StoredBook_BookID" }, "dbo.StoredBooks");
            DropForeignKey("dbo.StoredBooks", "StoreID", "dbo.Stores");
            DropForeignKey("dbo.StoredBooks", "BookID", "dbo.Books");
            DropForeignKey("dbo.Lendings", "CustomerID", "dbo.Customers");
            DropIndex("dbo.SuppliedBooks", new[] { "BookID" });
            DropIndex("dbo.SuppliedBooks", new[] { "SupplierID" });
            DropIndex("dbo.Sales", new[] { "BookID" });
            DropIndex("dbo.Sales", new[] { "CustomerID" });
            DropIndex("dbo.StoredBooks", new[] { "BookID" });
            DropIndex("dbo.StoredBooks", new[] { "StoreID" });
            DropIndex("dbo.Lendings", new[] { "StoredBook_StoreID", "StoredBook_BookID" });
            DropIndex("dbo.Lendings", new[] { "CustomerID" });
            DropTable("dbo.Suppliers");
            DropTable("dbo.SuppliedBooks");
            DropTable("dbo.Sales");
            DropTable("dbo.Stores");
            DropTable("dbo.StoredBooks");
            DropTable("dbo.Lendings");
            DropTable("dbo.Customers");
            DropTable("dbo.Books");
        }
    }
}
