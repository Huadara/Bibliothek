using LibraryBackend.Models;
using LibraryDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryBackend.DTOs
{
    public class DTOConverter
    {
        private static LibraryContext db = new LibraryContext();

        public static OpenLendingDTO convertLendingToDTO(Lending l)
        {
            string currentCust = db.Customers
                .Where(x => x.CustomerId == l.CustomerId)
                .Select(x => $"{x.Firstname} {x.Lastname}")
                .First();
            string currentBook = db.StoredBooks
                .Where(x => x.BookId == l.StoredBookBookId)
                .Select(x => x.Book.Title)
                .First();
            OpenLendingDTO lendingDTO = new OpenLendingDTO
            {
                customer_name = currentCust,
                book_title = currentBook,
                start_date = l.StartDate.ToLongDateString()
            };
            return lendingDTO;
        }

        public static BookDTO convertBookToDTO(Book b)
        {
            BookDTO bookDTO = new BookDTO
            {
                book_id = b.BookId,
                title = b.Title,
                author = b.Author,
                isbn = b.Isbn,
                publisher = b.Publisher,
                price = b.Price
            };
            return bookDTO;
        }

        public static CustomerDTO convertCustomerToDTO(Customer c)
        {
            CustomerDTO customerDTO = new CustomerDTO
            {
                customer_id = c.CustomerId,
                firstname = c.Firstname,
                lastname = c.Lastname,
                address = c.Address,
                credit_number = c.CreditNumber
            };
            return customerDTO;
        }

        public static SupplierDTO convertSupplierToDTO(Supplier c)
        {
            SupplierDTO supplierDTO = new SupplierDTO
            {
                supplier_id = c.SupplierId,
                company_name = c.CompanyName,
                address = c.Address
            };
            return supplierDTO;
        }
    }
}
