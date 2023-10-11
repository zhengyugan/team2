using System.Linq;
using System.Reflection;
using webapi.Models;

namespace webapi
{
    public class DataSeed : IDataSeed
    {
        private readonly DataContext _context;

        public DataSeed(DataContext context)
        {
            _context = context;
        }

        public void SeedData(int nUsers, int nOrders)
        {
            if (!_context.product_categories.Any())
            {
                SeedProductCategories();
                _context.SaveChanges();
            }

            if (!_context.products.Any())
            {
                SeedProducts();
                _context.SaveChanges();
            }

            if (!_context.product_variants.Any())
            {
                SeedProductVariants();
                _context.SaveChanges();
            }

            if (!_context.users.Any())
            {
               SeedUsers(nUsers);
                _context.SaveChanges();
            }

            if (!_context.orders.Any())
            {
                SeedOrders(nOrders);
                _context.SaveChanges();
            }

            if (!_context.order_items.Any())
            {
                var orders = _context.orders.ToList();
                for (var i = 0; i <= orders.Count; i++)
                {
                    SeedOrderItems(orders[i]);
                    _context.SaveChanges();
                }
            }
        }

        private void SeedProductCategories()
        {
            List<ProductCategories> productCategories = BuildProductCategoryList();

            foreach (var productCategory in productCategories)
            {
                _context.product_categories.Add(productCategory);
            }
        }

        private List<ProductCategories> BuildProductCategoryList()
        {
            var productCategory = new List<ProductCategories>();
            
            productCategory.Add(new ProductCategories
            {
                name = "Dog",
                desc = "Dog Products"
            });

            productCategory.Add(new ProductCategories
            {
                name = "Cat",
                desc = "Cat Products"
            });

            return productCategory;
        }

        private void SeedProducts()
        {
            List<Products> products = BuildProductList();

            foreach (var product in products)
            {
                _context.products.Add(product);
            }
        }

