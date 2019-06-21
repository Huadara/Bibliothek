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
            Console.WriteLine($"title: {newBook.title}, price: ${newBook.price}");
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
                Console.WriteLine($"#### there was no book with id {id}: {e.Message} ####");
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
                Console.WriteLine($"#### there was no book with id {id}: {e.Message} ####");
                return new BookIdDTO() { book_id = -1 };
            }
        }

        // POST: library/books/StoreBook
        //public int book_id;
        //public int store_id;
        //public int amount;
        [HttpPost("StoreBook")]
        public ActionResult<StoredBookDTO> StoreBook(StoredBookDTO book)
        {
            Console.WriteLine($"*****   storing a new book: {book.book_id}*{book.amount} --> storeId: {book.store_id}");
            try
            {
                int storedBookCount = Context.db.StoredBooks.Where(x => x.StoreId == book.store_id && x.BookId == book.book_id).Count();
                if (storedBookCount == 0) //book is new
                {
                    Context.db.StoredBooks.Add(new StoredBook()
                    {
                        StoreId = book.store_id,
                        BookId = book.book_id,
                        Amount = book.amount
                    });
                    return new StoredBookDTO() { store_id = book.store_id, amount = book.amount, book_id = book.book_id };
                }
                else //there are already books stored
                {
                    StoredBook b = Context.db.StoredBooks.Where(x => x.StoreId == book.store_id && x.BookId == book.book_id).First();
                    b.Amount = b.Amount + book.amount;

                    return new StoredBookDTO() { store_id = b.StoreId, amount = b.Amount, book_id = b.BookId };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"#### book couldn't be stored {book.book_id}: {e.Message} ####");
                return new StoredBookDTO() { store_id = book.store_id, amount = -1, book_id = book.book_id };
            }
            finally
            {
                Context.db.SaveChanges();
            }
        }

        // POST: library/books/LendBook
        //public int customer_id;
        //public int book_id;
        //public int store_id;
        //public int amount;
        [HttpPost("LendBook")]
        public ActionResult<StoredBookDTO> LendBook(LendingDTO lending)
        {
            Console.WriteLine($"*****   lending a new book: customer '{lending.customer_id}', book '{lending.book_id}'*{lending.amount} --> storeId: {lending.store_id}");
            try
            {
                int actualAmount = GetAmountOfCertainBook(lending.book_id, lending.store_id);
                if ((actualAmount - lending.amount) < 0) throw new Exception();
                StoredBook storedBook = Context.db.StoredBooks.Where(x => x.StoreId == lending.store_id && x.BookId == lending.book_id).First();
                storedBook.Amount -= lending.amount;
                Lending dbLending = new Lending()
                {
                    CustomerId = lending.customer_id,
                    StoredBook = storedBook,
                    StartDate = DateTime.Now,
                    ActualReturnDate = DateTime.Now
                };
                Context.db.Lendings.Add(dbLending);
                Context.db.SaveChanges();
                return new StoredBookDTO() { store_id = storedBook.StoreId, amount = storedBook.Amount, book_id = storedBook.BookId };
            }
            catch (Exception e)
            {
                Console.WriteLine($"#### book couldn't be lended {lending.book_id}: {e.Message} ####");
                return new StoredBookDTO() { store_id = lending.store_id, amount = -1, book_id = lending.book_id };
            }
        }

        // POST: library/books/RestoreBook
        //public int customer_id;
        //public int book_id;
        //public int store_id;
        //public int amount;
        [HttpPost("RestoreBook")]
        public ActionResult<StoredBookDTO> RestoreBook(LendingDTO lending)
        {
            Console.WriteLine($"*****   giving a new book back: customer '{lending.customer_id}', book '{lending.book_id}'*{lending.amount} --> storeId: {lending.store_id}");
            try
            {
                StoredBook storedBook = Context.db.StoredBooks.Where(x => x.StoreId == lending.store_id && x.BookId == lending.book_id).First();
                storedBook.Amount += lending.amount;
                Lending dbLending = Context.db.Lendings.Where(x => x.StoredBookBookId == lending.book_id && x.StoredBookStoreId == lending.store_id).FirstOrDefault();
                dbLending.ActualReturnDate = DateTime.Now;
                //TODO: Wenn das Buch nach erst nach dem Startdatum + 3 zurückgegeben wurde, wird das Buch verkauft.
                Context.db.SaveChanges();
                return new StoredBookDTO() { store_id = storedBook.StoreId, amount = storedBook.Amount, book_id = storedBook.BookId };
            }
            catch (Exception e)
            {
                Console.WriteLine($"#### book couldn't be restored {lending.book_id}: {e.Message} ####");
                return new StoredBookDTO() { store_id = lending.store_id, amount = -1, book_id = lending.book_id };
            }
        }

        // POST: library/books/BuyBook
        [HttpPost("BuyBook")]
        public ActionResult<StoredBookDTO> BuyBook(LendingDTO lending)
        {
            Console.WriteLine($"*****   buying a new book: customer '{lending.customer_id}', book '{lending.book_id}'*{lending.amount} --> storeId: {lending.store_id}");
            //TODO
            return null;
        }

        #region Helpers
        private int GetAmountOfCertainBook(int bookId, int storeId)
        {
            int amount = Context.db.StoredBooks.Where(x => x.BookId == bookId && x.StoreId == storeId).Count();
            if (amount == 0) return -1;
            else return Context.db.StoredBooks.Where(x => x.BookId == bookId && x.StoreId == storeId).First().Amount;
        }
        #endregion
    }
}
