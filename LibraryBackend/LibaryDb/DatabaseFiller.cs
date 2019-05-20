using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibaryDb
{
    public class DatabaseFiller
    {
        private LibaryContext db { get; set; }
        public void FillDatabase()
        {
            db = new LibaryContext();
            //FillCustomerTable();
            //FillSalesTable();
            FillBooksTable();
        }

        private void FillBooksTable()
        {
            string[] customers = File.ReadAllLines(@"C:\Schule\4b\DBI\LibaryProject\Bibliothek\LibraryBackend\LibaryDb\Books.txt");
            customers.ToList().Skip(1).Select(x => x.Split(';')).ToList().ForEach(x =>
            {
                Console.WriteLine(x[5]);
                db.Books.Add(new Book
                {
                    BookID = int.Parse(x[0]),
                    Title = x[1],
                    Author = x[2],
                    ISBN = x[3],
                    Publisher = x[4],
                    Price = float.Parse(x[5].Replace('.',','))
                });
                

            });
            db.SaveChanges();
        }

        private void FillSalesTable()
        {
            throw new NotImplementedException();
        }

        private void FillCustomerTable()
        {
            string[] customers = File.ReadAllLines(@"C:\Schule\4b\DBI\LibaryProject\Bibliothek\LibraryBackend\LibaryDb\Customers.csv");
            customers.ToList().Skip(1).Select(x => x.Split(',')).ToList().ForEach(x => db.Customers.Add(new Customer
            {
                CustomerID = int.Parse(x[0]),
                Firstname = x[1],
                Lastname = x[2],
                Address = x[3],
                CreditNumber = x[4]
            }));
            db.SaveChanges();
        }
    }
}
