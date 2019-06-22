﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryBackend.Models
{
    public class BookDTO
    {
        //{"title":"Dating the Enemy","author":"Gownge","isbn":"002542194-8","publisher":"Ciotto","supplierId":93,"price":46}
        public int book_id;
        public string title;
        public string author;
        public string isbn;
        public string publisher;
        public double price;

    }
}
