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
        public static BookDTO convertBookToDTO(Book b)
        {
            BookDTO bookDTO = new BookDTO()
            {
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
            CustomerDTO customerDTO = new CustomerDTO()
            {
                firstname = c.Firstname,
                lastname = c.Lastname,
                address = c.Address,
                credit_number = c.CreditNumber
            };
            return customerDTO;
        }

        public static SupplierDTO convertSupplierToDTO(Supplier c)
        {
            SupplierDTO supplierDTO = new SupplierDTO()
            {
                company_name = c.CompanyName,
                address = c.Address
            };
            return supplierDTO;
        }
    }
}
