using System;
using System.Collections.Generic;

namespace LibraryDb
{
    public partial class Store
    {
        public Store()
        {
            StoredBooks = new HashSet<StoredBook>();
        }

        public int StoreId { get; set; }
        public string Address { get; set; }

        public ICollection<StoredBook> StoredBooks { get; set; }
    }
}