        private List<Products> BuildProductList()
        {
            var products = new List<Products>();
            var productCategories = _context.product_categories.ToList();

            products.Add(new Products
            {
                name = "Classic Dog Collars",
                desc = "Crafted from high-quality, supple leather, this collar not only looks chic but also offers durability and comfort. It's the perfect accessory for any well-dressed canine companion",
                product_category = productCategories.First(productCategory => productCategory.id == 1),
                url = "1.jpg",
                sizing_type = "size",
                created_at = new DateTime(),
                created_by = 0
            });
            products.Add(new Products
            {
                name = "Adventurous Dog Collars",
                desc = "Designed with a special reflective strip, it ensures visibility in low-light conditions, enhancing your dog's safety.",
                product_category = productCategories.First(productCategory => productCategory.id == 1),
                url = "2.jpg",
                sizing_type = "size",
                created_at = new DateTime(),
                created_by = 0
            });
            products.Add(new Products
            {
                name = "Flexible Dog Collars",
                desc = "Our adjustable nylon dog collar is perfect for dogs of all sizes. Made from durable, weather-resistant nylon, it's built to withstand the elements.",
                product_category = productCategories.First(productCategory => productCategory.id == 1),
                url = "3.jpg",
                sizing_type = "size",
                created_at = new DateTime(),
                created_by = 0
            });
            products.Add(new Products
            {
                name = "Fashionable Step-In Harness",
                desc = "The fashionable patterns and soft padding make this harness both functional and fashion-forward, ensuring your dog stands out wherever you go.",
                product_category = productCategories.First(productCategory => productCategory.id == 1),
                url = "4.jpg",
                sizing_type = "size",
                created_at = new DateTime(),
                created_by = 0
            });
            products.Add(new Products
            {
                name = "Adventure-Ready Hiking Harness",
                desc = "Gear up for outdoor excursions with our rugged hiking dog harness. Built with sturdy, water-resistant materials and reinforced stitching.",
                product_category = productCategories.First(productCategory => productCategory.id == 1),
                url = "5.jpg",
                sizing_type = "size",
                created_at = new DateTime(),
                created_by = 0
            });
            products.Add(new Products
            {
                name = "Casual Y-Harness",
                desc = "Designed with a front leash attachment, it gently discourages pulling while providing maximum comfort.The breathable mesh material keeps your furry friend cool and cozy.",
                product_category = productCategories.First(productCategory => productCategory.id == 1),
                url = "6.jpg",
                sizing_type = "size",
                created_at = new DateTime(),
                created_by = 0
            });
            products.Add(new Products
            {
                name = "Retractable Dog Lead",
                desc = "With an extendable cord, it allows your pup to explore while you maintain command. The ergonomic handle provides a comfortable grip, and the one-button brake and lock system ensures safety on walks.",
                product_category = productCategories.First(productCategory => productCategory.id == 1),
                url = "7.jpg",
                sizing_type = "length",
                created_at = new DateTime(),
                created_by = 0
            });
            products.Add(new Products
            {
                name = "Durable Nylon Dog Lead",
                desc = "Made from high-quality nylon, it can withstand your dog's energy and enthusiasm.",
                product_category = productCategories.First(productCategory => productCategory.id == 1),
                url = "8.jpg",
                sizing_type = "length",
                created_at = new DateTime(),
                created_by = 0
            });
            products.Add(new Products
            {
                name = "Cat Harness",
                desc = "Adjustable straps for a secure and comfortable fit. Durable and lightweight materials for the cat's comfort.",
                product_category = productCategories.First(productCategory => productCategory.id == 2),
                url = "9.jpg",
                sizing_type = "size",
                created_at = new DateTime(),
                created_by = 0
            });
            products.Add(new Products
            {
                name = "Cat Lead",
                desc = "The length ranging from 4 to 6 feet, alloes your cat some freedom to explore while keeping them under your control.",
                product_category = productCategories.First(productCategory => productCategory.id == 2),
                url = "10.jpg",
                sizing_type = "length",
                created_at = new DateTime(),
                created_by = 0
            });
            products.Add(new Products
            {
                name = "Cat Collar",
                desc = "The length ranging from 4 to 6 feet, alloes your cat some freedom to explore while keeping them under your control.",
                product_category = productCategories.First(productCategory => productCategory.id == 2),
                url = "11.jpg",
                sizing_type = "size",
                created_at = new DateTime(),
                created_by = 0
            });

            return products;
        }

        private void SeedProductVariants()
        {
            List<ProductVariant> productVariants = BuildProductVariantList();

            foreach (var productVariant in productVariants)
            {
                _context.product_variants.Add(productVariant);
            }
        }

