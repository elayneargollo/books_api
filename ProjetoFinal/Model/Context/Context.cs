using Microsoft.EntityFrameworkCore;
using Solutis.Model;

namespace WebMySQL.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Book> Book {get; set; }
        public DbSet<User> User{get; set;}
      

    }
}
