using System.Collections.Immutable;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using webapi.Migrations;
using webapi.Models;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private DataContext _context;
        public ProductController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult getProducts()
        {
            var products = _context.products.ToList();

            return Ok(new ApiResponseWrapper("", products.ToArray()));
        }

		[HttpGet("GetAllProducts")]
		public IActionResult getAllProducts()
		{
			var distinctVal = _context.product_variants.Select(
				e=>e.product.id).Distinct().ToArray();

			var products = (from prod_variant in _context.product_variants
						join product in _context.products on prod_variant.product.id equals product.id
						select new
						{
							product.id,
							prod_variant.quantity,
							prod_variant.size,
							prod_variant.color,
							prod_variant.length,
							prod_variant.price,
							product.name,
							product.desc,
							product.url
						}).Take(distinctVal.Length).ToList();

			if (products.Count > 0)
			{
				return Ok(new ApiResponseWrapper("", products.ToArray()));
			}
			else
			{
				return NotFound(new ApiResponseWrapper("Products not found!", products.ToArray()));
			}

		}

		[HttpGet("{id}", Name = "getProduct")]
        public IActionResult getProduct(int id)
        {
			var product = _context.products.Where(product => product.id == id).ToList();

			if (product.Count > 0)
			{
				return Ok(new ApiResponseWrapper("", product.ToArray()));
			}
			else
			{
				return NotFound(new ApiResponseWrapper("Product not found!", product.ToArray()));
			}
		}

        [HttpGet("GetProductById/{id}")]
		public IActionResult getProductById(int id)
		{
			var prod = (from prod_variant in _context.product_variants where prod_variant.id == id
							join product in _context.products on prod_variant.product.id equals product.id
						select new
							{
								prod_variant.id,
								prod_variant.quantity,
								prod_variant.size,
								prod_variant.color,
								prod_variant.length,
								prod_variant.price,
								product.name,
								product.desc,
								product.url
							}).ToList();

			if (prod.Count > 0)
			{
				return Ok(new ApiResponseWrapper("", prod.ToArray()));
			}
			else
			{
				return NotFound(new ApiResponseWrapper("Product not found!", prod.ToArray()));
			}
		}

		[HttpPost("StoreProduct")]
        public IActionResult storeProduct([FromBody] Products product)
        {
            _context.products.Add(product);
            _context.SaveChanges();

            List<Products> objList = new List<Products>();
            objList.Add(product);

            return Ok(new ApiResponseWrapper("", objList.ToArray()));
        }

		[HttpPost("AddItem")]
		public async Task<IActionResult> AddItemAsync([FromBody] CartInfo cartInfo)
		{
			var prodVariantList = _context.product_variants.ToList();
			var userList = _context.users.ToList();

			var cart = new Carts
			{
				user = userList.First(c => c.id == cartInfo.user_id),
				product_variant = prodVariantList.First(c => c.id == cartInfo.product_variant_id),
				quantity = cartInfo.quantity
			};
			_context.carts.Add(cart);
		

			int initialQuantity = prodVariantList.Single(c => c.id == cartInfo.product_variant_id).quantity;

			int finalQuantity = initialQuantity - cartInfo.quantity;

			var updateQuery = _context.product_variants.Find(cartInfo.product_variant_id);
			updateQuery.quantity = finalQuantity;

			List<Carts> objList = new List<Carts>();

			await _context.SaveChangesAsync();

			return Ok(new ApiResponseWrapper("", objList.ToArray()));

		}

		[HttpGet("GetProductVariation/{id}")]
		public IActionResult GetProductVariation(int id)
		{

			var variation = (from prod_variant in _context.product_variants
							 where prod_variant.product.id == id
							 select new
							 {
								 prod_variant.size,
								 prod_variant.color,
								 prod_variant.id,
								 prod_variant.quantity
							 }).ToList();
			if (variation.Count > 0)
			{
				return Ok(new ApiResponseWrapper("", variation.ToArray()));
			}
			else
			{
				return NotFound(new ApiResponseWrapper("Product not found!", variation.ToArray()));
			}
			

		}

		[HttpGet("AddMockItem")]
        public async Task<IActionResult> AddMockItemAsync()
        {
			var mockProductCat = new List<ProductCategories>
							  {
								  new ProductCategories{name="Dog",desc="Dog Products"},
								  new ProductCategories{name="Cat",desc="Cat Products"}

					 };
			_context.AddRange(mockProductCat);
			await _context.SaveChangesAsync();

			var prod_cat = _context.product_categories.ToList();

			var mockProduct = new List<Products>
					 {
							 new Products{name="Classic Dog Collars",desc="Crafted from high-quality, supple leather, this collar not only looks chic but also offers durability and comfort. It's the perfect accessory for any well-dressed canine companion",
										 product_category=prod_cat.First(c=>c.id ==1),url="assets/images/dog_collar_1.jpg" },

							 new Products{name="Adventurous Dog Collars",desc="Designed with a special reflective strip, it ensures visibility in low-light conditions, enhancing your dog's safety.",
										  product_category=prod_cat.First(c=>c.id ==1),url="assets/images/dog_collar_2.jpg" },

							 new Products{name="Flexible Dog Collars",desc="Our adjustable nylon dog collar is perfect for dogs of all sizes. Made from durable, weather-resistant nylon, it's built to withstand the elements.",
										  product_category=prod_cat.First(c=>c.id ==1),url="assets/images/dog_collar_3.jpg" },

							 new Products{name="Fashionable Step-In Harness",desc="The fashionable patterns and soft padding make this harness both functional and fashion-forward, ensuring your dog stands out wherever you go",
										  product_category=prod_cat.First(c=>c.id ==1),url="assets/images/dog_harness_1.jpg" },

							 new Products{name="Adventure-Ready Hiking Harness",desc="Gear up for outdoor excursions with our rugged hiking dog harness. Built with sturdy, water-resistant materials and reinforced stitching",
										 product_category = prod_cat.First(c => c.id == 1),url="assets/images/dog_harness_2.jpg" },

							 new Products{name="Casual Y-Harness",desc="Designed with a front leash attachment, it gently discourages pulling while providing maximum comfort.The breathable mesh material keeps your furry friend cool and cozy",
										 product_category = prod_cat.First(c => c.id == 1),url="assets/images/dog_harness_3.jpg" },

							 new Products{name="Retractable Dog Lead",desc="With an extendable cord, it allows your pup to explore while you maintain command. The ergonomic handle provides a comfortable grip, and the one-button brake and lock system ensures safety on walks.",
										 product_category = prod_cat.First(c => c.id == 1),url="assets/images/dog_lead_2.jpg" },

							 new Products{name="Durable Nylon Dog Lead",desc="Made from high-quality nylon, it can withstand your dog's energy and enthusiasm.",
										 product_category = prod_cat.First(c => c.id == 1),url="assets/images/dog_lead_1.jpg" },

							 new Products{name="Cat Harness",desc="Adjustable straps for a secure and comfortable fit. Durable and lightweight materials for the cat's comfort.",
										 product_category = prod_cat.First(c => c.id == 2),url="assets/images/cat_harness_1.jpg" },

							 new Products{name="Cat Lead",desc="The length ranging from 4 to 6 feet, alloes your cat some freedom to explore while keeping them under your control",
										 product_category = prod_cat.First(c => c.id == 2),url="assets/images/cat_lead_1.jpg" },

							 new Products{name="Cat Collar",desc="The length ranging from 4 to 6 feet, alloes your cat some freedom to explore while keeping them under your control",
										 product_category = prod_cat.First(c => c.id == 2),url="assets/images/cat_collar_1.jpg" }
						 };
			_context.AddRange(mockProduct);
			await _context.SaveChangesAsync();

			var prod = _context.products.ToList();
			var mockProdVariation = new List<ProductVariant>
				{
					new ProductVariant{quantity=8,length="12 meter",size="M",color="Red",price=18.50,product=prod.First(c => c.id == 1)},
					new ProductVariant{quantity=8,length="4 meter",size="M",color="Red",price=20.50,product=prod.First(c => c.id == 2)},
					new ProductVariant{quantity=8,length="12 meter",size="M",color="Red",price=11.90,product = prod.First(c => c.id == 3)},
					new ProductVariant{quantity=8,length="10 meter",size="M",color="Red",price=16.70,product = prod.First(c => c.id == 4)},
					new ProductVariant{quantity=8,length="8 meter",size="M",color="Red",price=20.00,product = prod.First(c => c.id == 5)},
					new ProductVariant{quantity=8,length="6 meter",size="M",color="Red",price=23.50,product = prod.First(c => c.id == 6)},
					new ProductVariant{quantity=8,length="2 meter",size="M",color="Red",price=19.00,product = prod.First(c => c.id == 7)},
					new ProductVariant{quantity=8,length="1 meter",size="M",color="Red",price=44.50,product = prod.First(c => c.id == 8)},
					new ProductVariant{quantity=8,length="11 meter",size="M",color="Red",price=23.50,product = prod.First(c => c.id == 9)},
					new ProductVariant{quantity=8,length="3 meter",size="M",color="Red",price=22.50,product = prod.First(c => c.id == 10)},
					new ProductVariant{quantity=8,length="5 meter",size="M",color="Red",price=28.50,product = prod.First(c => c.id == 11)},
					new ProductVariant{quantity=11,length="12 meter",size="S",color="Red",price=18.50,product=prod.First(c => c.id == 1)},
					new ProductVariant{quantity=11,length="4 meter",size="S",color="Red",price=20.50,product=prod.First(c => c.id == 2)},
					new ProductVariant{quantity=11,length="12 meter",size="S",color="Red",price=11.90,product = prod.First(c => c.id == 3)},
					new ProductVariant{quantity=11,length="10 meter",size="S",color="Red",price=16.70,product = prod.First(c => c.id == 4)},
					new ProductVariant{quantity=11,length="8 meter",size="S",color="Red",price=20.00,product = prod.First(c => c.id == 5)},
					new ProductVariant{quantity=11,length="6 meter",size="S",color="Red",price=23.50,product = prod.First(c => c.id == 6)},
					new ProductVariant{quantity=11,length="2 meter",size="S",color="Red",price=19.00,product = prod.First(c => c.id == 7)},
					new ProductVariant{quantity=11,length="1 meter",size="S",color="Red",price=44.50,product = prod.First(c => c.id == 8)},
					new ProductVariant{quantity=11,length="11 meter",size="S",color="Red",price=23.50,product = prod.First(c => c.id == 9)},
					new ProductVariant{quantity=11,length="3 meter",size="S",color="Red",price=22.50,product = prod.First(c => c.id == 10)},
					new ProductVariant{quantity=11,length="5 meter",size="S",color="Red",price=28.50,product = prod.First(c => c.id == 11)},
					new ProductVariant{quantity=8,length="12 meter",size="L",color="Red",price=18.50,product=prod.First(c => c.id == 1)},
					new ProductVariant{quantity=8,length="4 meter",size="L",color="Red",price=20.50,product=prod.First(c => c.id == 2)},
					new ProductVariant{quantity=8,length="12 meter",size="L",color="Red",price=11.90,product = prod.First(c => c.id == 3)},
					new ProductVariant{quantity=8,length="10 meter",size="L",color="Red",price=16.70,product = prod.First(c => c.id == 4)},
					new ProductVariant{quantity=8,length="8 meter",size="L",color="Red",price=20.00,product = prod.First(c => c.id == 5)},
					new ProductVariant{quantity=8,length="6 meter",size="L",color="Red",price=23.50,product = prod.First(c => c.id == 6)},
					new ProductVariant{quantity=8,length="2 meter",size="L",color="Red",price=19.00,product = prod.First(c => c.id == 7)},
					new ProductVariant{quantity=8,length="1 meter",size="L",color="Red",price=44.50,product = prod.First(c => c.id == 8)},
					new ProductVariant{quantity=8,length="11 meter",size="L",color="Red",price=23.50,product = prod.First(c => c.id == 9)},
					new ProductVariant{quantity=8,length="3 meter",size="L",color="Red",price=22.50,product = prod.First(c => c.id == 10)},
					new ProductVariant{quantity=8,length="5 meter",size="L",color="Red",price=28.50,product = prod.First(c => c.id == 11)},

					new ProductVariant{quantity=10,length="12 meter",size="M",color="Blue",price=18.50,product=prod.First(c => c.id == 1)},
					new ProductVariant{quantity=10,length="4 meter",size="M",color="Blue",price=20.50,product=prod.First(c => c.id == 2)},
					new ProductVariant{quantity=10,length="12 meter",size="M",color="Blue",price=11.90,product = prod.First(c => c.id == 3)},
					new ProductVariant{quantity=10,length="10 meter",size="M",color="Blue",price=16.70,product = prod.First(c => c.id == 4)},
					new ProductVariant{quantity=10,length="8 meter",size="M",color="Blue",price=20.00,product = prod.First(c => c.id == 5)},
					new ProductVariant{quantity=10,length="6 meter",size="M",color="Blue",price=23.50,product = prod.First(c => c.id == 6)},
					new ProductVariant{quantity=10,length="2 meter",size="M",color="Blue",price=19.00,product = prod.First(c => c.id == 7)},
					new ProductVariant{quantity=10,length="1 meter",size="M",color="Blue",price=44.50,product = prod.First(c => c.id == 8)},
					new ProductVariant{quantity=10,length="11 meter",size="M",color="Blue",price=23.50,product = prod.First(c => c.id == 9)},
					new ProductVariant{quantity=10,length="3 meter",size="M",color="Blue",price=22.50,product = prod.First(c => c.id == 10)},
					new ProductVariant{quantity=10,length="5 meter",size="M",color="Blue",price=28.50,product = prod.First(c => c.id == 11)},
					new ProductVariant{quantity=10,length="12 meter",size="L",color="Blue",price=18.50,product=prod.First(c => c.id == 1)},
					new ProductVariant{quantity=10,length="4 meter",size="L",color="Blue",price=20.50,product=prod.First(c => c.id == 2)},
					new ProductVariant{quantity=10,length="12 meter",size="L",color="Blue",price=11.90,product = prod.First(c => c.id == 3)},
					new ProductVariant{quantity=10,length="10 meter",size="L",color="Blue",price=16.70,product = prod.First(c => c.id == 4)},
					new ProductVariant{quantity=10,length="8 meter",size="L",color="Blue",price=20.00,product = prod.First(c => c.id == 5)},
					new ProductVariant{quantity=10,length="6 meter",size="L",color="Blue",price=23.50,product = prod.First(c => c.id == 6)},
					new ProductVariant{quantity=10,length="2 meter",size="L",color="Blue",price=19.00,product = prod.First(c => c.id == 7)},
					new ProductVariant{quantity=10,length="1 meter",size="L",color="Blue",price=44.50,product = prod.First(c => c.id == 8)},
					new ProductVariant{quantity=10,length="11 meter",size="L",color="Blue",price=23.50,product = prod.First(c => c.id == 9)},
					new ProductVariant{quantity=10,length="3 meter",size="L",color="Blue",price=22.50,product = prod.First(c => c.id == 10)},
					new ProductVariant{quantity=10,length="5 meter",size="L",color="Blue",price=28.50,product = prod.First(c => c.id == 11)}

				};
			_context.AddRange(mockProdVariation);
			await _context.SaveChangesAsync();

			List<Carts> objList = new List<Carts>();


			return Ok(new ApiResponseWrapper("Success", objList.ToArray()));

        }

    }
}
