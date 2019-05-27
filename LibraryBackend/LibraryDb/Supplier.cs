using System;
using System.Collections.Generic;

namespace LibraryDb
{
    public partial class Supplier
    {
        public Supplier()
        {
            SuppliedBooks = new HashSet<SuppliedBook>();
        }

        public int SupplierId { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }

        public ICollection<SuppliedBook> SuppliedBooks { get; set; }
    }
}
