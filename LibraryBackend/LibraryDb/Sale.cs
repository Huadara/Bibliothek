using System;
using System.Collections.Generic;

namespace LibraryDb
{
    public partial class Sale
    {
        public int SaleId { get; set; }
        public int CustomerId { get; set; }
        public int BookId { get; set; }
        public int StoreId { get; set; }
        public int PaidPrice { get; set; }

        public Book Book { get; set; }
        public Customer Customer { get; set; }
    }
}
