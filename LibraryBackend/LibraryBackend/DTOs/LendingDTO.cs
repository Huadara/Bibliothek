using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryBackend.DTOs
{
    public class LendingDTO
    {
        public int customer_id;
        public int book_id;
        public int store_id;
        public int amount;
    }
}
