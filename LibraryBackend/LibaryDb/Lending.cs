using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryDb
{
    public class Lending
    {
        public int LendingID { get; set; }
        public int CustomerID { get; set; }
        public int StoredBookID { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime ActualReturnDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual StoredBook StoredBook { get; set; }
    }
}
