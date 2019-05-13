using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryDb
{
    public class Sale
    {
        public int SaleID { get; set; }
        
        public int CustomerID { get; set; }

        public int BookID { get; set; }

        public int StoreID { get; set; }

        public int PaidPrice { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Book Book { get; set; }
    }
}
