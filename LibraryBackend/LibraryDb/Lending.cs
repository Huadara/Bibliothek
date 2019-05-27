using System;
using System.Collections.Generic;

namespace LibraryDb
{
    public partial class Lending
    {
        public int LendingId { get; set; }
        public int CustomerId { get; set; }
        public int StoredBookId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ActualReturnDate { get; set; }
        public int? StoredBookStoreId { get; set; }
        public int? StoredBookBookId { get; set; }

        public Customer Customer { get; set; }
        public StoredBook StoredBook { get; set; }
    }
}
