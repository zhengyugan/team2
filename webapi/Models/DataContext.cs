using Microsoft.EntityFrameworkCore;

namespace webapi.Models
{
	public class DataContext : DbContext
	{
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Products> products { get; set; }
		public DbSet<ProductCategories> product_categories { get; set; }
        public DbSet<ProductVariant> product_variants { get; set; }
        public DbSet<Carts> carts { get; set; }
        public DbSet<Orders> orders { get;}
        public DbSet<OrderItems> order_items { get; set; }
		public DbSet<Users> users { get; set; }
		public DbSet<UserAddresses> user_addresses { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Products>()
				.Property(b => b.created_at)
				.HasDefaultValueSql("getdate()");

			modelBuilder.Entity<ProductCategories>()
				.Property(b => b.created_at)
				.HasDefaultValueSql("getdate()");

			modelBuilder.Entity<ProductVariant>()
				.Property(b => b.created_at)
				.HasDefaultValueSql("getdate()");

			modelBuilder.Entity<Carts>()
				.Property(b => b.created_at)
				.HasDefaultValueSql("getdate()");

			modelBuilder.Entity<Orders>()
				.Property(b => b.created_at)
				.HasDefaultValueSql("getdate()");

			modelBuilder.Entity<OrderItems>()
				.Property(b => b.created_at)
				.HasDefaultValueSql("getdate()");

			modelBuilder.Entity<Users>()
				.Property(b => b.created_at)
				.HasDefaultValueSql("getdate()");

			modelBuilder.Entity<UserAddresses>()
				.Property(b => b.created_at)
				.HasDefaultValueSql("getdate()");
		}

	}
}
