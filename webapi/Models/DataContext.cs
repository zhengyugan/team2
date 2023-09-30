using Microsoft.EntityFrameworkCore;

namespace webapi.Models
{
	public class DataContext:DbContext
	{
        public DataContext(DbContextOptions<DataContext> options) :base(options)
        {
            
        }

        public DbSet<Products> products { get; set; }
        public DbSet<ProductCategories> product_categories { get; set; }
        public DbSet<ProductVariant> product_variants { get; set; }
        public DbSet<Carts> carts { get; set; }
        public DbSet<Orders> orders { get;}
        public DbSet<OrderItems> order_items { get; set; }
		public DbSet<Users> users { get; set; }
		public DbSet<UserAddresses> user_addresses { get; set; }



	}
}
