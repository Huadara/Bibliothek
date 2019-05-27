using System;
using System.Collections.Generic;

namespace LibraryDb
{
    public partial class SuppliedBook
    {
        public int SupplierId { get; set; }
        public int BookId { get; set; }

        public Book Book { get; set; }
        public Supplier Supplier { get; set; }
    }
}
