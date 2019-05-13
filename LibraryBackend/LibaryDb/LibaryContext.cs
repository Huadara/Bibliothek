using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryDb
{
    public class LibaryContext : DbContext
    {
        public LibaryContext()
            : base("name=LibaryContext")
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Lending> Lendings { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<StoredBook> StoredBooks { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<SuppliedBook> SuppliedBooks { get; set; }
    }
}
