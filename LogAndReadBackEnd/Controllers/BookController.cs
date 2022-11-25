namespace LogAndReadBackEnd.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.AspNetCore.Mvc;
    using Persistence;

    public class BookController : BaseController
    {
        private readonly IRepository<Book> _bookRepository;

        public BookController(IRepository<Book> bookRepository)
        {
            this._bookRepository = bookRepository;
        }

        [HttpGet("/book/{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            return this._bookRepository.Get(book => book.Id == id);
        }

        [HttpGet("/books")]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            return this._bookRepository.GetAll().ToList();
        }
    }
}
