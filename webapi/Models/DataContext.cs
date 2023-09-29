using Microsoft.EntityFrameworkCore;

namespace webapi.Models
{
	public class DataContext:DbContext
	{
        public DataContext(DbContextOptions<DataContext> options) :base(options)
        {
            
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<ProductVariation> ProductVariation { get; set; }
        public DbSet<Cart> CartItem { get; set; }


    }
}
