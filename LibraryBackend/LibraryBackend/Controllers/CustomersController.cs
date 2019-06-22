using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryBackend.DTOs;
using LibraryDb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBackend.Controllers
{
    [Route("library/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // GET: library/customers
        [HttpGet]
        public ActionResult<List<CustomerDTO>> Get()
        {
            List<Customer> dbCustomers = Context.db.Customers.ToList();
            Console.WriteLine($"*** returning {dbCustomers.Count()} customers ***");
            return dbCustomers.Select(x => DTOConverter.convertCustomerToDTO(x)).ToList();
        }

        // GET: library/customers/5
        [HttpGet("{id}")]
        public ActionResult<CustomerDTO> Get(int id)
        {
            return DTOConverter.convertCustomerToDTO(Context.db.Customers.Where(x => x.CustomerId == id).First());
        }

        // POST: library/customers
        //public string firstname;
        //public string lastname;
        //public string address;
        //public string credit_number;
        [HttpPost]
        public ActionResult<CustomerIdDTO> Post([FromBody] CustomerDTO newCustomer)
        {
            Customer c = new Customer()
            {
                Firstname = newCustomer.firstname,
                Lastname = newCustomer.lastname,
                Address = newCustomer.address,
                CreditNumber = newCustomer.credit_number
            };
            Context.db.Customers.Add(c);
            Context.db.SaveChanges();
            int newId = c.CustomerId;
            Console.WriteLine($"                ++++++++++++++ {newId} ++++++++++++++");
            return new CustomerIdDTO() { customer_id = newId };
        }

        // PUT: library/customers/5
        [HttpPut("{id}")]
        public ActionResult<CustomerIdDTO> Put(int id, [FromBody] CustomerDTO newCustomer)
        {
            try
            {
                Customer c = Context.db.Customers.Where(x => x.CustomerId == id).First();
                c.Firstname = newCustomer.firstname;
                c.Lastname = newCustomer.lastname;
                c.Address = newCustomer.address;
                c.CreditNumber = newCustomer.credit_number;
                Context.db.SaveChanges();
                return new CustomerIdDTO() { customer_id = c.CustomerId };
            }
            catch (Exception e)
            {
                Console.WriteLine($"#### there was no customer with id {id}: {e.Message} ####");
                return new CustomerIdDTO() { customer_id = -1 };
            }
        }

        // DELETE: library/customers/5
        [HttpDelete("{id}")]
        public ActionResult<CustomerIdDTO> Delete(int id)
        {
            try
            {
                Customer c = Context.db.Customers.Where(x => x.CustomerId == id).First();
                if (c == null) throw new Exception();
                Context.db.Customers.Remove(c);
                Context.db.SaveChanges();
                return new CustomerIdDTO() { customer_id = c.CustomerId };
            }
            catch (Exception e)
            {
                Console.WriteLine($"#### there was no customer with id {id}: {e.Message} ####");
                return new CustomerIdDTO() { customer_id = -1 };
            }
        }
    }
}