using System;
using System.Collections.Generic;

namespace LibraryDb
{
    public partial class StoredBook
    {
        public StoredBook()
        {
            Lendings = new HashSet<Lending>();
        }

        public int StoreId { get; set; }
        public int BookId { get; set; }
        public int Amount { get; set; }

        public Book Book { get; set; }
        public Store Store { get; set; }
        public ICollection<Lending> Lendings { get; set; }
    }
}