        private List<ProductVariant> BuildProductVariantList()
        {
            var productVariants = new List<ProductVariant>();
            var products = _context.products.ToList();

            productVariants.Add(new ProductVariant { quantity = 8, length = "12", size = "M", color = "Red", price = 18.50, product = products.First(c => c.id == 1), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 8, length = "4", size = "M", color = "Red", price = 20.50, product = products.First(c => c.id == 2), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 8, length = "12", size = "M", color = "Red", price = 11.90, product = products.First(c => c.id == 3), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 8, length = "10", size = "M", color = "Red", price = 16.70, product = products.First(c => c.id == 4), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 8, length = "8", size = "M", color = "Red", price = 20.00, product = products.First(c => c.id == 5), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 8, length = "6", size = "M", color = "Red", price = 23.50, product = products.First(c => c.id == 6), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 8, length = "2", size = "M", color = "Red", price = 19.00, product = products.First(c => c.id == 7), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 8, length = "1", size = "M", color = "Red", price = 44.50, product = products.First(c => c.id == 8), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 8, length = "11", size = "M", color = "Red", price = 23.50, product = products.First(c => c.id == 9), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 8, length = "3", size = "M", color = "Red", price = 22.50, product = products.First(c => c.id == 10), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 8, length = "5", size = "M", color = "Red", price = 28.50, product = products.First(c => c.id == 11), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 11, length = "12", size = "S", color = "Red", price = 18.50, product = products.First(c => c.id == 1), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 11, length = "4", size = "S", color = "Red", price = 20.50, product = products.First(c => c.id == 2), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 11, length = "12", size = "S", color = "Red", price = 11.90, product = products.First(c => c.id == 3), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 11, length = "10", size = "S", color = "Red", price = 16.70, product = products.First(c => c.id == 4), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 11, length = "8", size = "S", color = "Red", price = 20.00, product = products.First(c => c.id == 5), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 11, length = "6", size = "S", color = "Red", price = 23.50, product = products.First(c => c.id == 6), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 11, length = "2", size = "S", color = "Red", price = 19.00, product = products.First(c => c.id == 7), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 11, length = "1", size = "S", color = "Red", price = 44.50, product = products.First(c => c.id == 8), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 11, length = "11", size = "S", color = "Red", price = 23.50, product = products.First(c => c.id == 9), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 11, length = "3", size = "S", color = "Red", price = 22.50, product = products.First(c => c.id == 10), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 11, length = "5", size = "S", color = "Red", price = 28.50, product = products.First(c => c.id == 11), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 8, length = "12", size = "L", color = "Red", price = 18.50, product = products.First(c => c.id == 1), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 8, length = "4", size = "L", color = "Red", price = 20.50, product = products.First(c => c.id == 2), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 8, length = "12", size = "L", color = "Red", price = 11.90, product = products.First(c => c.id == 3), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 8, length = "10", size = "L", color = "Red", price = 16.70, product = products.First(c => c.id == 4), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 8, length = "8", size = "L", color = "Red", price = 20.00, product = products.First(c => c.id == 5), created_at = new DateTime(), created_by = 0});
            productVariants.Add(new ProductVariant { quantity = 8, length = "6", size = "L", color = "Red", price = 23.50, product = products.First(c => c.id == 6), created_at = new DateTime(), created_by = 0});
            productVariants.Add(new ProductVariant { quantity = 8, length = "2", size = "L", color = "Red", price = 19.00, product = products.First(c => c.id == 7), created_at = new DateTime(), created_by = 0});
            productVariants.Add(new ProductVariant { quantity = 8, length = "1", size = "L", color = "Red", price = 44.50, product = products.First(c => c.id == 8), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 8, length = "11", size = "L", color = "Red", price = 23.50, product = products.First(c => c.id == 9), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 8, length = "3", size = "L", color = "Red", price = 22.50, product = products.First(c => c.id == 10), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 8, length = "5", size = "L", color = "Red", price = 28.50, product = products.First(c => c.id == 11), created_at = new DateTime(), created_by = 0 });
            
            productVariants.Add(new ProductVariant { quantity = 10, length = "12", size = "M", color = "Blue", price = 18.50, product = products.First(c => c.id == 1), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 10, length = "4", size = "M", color = "Blue", price = 20.50, product = products.First(c => c.id == 2), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 10, length = "12 ", size = "M", color = "Blue", price = 11.90, product = products.First(c => c.id == 3), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 10, length = "10", size = "M", color = "Blue", price = 16.70, product = products.First(c => c.id == 4), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 10, length = "8", size = "M", color = "Blue", price = 20.00, product = products.First(c => c.id == 5), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 10, length = "6", size = "M", color = "Blue", price = 23.50, product = products.First(c => c.id == 6), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 10, length = "2", size = "M", color = "Blue", price = 19.00, product = products.First(c => c.id == 7), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 10, length = "1", size = "M", color = "Blue", price = 44.50, product = products.First(c => c.id == 8), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 10, length = "11", size = "M", color = "Blue", price = 23.50, product = products.First(c => c.id == 9), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 10, length = "3", size = "M", color = "Blue", price = 22.50, product = products.First(c => c.id == 10), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 10, length = "5", size = "M", color = "Blue", price = 28.50, product = products.First(c => c.id == 11), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 10, length = "12", size = "L", color = "Blue", price = 18.50, product = products.First(c => c.id == 1), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 10, length = "4", size = "L", color = "Blue", price = 20.50, product = products.First(c => c.id == 2), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 10, length = "12", size = "L", color = "Blue", price = 11.90, product = products.First(c => c.id == 3), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 10, length = "10", size = "L", color = "Blue", price = 16.70, product = products.First(c => c.id == 4), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 10, length = "8", size = "L", color = "Blue", price = 20.00, product = products.First(c => c.id == 5), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 10, length = "6", size = "L", color = "Blue", price = 23.50, product = products.First(c => c.id == 6), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 10, length = "2", size = "L", color = "Blue", price = 19.00, product = products.First(c => c.id == 7), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 10, length = "1", size = "L", color = "Blue", price = 44.50, product = products.First(c => c.id == 8), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 10, length = "11", size = "L", color = "Blue", price = 23.50, product = products.First(c => c.id == 9), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 10, length = "3", size = "L", color = "Blue", price = 22.50, product = products.First(c => c.id == 10), created_at = new DateTime(), created_by = 0 });
            productVariants.Add(new ProductVariant { quantity = 10, length = "5", size = "L", color = "Blue", price = 28.50, product = products.First(c => c.id == 11), created_at = new DateTime(), created_by = 0 });

            return productVariants;
        }

