using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LibraryDb
{
    public partial class LibraryContext : DbContext
    {
        public readonly static string DB_FOLDER_NAME = "LibraryDb";
        public LibraryContext()
        {
        }

        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Lending> Lendings { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<StoredBook> StoredBooks { get; set; }
        public virtual DbSet<SuppliedBook> SuppliedBooks { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string path = Environment.CurrentDirectory;
            path = $"{path.Substring(0, path.LastIndexOf('\\') + 1)}{DB_FOLDER_NAME}";
            Console.WriteLine($"            *** path = {path}");
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer($"data source=(LocalDB)\\mssqllocaldb;attachdbfilename={path}\\LibraryDB.mdf;integrated security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.Property(e => e.Isbn).HasColumnName("ISBN");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            });

            modelBuilder.Entity<Lending>(entity =>
            {
                entity.HasIndex(e => e.CustomerId)
                    .HasName("IX_CustomerID");

                entity.HasIndex(e => new { e.StoredBookStoreId, e.StoredBookBookId })
                    .HasName("IX_StoredBook_StoreID_StoredBook_BookID");

                entity.Property(e => e.LendingId).HasColumnName("LendingID");

                entity.Property(e => e.ActualReturnDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.StoredBookBookId).HasColumnName("StoredBook_BookID");

                entity.Property(e => e.StoredBookId).HasColumnName("StoredBookID");

                entity.Property(e => e.StoredBookStoreId).HasColumnName("StoredBook_StoreID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Lendings)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_dbo.Lendings_dbo.Customers_CustomerID");

                entity.HasOne(d => d.StoredBook)
                    .WithMany(p => p.Lendings)
                    .HasForeignKey(d => new { d.StoredBookStoreId, d.StoredBookBookId })
                    .HasConstraintName("FK_dbo.Lendings_dbo.StoredBooks_StoredBook_StoreID_StoredBook_BookID");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey });

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasIndex(e => e.BookId)
                    .HasName("IX_BookID");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("IX_CustomerID");

                entity.Property(e => e.SaleId).HasColumnName("SaleID");

                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_dbo.Sales_dbo.Books_BookID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_dbo.Sales_dbo.Customers_CustomerID");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.Property(e => e.StoreId).HasColumnName("StoreID");
            });

            modelBuilder.Entity<StoredBook>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.BookId });

                entity.HasIndex(e => e.BookId)
                    .HasName("IX_BookID");

                entity.HasIndex(e => e.StoreId)
                    .HasName("IX_StoreID");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.StoredBooks)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_dbo.StoredBooks_dbo.Books_BookID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoredBooks)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_dbo.StoredBooks_dbo.Stores_StoreID");
            });

            modelBuilder.Entity<SuppliedBook>(entity =>
            {
                entity.HasKey(e => new { e.SupplierId, e.BookId });

                entity.HasIndex(e => e.BookId)
                    .HasName("IX_BookID");

                entity.HasIndex(e => e.SupplierId)
                    .HasName("IX_SupplierID");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.BookId).HasColumnName("BookID");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.SuppliedBooks)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK_dbo.SuppliedBooks_dbo.Books_BookID");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.SuppliedBooks)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_dbo.SuppliedBooks_dbo.Suppliers_SupplierID");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");
            });
        }
    }
}
