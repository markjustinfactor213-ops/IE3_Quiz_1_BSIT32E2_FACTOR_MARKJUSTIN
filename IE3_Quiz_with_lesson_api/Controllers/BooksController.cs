
using Microsoft.AspNetCore.Mvc;
using IE3_Quiz_with_lesson_api.Models;
using System.Collections.Generic;
using System.Linq;

namespace IE3_Quiz_with_lesson_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static readonly List<Book> _books = new List<Book>();

        // GET: api/books
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAll()
        {
            return Ok(_books);
        }

        // GET: api/books/{id}
        [HttpGet("{id:int}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound(new
                {
                    message = $"Book with ID {id} was not found."
                });
            }

            return Ok(book);
        }

        // POST: api/books
        [HttpPost]
        public IActionResult Create(Book newBook)
        {
            newBook.Id = _books.Any()
                ? _books.Max(b => b.Id) + 1
                : 1;

            _books.Add(newBook);

            return CreatedAtRoute(
                "GetById",
                new { id = newBook.Id },
                newBook
            );
        }

        // PUT: api/books/{id}
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, Book updatedBook)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound(new
                {
                    message = $"Book with ID {id} was not found."
                });
            }

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.IsAvailable = updatedBook.IsAvailable;
            book.YearPublished = updatedBook.YearPublished;

            return NoContent();
        }

        // DELETE: api/books/{id}
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound(new
                {
                    message = $"Book with ID {id} was not found."
                });
            }

            _books.Remove(book);

            return NoContent();
        }
    }
}