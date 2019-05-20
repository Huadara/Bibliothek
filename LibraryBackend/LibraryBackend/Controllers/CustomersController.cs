using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryBackend.DTOs;
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
        public string Get()
        {
            return "{}";
        }

        // GET: library/customers/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<CustomerDTO> Get(int id)
        {
            return new CustomerDTO() { firstname = "Rainer" };
        }

        // POST: library/customers
        [HttpPost]
        public ActionResult<CustomerIdDTO> Post([FromBody] CustomerDTO value)
        {
            Console.WriteLine("+++++++++++++++++++++++++" + value);
            return new CustomerIdDTO() { customer_id = 5 };
        }

        // PUT: library/customers/5
        [HttpPut("{id}")]
        public ActionResult<CustomerIdDTO> Put(int id, [FromBody] CustomerDTO value)
        {
            //TODO: ändern in DB
            return new CustomerIdDTO() { customer_id = 5 };
        }

        // DELETE: library/customers/5
        [HttpDelete("{id}")]
        public ActionResult<CustomerIdDTO> Delete(int id)
        {
            return new CustomerIdDTO() { customer_id = 5 };
        }
    }
}