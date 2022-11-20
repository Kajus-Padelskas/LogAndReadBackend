using LogAndReadBackEnd.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogAndReadBackEnd.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<WebUser> Users { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
