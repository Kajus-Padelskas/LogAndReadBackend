using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogAndReadBackEnd.Data;
using LogAndReadBackEnd.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LogAndReadBackEnd.Controllers
{
    public class BookController : BaseController
    {
        private readonly DataContext _context;

        public BookController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("/book/{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            return await _context.Books.SingleOrDefaultAsync(book => book.Id == id);
        }

        [HttpGet("/books")]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }
    }
}
