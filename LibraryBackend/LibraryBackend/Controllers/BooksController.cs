using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryBackend.DTOs;
using LibraryBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LibraryBackend.Controllers
{
    [Route("library/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // GET: library/books
        [HttpGet]
        public string Get()
        {
            return System.IO.File.ReadAllText(@".\Test Data\get_books.json");
        }

        // GET: library/books/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return JsonConvert.SerializeObject(new BookDTO() { title = "ABC" });
        }

        // POST: library/books
        [HttpPost]
        public void Post([FromBody] string value)
        {
            Console.WriteLine("+++++++++++++++++++++++++" + value);
            //return JsonConvert.SerializeObject(new BookIdDTO() { book_id = 5 });
        }

        // PUT: library/books/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE: library/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
