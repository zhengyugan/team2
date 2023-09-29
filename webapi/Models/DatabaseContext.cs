using Microsoft.EntityFrameworkCore;

namespace webapi.Models
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        //public DbSet<ToDo> ToDos { get; set; }
    }
}
