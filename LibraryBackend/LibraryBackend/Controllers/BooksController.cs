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
            return new BookIdDTO()
            {
                book_id = newId,
                message = $"New book with ID {newId} has been added."
            };
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

        // GET: library/books/GetStoredBook
        [HttpGet("GetStoredBook/{bookId?}/{storeId?}")]
        public ActionResult<StoredBookDTO> GetStoredBook(int bookId, int storeId)
        {
            try
            {
                var storedBook = Context.db.StoredBooks.Single(x => x.StoreId == storeId && x.BookId == bookId);
                string lowStockWarning = storedBook.Amount <= 5
                    ? $"Book on low stock with an amount of {storedBook.Amount}"
                    : $"Book in stock. Amount: {storedBook.Amount}";
                return new StoredBookDTO
                {
                    store_id = storedBook.StoreId,
                    amount = storedBook.Amount,
                    book_id = storedBook.BookId,
                    message = lowStockWarning
                };
            }
            catch (Exception e)
            {
                Console.WriteLine($"#### book not in stock {bookId}: {e.Message} ####");
                return new StoredBookDTO
                {
                    store_id = storeId,
                    amount = -1,
                    book_id = bookId,
                    message = "Book not in stock"
                };
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
                    return new StoredBookDTO()
                    {
                        store_id = book.store_id,
                        amount = book.amount,
                        book_id = book.book_id,
                        message = $"New Book with ID {book.book_id} stored."
                    };
                }
                else //there are already books stored
                {
                    StoredBook b = Context.db.StoredBooks.Where(x => x.StoreId == book.store_id && x.BookId == book.book_id).First();
                    b.Amount = b.Amount + book.amount;

                    return new StoredBookDTO()
                    {
                        store_id = b.StoreId,
                        amount = b.Amount,
                        book_id = b.BookId,
                        message = $"Added amount for book with ID {b.BookId}. Current stock: {b.Amount}"
                    };
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"#### book couldn't be stored {book.book_id}: {e.Message} ####");
                return new StoredBookDTO()
                {
                    store_id = book.store_id,
                    amount = -1,
                    book_id = book.book_id
                };
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
                var currentTime = DateTime.Now;
                Lending dbLending = new Lending()
                {
                    CustomerId = lending.customer_id,
                    StoredBook = storedBook,
                    StartDate = currentTime,
                    ActualReturnDate = currentTime
                };
                Context.db.Lendings.Add(dbLending);
                Context.db.SaveChanges();
                return new StoredBookDTO()
                {
                    store_id = storedBook.StoreId,
                    amount = storedBook.Amount,
                    book_id = storedBook.BookId,
                    message = $"Book with ID {storedBook.BookId} has been lended. Remaining amount: {storedBook.Amount}"
                };
            }
            catch (Exception e)
            {
                Console.WriteLine($"#### book couldn't be lended {lending.book_id}: {e.Message} ####");
                return new StoredBookDTO()
                {
                    store_id = lending.store_id,
                    amount = -1,
                    book_id = lending.book_id,
                    message = $"Book with ID {lending.book_id} not in stock. Cannot be lended"
                };
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
                Lending dbLending = Context.db.Lendings
                    .Where(x => x.StoredBookBookId == lending.book_id
                    && x.StoredBookStoreId == lending.store_id
                    && x.CustomerId == lending.customer_id
                    && DateTime.Compare(x.StartDate, x.ActualReturnDate) == 0).FirstOrDefault();
                dbLending.ActualReturnDate = DateTime.Now;
                StoredBook storedBook = Context.db.StoredBooks.Single(x => x.StoreId == lending.store_id && x.BookId == lending.book_id);
                storedBook.Amount += lending.amount;
                Context.db.SaveChanges();
                return new StoredBookDTO()
                {
                    store_id = storedBook.StoreId,
                    amount = storedBook.Amount,
                    book_id = storedBook.BookId,
                    message = $"Restored lended book with ID {storedBook.BookId}. Amount: {storedBook.Amount}"
                };
            }
            catch (Exception e)
            {
                Console.WriteLine($"#### book couldn't be restored {lending.book_id}: {e.Message} ####");
                return new StoredBookDTO()
                {
                    store_id = lending.store_id,
                    amount = -1,
                    book_id = lending.book_id,
                    message = $"Book has not been lended."
                };
            }
        }

        // POST: library/books/BuyBook
        //public int customer_id;
        //public int book_id;
        //public int store_id;
        //public int amount;
        [HttpPost("BuyBook")]
        public ActionResult<StoredBookDTO> BuyBook(LendingDTO purchase)
        {
            Console.WriteLine($"*****   buying a new book: customer '{purchase.customer_id}', book '{purchase.book_id}'*{purchase.amount} --> storeId: {purchase.store_id}");
            try
            {
                int actualAmount = GetAmountOfCertainBook(purchase.book_id, purchase.store_id);
                if ((actualAmount - purchase.amount) < 0) throw new Exception();
                StoredBook storedBook = Context.db.StoredBooks
                    .Single(x => x.StoreId == purchase.store_id && x.BookId == purchase.book_id);
                storedBook.Amount -= purchase.amount;
                var price = Context.db.Books
                    .Single(x => x.BookId == storedBook.BookId)
                    .Price;
                Context.db.Sales
                    .Add(new Sale
                    {
                        CustomerId = purchase.customer_id,
                        BookId = (int)purchase.book_id,
                        StoreId = (int)purchase.store_id,
                        PaidPrice = price
                    });
                Context.db.SaveChanges();
                return new StoredBookDTO()
                {
                    store_id = storedBook.StoreId,
                    amount = storedBook.Amount,
                    book_id = storedBook.BookId,
                    message = $"Book with ID {storedBook.BookId} has been bought."
                };
            }
            catch (Exception e)
            {
                Console.WriteLine($"#### book couldn't be bought {purchase.book_id}: {e.Message} ####");
                return new StoredBookDTO()
                {
                    store_id = purchase.store_id,
                    amount = -1,
                    book_id = purchase.book_id,
                    message = $"No books in stock!"
                };
            }
        }

        [HttpGet("GetRelevantLendings")]
        public ActionResult<List<OpenLendingDTO>> GetRelevantLendings()
        {
            return Context.db.Lendings
                .ToList()
                .Where(x => DateTime.Compare(x.StartDate, x.ActualReturnDate) == 0)
                .Select(x => DTOConverter.convertLendingToDTO(x))
                .ToList();
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
