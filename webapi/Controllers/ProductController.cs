using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Models;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace webapi.Controllers
{
	[ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private DataContext _context;
        public ProductController(DataContext context, IConfiguration _configutation)
        {
            _context = context;
			_configuration = _configutation;
        }

        [HttpGet("{pageIndex:int}/{pageSize:int}")]
        public IActionResult Get(int pageIndex, int pageSize)
        {
            var data = _context.product_variants?.Include(product_variant => product_variant.product)
                .Where(product_variant => product_variant.quantity <= 10);

            var page = new PaginatedResponse<ProductVariant>(data, pageIndex, pageSize);

            var totalCount = (int)((data != null && data.Any()) ? (data?.Count()) : 0);
            var totalPages = Math.Ceiling((double)totalCount / pageSize);

            var response = new
            {
                Page = page,
                TotalPages = totalPages
            };

            return Ok(response);
        }

        [HttpGet]
        public IActionResult getProducts()
        {
			var products = _context.products.
				Include(products => products.product_category).
				Include(products => products.product_variants).
				OrderByDescending(product => product.id).
				ToList();

            return Ok(new ApiResponseWrapper("", products.ToArray()));
        }

		[HttpGet("GetAllProducts/{pageIndex:int}/{pageSize:int}")]
		public IActionResult getAllProducts(int pageIndex, int pageSize)
		{
			var distinctVal = _context.product_variants.Select(
				e=>e.product.id).Distinct().ToArray();

			var details = new ProblemDetails();

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
							product.url,
							product.deleted_at
						}).GroupBy(obj=>obj.id).Select(group=>group.First()).ToList();
			
			
			var Data = products.Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
			var Total = products.Count();

            var totalCount = (int)((products != null && products.Any()) ? (products?.Count()) : 0);
            var totalPages = Math.Ceiling((double)totalCount / pageSize);

            var response = new
            {
                product = Data,
				productTotal = Total,
                TotalPages = totalPages
            };

            return Ok(response);

		}

        [HttpPost("StoreProduct")]
        public IActionResult StoreProduct([FromBody] StoreProductDto productDto)
        {
            Console.WriteLine("here");
			Console.WriteLine(productDto);
			Console.WriteLine("there");
			ProductCategories product_category = _context.product_categories.Find(productDto.ProductCategoryId);
			Products? product = new Products
			{
				product_category = product_category,
				name = productDto.Name,
				desc = productDto.Description,
				url = "url",
				sizing_type = productDto.SizingType,
				created_at = DateTime.Now,
				created_by = 1,
			};
			_context.products.Add(product);
			_context.SaveChanges();

			foreach (var prouctVariant in productDto.ProductVariants)
			{
				ProductVariant productVariants = new ProductVariant
				{
					quantity = prouctVariant.Quantity,
					size = prouctVariant.Size,
					color = prouctVariant.Color,
					length = prouctVariant.Length,
					price = productDto.Price,
					product = product,
					created_at = DateTime.Now,
					created_by = 1,
				};

				_context.product_variants.Add(productVariants);
			}
			_context.SaveChanges();

			var productStored = _context.products.Where(p => p.id == product.id).ToList();

			return Ok(new ApiResponseWrapper("", productStored.ToArray()));
		}

        [HttpPatch("PatchProduct")]
        public IActionResult PatchProduct([FromBody] StoreProductDto productDto)
        {
            ProductCategories? product_category = _context.product_categories.Find(productDto.ProductCategoryId);
            
			if (product_category != null)
			{
                Products? updateProduct = _context.products.Where(product => product.id == productDto.id).FirstOrDefault();
				updateProduct.product_category = product_category;
				updateProduct.name = productDto.Name;
				updateProduct.desc = productDto.Description;
				updateProduct.url = "update";
				updateProduct.sizing_type = productDto.SizingType;
				updateProduct.moodified_at = DateTime.Now;
				updateProduct.modified_by = 1;
				_context.SaveChanges();

                foreach (var prouctVariant in productDto.ProductVariants)
				{
					if(prouctVariant.ProductVariantId != null)
					{
						ProductVariant updateProductVariant = _context.product_variants.Find(prouctVariant.ProductVariantId);
						updateProductVariant.quantity = prouctVariant.Quantity;
						updateProductVariant.size = prouctVariant.Size;
						updateProductVariant.price = productDto.Price;
						updateProductVariant.color = prouctVariant.Color;
						updateProductVariant.length = prouctVariant.Length;
						updateProductVariant.moodified_at = DateTime.Now;
						updateProductVariant.modified_by = 1;
                        updateProductVariant.deleted_by = 1;
						_context.SaveChanges();
					}
					else
					{
                        ProductVariant storeProductVariant = new ProductVariant
                        {
                            quantity = prouctVariant.Quantity,
                            size = prouctVariant.Size,
                            color = prouctVariant.Color,
                            length = prouctVariant.Length,
                            price = productDto.Price,
                            product = updateProduct,
                            created_at = DateTime.Now,
                            created_by = 1,
                        };

                        _context.product_variants.Add(storeProductVariant);
                    }
				}

				_context.SaveChanges();

				var productUpdated = _context.products.Where(p => p.id == updateProduct.id).ToList();

				return Ok(new ApiResponseWrapper("", productUpdated.ToArray()));
            }
			else
			{
                return NotFound(new ApiResponseWrapper("Product category not found!", null));
            }
        }


        [HttpGet("{id}", Name = "getProduct")]
        public IActionResult getProduct(int id)
        {
            var product = _context.products.Where(product => product.id == id).Include(products => products.product_category).
				Include(products => products.product_variants).ToList();

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
			var prod = (from prod_variant in _context.product_variants where prod_variant.product.id == id
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
								 prod_variant.quantity,
								 prod_variant.length
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

		[HttpPut("DeleteItem/{id}")]
		public IActionResult DeleteItem(int id)
		{
			var productList = _context.products.Find(id);

            if (productList == null)
            {
                return NotFound();
            }

			productList.deleted_at = DateTime.UtcNow;
			_context.SaveChanges();

			return NoContent();
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
										 product_category=prod_cat.First(c=>c.id ==1),url="assets/images/dog_collar_1.jpg",sizing_type="size"},

							 new Products{name="Adventurous Dog Collars",desc="Designed with a special reflective strip, it ensures visibility in low-light conditions, enhancing your dog's safety.",
										  product_category=prod_cat.First(c=>c.id ==1),url="assets/images/dog_collar_2.jpg",sizing_type="size" },

							 new Products{name="Flexible Dog Collars",desc="Our adjustable nylon dog collar is perfect for dogs of all sizes. Made from durable, weather-resistant nylon, it's built to withstand the elements.",
										  product_category=prod_cat.First(c=>c.id ==1),url="assets/images/dog_collar_3.jpg",sizing_type="size" },

							 new Products{name="Fashionable Step-In Harness",desc="The fashionable patterns and soft padding make this harness both functional and fashion-forward, ensuring your dog stands out wherever you go",
										  product_category=prod_cat.First(c=>c.id ==1),url="assets/images/dog_harness_1.jpg",sizing_type="size" },

							 new Products{name="Adventure-Ready Hiking Harness",desc="Gear up for outdoor excursions with our rugged hiking dog harness. Built with sturdy, water-resistant materials and reinforced stitching",
										 product_category = prod_cat.First(c => c.id == 1),url="assets/images/dog_harness_2.jpg",sizing_type="size" },

							 new Products{name="Casual Y-Harness",desc="Designed with a front leash attachment, it gently discourages pulling while providing maximum comfort.The breathable mesh material keeps your furry friend cool and cozy",
										 product_category = prod_cat.First(c => c.id == 1),url="assets/images/dog_harness_3.jpg",sizing_type="size" },

							 new Products{name="Retractable Dog Lead",desc="With an extendable cord, it allows your pup to explore while you maintain command. The ergonomic handle provides a comfortable grip, and the one-button brake and lock system ensures safety on walks.",
										 product_category = prod_cat.First(c => c.id == 1),url="assets/images/dog_lead_2.jpg" ,sizing_type="length"},

							 new Products{name="Durable Nylon Dog Lead",desc="Made from high-quality nylon, it can withstand your dog's energy and enthusiasm.",
										 product_category = prod_cat.First(c => c.id == 1),url="assets/images/dog_lead_1.jpg",sizing_type="length" },

							 new Products{name="Cat Harness",desc="Adjustable straps for a secure and comfortable fit. Durable and lightweight materials for the cat's comfort.",
										 product_category = prod_cat.First(c => c.id == 2),url="assets/images/cat_harness_1.jpg",sizing_type="size" },

							 new Products{name="Cat Lead",desc="The length ranging from 4 to 6 feet, alloes your cat some freedom to explore while keeping them under your control",
										 product_category = prod_cat.First(c => c.id == 2),url="assets/images/cat_lead_1.jpg" ,sizing_type="length"},

							 new Products{name="Cat Collar",desc="The length ranging from 4 to 6 feet, alloes your cat some freedom to explore while keeping them under your control",
										 product_category = prod_cat.First(c => c.id == 2),url="assets/images/cat_collar_1.jpg",sizing_type="size" }
						 };
			_context.AddRange(mockProduct);
			await _context.SaveChangesAsync();

			var prod = _context.products.ToList();
			var mockProdVariation = new List<ProductVariant>
				{
					 new ProductVariant{quantity=8,length="",size="S",color="Red",price=18.50,product=prod.First(c => c.id == 13)},
					 new ProductVariant{quantity=8,length="",size="M",color="Red",price=18.50,product=prod.First(c => c.id == 13)},
					 new ProductVariant{quantity=8,length="",size="L",color="Red",price=18.50,product=prod.First(c => c.id == 13)},
					 new ProductVariant{quantity=8,length="",size="S",color="Blue",price=18.50,product=prod.First(c => c.id == 13)},
					 new ProductVariant{quantity=8,length="",size="M",color="Blue",price=18.50,product=prod.First(c => c.id == 13)},
					 new ProductVariant{quantity=8,length="",size="L",color="Blue",price=18.50,product=prod.First(c => c.id == 13)},
					 new ProductVariant{quantity=8,length="",size="S",color="Yellow",price=18.50,product=prod.First(c => c.id == 13)},
					 new ProductVariant{quantity=8,length="",size="M",color="Yellow",price=18.50,product=prod.First(c => c.id == 13)},
					 new ProductVariant{quantity=8,length="",size="L",color="Yellow",price=18.50,product=prod.First(c => c.id == 13)},

					 new ProductVariant{quantity=8,length="",size="S",color="Red",price=18.50,product=prod.First(c => c.id == 14)},
					 new ProductVariant{quantity=8,length="",size="M",color="Red",price=18.50,product=prod.First(c => c.id == 14)},
					 new ProductVariant{quantity=8,length="",size="L",color="Red",price=18.50,product=prod.First(c => c.id == 14)},
					 new ProductVariant{quantity=8,length="",size="S",color="Blue",price=18.50,product=prod.First(c => c.id == 14)},
					 new ProductVariant{quantity=8,length="",size="M",color="Blue",price=18.50,product=prod.First(c => c.id == 14)},
					 new ProductVariant{quantity=8,length="",size="L",color="Blue",price=18.50,product=prod.First(c => c.id == 14)},
					 new ProductVariant{quantity=8,length="",size="S",color="Yellow",price=18.50,product=prod.First(c => c.id == 14)},
					 new ProductVariant{quantity=8,length="",size="M",color="Yellow",price=18.50,product=prod.First(c => c.id == 14)},
					 new ProductVariant{quantity=8,length="",size="L",color="Yellow",price=18.50,product=prod.First(c => c.id == 14)},

					 new ProductVariant{quantity=11,length="",size="S",color="Red",price=20.50,product=prod.First(c => c.id == 15)},
					 new ProductVariant{quantity=11,length="",size="M",color="Red",price=20.50,product=prod.First(c => c.id == 15)},
					 new ProductVariant{quantity=11,length="",size="L",color="Red",price=20.50,product=prod.First(c => c.id == 15)},
					 new ProductVariant{quantity=11,length="",size="S",color="Blue",price=20.50,product=prod.First(c => c.id == 15)},
					 new ProductVariant{quantity=11,length="",size="M",color="Blue",price=20.50,product=prod.First(c => c.id == 15)},
					 new ProductVariant{quantity=11,length="",size="L",color="Blue",price=20.50,product=prod.First(c => c.id == 15)},
					 new ProductVariant{quantity=11,length="",size="S",color="Yellow",price=20.50,product=prod.First(c => c.id == 15)},
					 new ProductVariant{quantity=11,length="",size="M",color="Yellow",price=20.50,product=prod.First(c => c.id == 15)},
					 new ProductVariant{quantity=11,length="",size="L",color="Yellow",price=20.50,product=prod.First(c => c.id == 15)},

					 new ProductVariant{quantity=11,length="",size="S",color="Red",price=23.50,product=prod.First(c => c.id == 16)},
					 new ProductVariant{quantity=11,length="",size="M",color="Red",price=23.50,product=prod.First(c => c.id == 16)},
					 new ProductVariant{quantity=11,length="",size="L",color="Red",price=23.50,product=prod.First(c => c.id == 16)},
					 new ProductVariant{quantity=11,length="",size="XL",color="Red",price=23.50,product=prod.First(c => c.id == 16)},
					 new ProductVariant{quantity=11,length="",size="S",color="Blue",price=23.50,product=prod.First(c => c.id == 16)},
					 new ProductVariant{quantity=11,length="",size="M",color="Blue",price=23.50,product=prod.First(c => c.id == 16)},
					 new ProductVariant{quantity=11,length="",size="L",color="Blue",price=23.50,product=prod.First(c => c.id ==16)},
					 new ProductVariant{quantity=11,length="",size="XL",color="Blue",price=23.50,product=prod.First(c => c.id == 16)},
					 new ProductVariant{quantity=11,length="",size="S",color="Yellow",price=23.50,product=prod.First(c => c.id == 16)},
					 new ProductVariant{quantity=11,length="",size="M",color="Yellow",price=23.50,product=prod.First(c => c.id == 16)},
					 new ProductVariant{quantity=11,length="",size="L",color="Yellow",price=23.50,product=prod.First(c => c.id == 16)},
					 new ProductVariant{quantity=11,length="",size="XL",color="Yellow",price=23.50,product=prod.First(c => c.id == 16)},
					 new ProductVariant{quantity=11,length="",size="S",color="Green",price=23.50,product=prod.First(c => c.id ==16)},
					 new ProductVariant{quantity=11,length="",size="M",color="Green",price=23.50,product=prod.First(c => c.id == 16)},
					 new ProductVariant{quantity=11,length="",size="L",color="Green",price=23.50,product=prod.First(c => c.id == 16)},
					 new ProductVariant{quantity=11,length="",size="XL",color="Green",price=23.50,product=prod.First(c => c.id == 16)},

					 new ProductVariant{quantity=11,length="",size="S",color="Red",price=23.50,product=prod.First(c => c.id == 17)},
					 new ProductVariant{quantity=11,length="",size="M",color="Red",price=23.50,product=prod.First(c => c.id == 17)},
					 new ProductVariant{quantity=11,length="",size="L",color="Red",price=23.50,product=prod.First(c => c.id == 17)},
					 new ProductVariant{quantity=11,length="",size="XL",color="Red",price=23.50,product=prod.First(c => c.id == 17)},
					 new ProductVariant{quantity=11,length="",size="S",color="Blue",price=23.50,product=prod.First(c => c.id == 17)},
					 new ProductVariant{quantity=11,length="",size="M",color="Blue",price=23.50,product=prod.First(c => c.id == 17)},
					 new ProductVariant{quantity=11,length="",size="L",color="Blue",price=23.50,product=prod.First(c => c.id == 17)},
					 new ProductVariant{quantity=11,length="",size="XL",color="Blue",price=23.50,product=prod.First(c => c.id == 17)},
					 new ProductVariant{quantity=11,length="",size="S",color="Yellow",price=23.50,product=prod.First(c => c.id == 17)},
					 new ProductVariant{quantity=11,length="",size="M",color="Yellow",price=23.50,product=prod.First(c => c.id == 17)},
					 new ProductVariant{quantity=11,length="",size="L",color="Yellow",price=23.50,product=prod.First(c => c.id == 17)},
					 new ProductVariant{quantity=11,length="",size="XL",color="Yellow",price=23.50,product=prod.First(c => c.id == 17)},
					 new ProductVariant{quantity=11,length="",size="S",color="Green",price=23.50,product=prod.First(c => c.id == 17)},
					 new ProductVariant{quantity=11,length="",size="M",color="Green",price=23.50,product=prod.First(c => c.id == 17)},
					 new ProductVariant{quantity=11,length="",size="L",color="Green",price=23.50,product=prod.First(c => c.id == 17)},
					 new ProductVariant{quantity=11,length="",size="XL",color="Green",price=23.50,product=prod.First(c => c.id == 17)},

					 new ProductVariant{quantity=11,length="",size="S",color="Red",price=23.50,product=prod.First(c => c.id == 18)},
					 new ProductVariant{quantity=11,length="",size="M",color="Red",price=23.50,product=prod.First(c => c.id == 18)},
					 new ProductVariant{quantity=11,length="",size="L",color="Red",price=23.50,product=prod.First(c => c.id == 18)},
					 new ProductVariant{quantity=11,length="",size="XL",color="Red",price=23.50,product=prod.First(c => c.id == 18)},
					 new ProductVariant{quantity=11,length="",size="S",color="Blue",price=23.50,product=prod.First(c => c.id == 18)},
					 new ProductVariant{quantity=11,length="",size="M",color="Blue",price=23.50,product=prod.First(c => c.id == 18)},
					 new ProductVariant{quantity=11,length="",size="L",color="Blue",price=23.50,product=prod.First(c => c.id == 18)},
					 new ProductVariant{quantity=11,length="",size="XL",color="Blue",price=23.50,product=prod.First(c => c.id == 18)},
					 new ProductVariant{quantity=11,length="",size="S",color="Yellow",price=23.50,product=prod.First(c => c.id == 18)},
					 new ProductVariant{quantity=11,length="",size="M",color="Yellow",price=23.50,product=prod.First(c => c.id == 18)},
					 new ProductVariant{quantity=11,length="",size="L",color="Yellow",price=23.50,product=prod.First(c => c.id == 18)},
					 new ProductVariant{quantity=11,length="",size="XL",color="Yellow",price=23.50,product=prod.First(c => c.id == 18)},
					 new ProductVariant{quantity=11,length="",size="S",color="Green",price=23.50,product=prod.First(c => c.id == 18)},
					 new ProductVariant{quantity=11,length="",size="M",color="Green",price=23.50,product=prod.First(c => c.id == 18)},
					 new ProductVariant{quantity=11,length="",size="L",color="Green",price=23.50,product=prod.First(c => c.id == 18)},
					 new ProductVariant{quantity=11,length="",size="XL",color="Green",price=23.50,product=prod.First(c => c.id == 18)},

					 new ProductVariant{quantity=11,length="5 meter",size="",color="Red",price=23.50,product=prod.First(c => c.id == 19)},
					 new ProductVariant{quantity=11,length="5 meter",size="",color="Blue",price=23.50,product=prod.First(c => c.id == 19)},
					 new ProductVariant{quantity=11,length="5 meter",size="",color="Yellow",price=23.50,product=prod.First(c => c.id == 19)},
					 new ProductVariant{quantity=11,length="5 meter",size="",color="Green",price=23.50,product=prod.First(c => c.id == 19)},
					 new ProductVariant{quantity=11,length="5 meter",size="",color="Purple",price=23.50,product=prod.First(c => c.id == 19)},
					 new ProductVariant{quantity=11,length="5 meter" ,size="",color="Black",price=23.50,product=prod.First(c => c.id == 19)},
					 new ProductVariant{quantity=11,length="5 meter",size="",color="White",price=23.50,product=prod.First(c => c.id == 19)},
					 new ProductVariant{quantity=11,length="10 meter",size="",color="Red",price=23.50,product=prod.First(c => c.id == 19)},
					 new ProductVariant{quantity=11,length="10 meter",size="",color="Blue",price=23.50,product=prod.First(c => c.id == 19)},
					 new ProductVariant{quantity=11,length="10 meter",size="",color="Yellow",price=23.50,product=prod.First(c => c.id == 19)},
					 new ProductVariant{quantity=11,length="10 meter",size="",color="Green",price=23.50,product=prod.First(c => c.id == 19)},
					 new ProductVariant{quantity=11,length="10 meter",size="",color="Purple",price=23.50,product=prod.First(c => c.id == 19)},
					 new ProductVariant{quantity=11,length="10 meter",size="",color="Black",price=23.50,product=prod.First(c => c.id == 19)},
					 new ProductVariant{quantity=11,length="10 meter",size="",color="White",price=23.50,product=prod.First(c => c.id == 19)},

					 new ProductVariant{quantity=11,length="5 meter",size="",color="Red",price=23.50,product=prod.First(c => c.id == 20)},
					 new ProductVariant{quantity=11,length="5 meter",size="",color="Blue",price=23.50,product=prod.First(c => c.id == 20)},
					 new ProductVariant{quantity=11,length="5 meter",size="",color="Yellow",price=23.50,product=prod.First(c => c.id == 20)},
					 new ProductVariant{quantity=11,length="5 meter",size="",color="Green",price=23.50,product=prod.First(c => c.id == 20)},
					 new ProductVariant{quantity=11,length="5 meter",size="",color="Purple",price=23.50,product=prod.First(c => c.id == 20)},
					 new ProductVariant{quantity=11,length="5 meter" ,size="",color="Black",price=23.50,product=prod.First(c => c.id == 20)},
					 new ProductVariant{quantity=11,length="5 meter",size="",color="White",price=23.50,product=prod.First(c => c.id == 20)},
					 new ProductVariant{quantity=11,length="10 meter",size="",color="Red",price=23.50,product=prod.First(c => c.id == 20)},
					 new ProductVariant{quantity=11,length="10 meter",size="",color="Blue",price=23.50,product=prod.First(c => c.id == 20)},
					 new ProductVariant{quantity=11,length="10 meter",size="",color="Yellow",price=23.50,product=prod.First(c => c.id == 20)},
					 new ProductVariant{quantity=11,length="10 meter",size="",color="Green",price=23.50,product=prod.First(c => c.id == 20)},
					 new ProductVariant{quantity=11,length="10 meter",size="",color="Purple",price=23.50,product=prod.First(c => c.id == 20)},
					 new ProductVariant{quantity=11,length="10 meter",size="",color="Black",price=23.50,product=prod.First(c => c.id == 20)},
					 new ProductVariant{quantity=11,length="10 meter",size="",color="White",price=23.50,product=prod.First(c => c.id == 20)},

					  new ProductVariant{quantity=11,length="",size="S",color="Red",price=23.50,product=prod.First(c => c.id == 21)},
					 new ProductVariant{quantity=11,length="",size="M",color="Red",price=23.50,product=prod.First(c => c.id == 21)},
					 new ProductVariant{quantity=11,length="",size="L",color="Red",price=23.50,product=prod.First(c => c.id == 21)},
					 new ProductVariant{quantity=11,length="",size="XL",color="Red",price=23.50,product=prod.First(c => c.id == 21)},
					 new ProductVariant{quantity=11,length="",size="S",color="Blue",price=23.50,product=prod.First(c => c.id == 21)},
					 new ProductVariant{quantity=11,length="",size="M",color="Blue",price=23.50,product=prod.First(c => c.id == 21)},
					 new ProductVariant{quantity=11,length="",size="L",color="Blue",price=23.50,product=prod.First(c => c.id == 21)},
					 new ProductVariant{quantity=11,length="",size="XL",color="Blue",price=23.50,product=prod.First(c => c.id == 21)},
					 new ProductVariant{quantity=11,length="",size="S",color="Yellow",price=23.50,product=prod.First(c => c.id == 21)},
					 new ProductVariant{quantity=11,length="",size="M",color="Yellow",price=23.50,product=prod.First(c => c.id == 21)},
					 new ProductVariant{quantity=11,length="",size="L",color="Yellow",price=23.50,product=prod.First(c => c.id == 21)},
					 new ProductVariant{quantity=11,length="",size="XL",color="Yellow",price=23.50,product=prod.First(c => c.id == 21)},
					 new ProductVariant{quantity=11,length="",size="S",color="Green",price=23.50,product=prod.First(c => c.id == 21)},
					 new ProductVariant{quantity=11,length="",size="M",color="Green",price=23.50,product=prod.First(c => c.id == 21)},
					 new ProductVariant{quantity=11,length="",size="L",color="Green",price=23.50,product=prod.First(c => c.id == 21)},
					 new ProductVariant{quantity=11,length="",size="XL",color="Green",price=23.50,product=prod.First(c => c.id == 21)},

					 new ProductVariant{quantity=11,length="5 meter",size="",color="Red",price=23.50,product=prod.First(c => c.id == 22)},
					 new ProductVariant{quantity=11,length="5 meter",size="",color="Blue",price=23.50,product=prod.First(c => c.id == 22)},
					 new ProductVariant{quantity=11,length="5 meter",size="",color="Yellow",price=23.50,product=prod.First(c => c.id == 22)},
					 new ProductVariant{quantity=11,length="5 meter",size="",color="Green",price=23.50,product=prod.First(c => c.id == 22)},
					 new ProductVariant{quantity=11,length="5 meter",size="",color="Purple",price=23.50,product=prod.First(c => c.id == 22)},
					 new ProductVariant{quantity=11,length="5 meter" ,size="",color="Black",price=23.50,product=prod.First(c => c.id == 22)},
					 new ProductVariant{quantity=11,length="5 meter",size="",color="White",price=23.50,product=prod.First(c => c.id == 22)},
					 new ProductVariant{quantity=11,length="10 meter",size="",color="Red",price=23.50,product=prod.First(c => c.id == 22)},
					 new ProductVariant{quantity=11,length="10 meter",size="",color="Blue",price=23.50,product=prod.First(c => c.id == 22)},
					 new ProductVariant{quantity=11,length="10 meter",size="",color="Yellow",price=23.50,product=prod.First(c => c.id == 22)},
					 new ProductVariant{quantity=11,length="10 meter",size="",color="Green",price=23.50,product=prod.First(c => c.id == 22)},
					 new ProductVariant{quantity=11,length="10 meter",size="",color="Purple",price=23.50,product=prod.First(c => c.id == 22)},
					 new ProductVariant{quantity=11,length="10 meter",size="",color="Black",price=23.50,product=prod.First(c => c.id == 22)},
					 new ProductVariant{quantity=11,length="10 meter",size="",color="White",price=23.50,product=prod.First(c => c.id == 22)},

					 new ProductVariant{quantity=11,length="",size="S",color="Red",price=20.50,product=prod.First(c => c.id == 23)},
					 new ProductVariant{quantity=11,length="",size="M",color="Red",price=20.50,product=prod.First(c => c.id == 23)},
					 new ProductVariant{quantity=11,length="",size="L",color="Red",price=20.50,product=prod.First(c => c.id == 23)},
					 new ProductVariant{quantity=11,length="",size="S",color="Blue",price=20.50,product=prod.First(c => c.id == 23)},
					 new ProductVariant{quantity=11,length="",size="M",color="Blue",price=20.50,product=prod.First(c => c.id == 23)},
					 new ProductVariant{quantity=11,length="",size="L",color="Blue",price=20.50,product=prod.First(c => c.id == 23)},
					 new ProductVariant{quantity=11,length="",size="S",color="Yellow",price=20.50,product=prod.First(c => c.id == 23)},
					 new ProductVariant{quantity=11,length="",size="M",color="Yellow",price=20.50,product=prod.First(c => c.id == 23)},
					 new ProductVariant{quantity=11,length="",size="L",color="Yellow",price=20.50,product=prod.First(c => c.id == 23)},

				
			 	};
			_context.AddRange(mockProdVariation);
			await _context.SaveChangesAsync();

			List<Carts> objList = new List<Carts>();


			return Ok(new ApiResponseWrapper("Success", objList.ToArray()));

        }
		
		}
}
