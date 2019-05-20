﻿using System;
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
        public ActionResult<BookDTO> Get(int id)
        {
            return new BookDTO() { title = "ABC" };
        }

        // POST: library/books
        [HttpPost]
        public ActionResult<BookIdDTO> Post([FromBody] BookDTO value)
        {
            Console.WriteLine("+++++++++++++++++++++++++" + value);
            return new BookIdDTO() { book_id = 5 };
        }

        // PUT: library/books/5
        [HttpPut("{id}")]
        public ActionResult<BookIdDTO> Put(int id, [FromBody] BookDTO value)
        {
            //TODO: ändern in DB
            return new BookIdDTO() { book_id = 5 };
        }

        // DELETE: library/books/5
        [HttpDelete("{id}")]
        public ActionResult<BookIdDTO> Delete(int id)
        {
            return new BookIdDTO() { book_id = 5 };
        }
    }
}
