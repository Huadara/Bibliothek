using System;
using System.Collections.Generic;

namespace LibraryDb
{
    public partial class Customer
    {
        public Customer()
        {
            Lendings = new HashSet<Lending>();
            Sales = new HashSet<Sale>();
        }

        public int CustomerId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string CreditNumber { get; set; }

        public ICollection<Lending> Lendings { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}