        private void SeedUsers(int n)
        {
            List<Users> users = BuildUserList(n);

            foreach (var user in users)
            {
                _context.users.Add(user);
            }
        }

        private List<Users> BuildUserList(int nCustomers)
        {
            var users = new List<Users>();
            var names = new List<string>();

            for (var i = 1; i <= nCustomers; i++)
            {
                var name = Helpers.MakeUniqueUserName(names);
                names.Add(name);

                users.Add(new Users
                {
                    username = name,
                    password = Helpers.GenerateRandomPassword(6),
                    first_name = name.Split('_')[0],
                    last_name = name.Split('_')[1],
                    telephone = Helpers.GenerateRandomTelephoneNumber(),
                    mobile = Helpers.GenerateRandomMobileNumber(),
                    email = Helpers.MakeCustomerEmail(name),
                    role = "user"
                });
            }

            return users;
        }

        private void SeedOrders(int n)
        {
            List<Orders> orders = BuildOrderList(n);

            foreach (var order in orders)
            {
                _context.orders.Add(order);
            }
        }

        private List<Orders> BuildOrderList(int nOrders)
        {
            var orders = new List<Orders>();
            var rand = new Random();

            for (var i = 1; i <= nOrders; i++)
            {
                var randUserId = rand.Next(1, _context.users.Count());
                var placed = Helpers.GetRandomOrderPlaced();
                var completed = Helpers.GetRandomOrderCompleted(placed);
                var users = _context.users.ToList();

                orders.Add(new Orders
                {
                    users = users.First(user => user.id == randUserId),
                    total = Helpers.GetRandomOrderTotal(),
                    payment_id = Helpers.GenerateRandomPaymentId(),
                    payment_status = completed != null ? "completed" : "pending",
                    order_status = completed != null ? "completed" : "pending",
                    moodified_at = completed,
                    modified_by = 0,
                    created_at = placed,
                    created_by = 0
                }); ;
            }

            return orders;
        }

        private void SeedOrderItems(Orders order)
        {
            List<OrderItems> orderItems = BuildOrderItemsList(order);

            foreach (var orderItem in orderItems)
            {
                _context.order_items.Add(orderItem);
            }
        }

        private List<OrderItems> BuildOrderItemsList(Orders order)
        {
            var orderItems = new List<OrderItems>();
            var rand1 = new Random();
            var rand2 = new Random();
            Random random = new Random();
            var nOrderItems = random.Next(1, 5);

            for (var j = 1; j <= nOrderItems; j++)
            {
                var randProductVariantId = rand1.Next(1, _context.product_variants.Count());
                var placed = Helpers.GetRandomOrderPlaced();
                var completed = Helpers.GetRandomOrderCompleted(placed);
                var product_variant = _context.product_variants.ToList();

                orderItems.Add(new OrderItems
                {
                    order = order,
                    product_variant = product_variant.First(product_variant => product_variant.id == randProductVariantId),
                    quantity = rand2.Next(1, 10),
                    created_at = placed,
                    created_by = 0
                });
            }

            return orderItems;
        }
    }
}
