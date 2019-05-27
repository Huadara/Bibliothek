using System;
using System.Collections.Generic;

namespace LibraryDb
{
    public partial class Book
    {
        public Book()
        {
            Sales = new HashSet<Sale>();
            StoredBooks = new HashSet<StoredBook>();
            SuppliedBooks = new HashSet<SuppliedBook>();
        }

        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Isbn { get; set; }
        public string Publisher { get; set; }
        public float Price { get; set; }

        public ICollection<Sale> Sales { get; set; }
        public ICollection<StoredBook> StoredBooks { get; set; }
        public ICollection<SuppliedBook> SuppliedBooks { get; set; }
    }
}
