using LibraryDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryBackend.Controllers
{
    public class Context
    {
        public static LibraryContext db = new LibraryContext();
    }
}
