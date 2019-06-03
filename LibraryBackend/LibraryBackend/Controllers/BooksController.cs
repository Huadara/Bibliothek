using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryBackend.DTOs;
using LibraryBackend.Models;
using LibraryDb;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LibraryBackend.Controllers
{
    [EnableCors("AllowMyOrigin")]
    [Route("library/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        // GET: library/books
        [HttpGet]
        public ActionResult<List<BookDTO>> Get()
        {
            List<Book> dbBooks = Context.db.Books.ToList();
            Console.WriteLine($"*** returning {dbBooks.Count()} books ***");
            return dbBooks.Select(x => DTOConverter.convertBookToDTO(x)).ToList();
        }

        // GET: library/books/5
        [HttpGet("{id}")]
        public ActionResult<BookDTO> Get(int id)
        {
            return DTOConverter.convertBookToDTO(Context.db.Books.Where(x => x.BookId == id).First());
        }

        // POST: library/books
        [HttpPost]
        public ActionResult<BookIdDTO> Post([FromBody] BookDTO newBook)
        {
            Book b = new Book()
            {
                Title = newBook.title,
                Author = newBook.author,
                Isbn = newBook.isbn,
                Publisher = newBook.publisher,
                Price = (float)newBook.price
            };
            Context.db.Books.Add(b);
            Context.db.SaveChanges();
            int newId = b.BookId;
            Console.WriteLine($"                ++++++++++++++ {newId} ++++++++++++++");
            return new BookIdDTO() { book_id = newId };
        }

        // PUT: library/books/5
        [HttpPut("{id}")]
        public ActionResult<BookIdDTO> Put(int id, [FromBody] BookDTO newBook)
        {
            try
            {
                Book b = Context.db.Books.Where(x => x.BookId == id).First();
                b.Title = newBook.title;
                b.Author = newBook.author;
                b.Isbn = newBook.isbn;
                b.Publisher = newBook.publisher;
                b.Price = (float)newBook.price;
                Context.db.SaveChanges();
                return new BookIdDTO() { book_id = b.BookId };
            }
            catch (Exception e)
            {
                //id wurde nicht in der db gefunden.
                return new BookIdDTO() { book_id = -1 };
            }
        }

        // DELETE: library/books/5
        [HttpDelete("{id}")]
        public ActionResult<BookIdDTO> Delete(int id)
        {
            try
            {
                Book b = Context.db.Books.Where(x => x.BookId == id).First();
                if (b == null) throw new Exception();
                Context.db.Books.Remove(b);
                Context.db.SaveChanges();
                return new BookIdDTO() { book_id = b.BookId };
            }
            catch (Exception e)
            {
                //id wurde nicht in der db gefunden.
                return new BookIdDTO() { book_id = -1 };
            }
        }
    }
}
