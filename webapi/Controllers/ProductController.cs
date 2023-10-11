using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using webapi.Migrations;
using Microsoft.EntityFrameworkCore;
using webapi.Models;
using Newtonsoft.Json;

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
				ToList();

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

        [HttpPost("StoreProduct")]
        public IActionResult storeProduct([FromBody] dynamic product)
        {
            Console.WriteLine("before");
            Console.WriteLine(product);
            Console.WriteLine("HEllo");

			var productCat = 1;
			if (product.productCategoryId == 1)
			{
				productCat = 1;
			}
			else
			{
				productCat = 2;
			}

            var product_category = _context.product_categories.First(
            	product_category_ => product_category_.id == productCat);

            Products productStore = new Products
			{
				product_category = product_category,
				name = product.name,
				desc = product.description,
				url = "product.url",
				sizing_type = product.sizingType,
				created_at = DateTime.Now,
				created_by = 1,
			};
			_context.products.Add(productStore);
			_context.SaveChangesAsync();

            Console.WriteLine("before");
            Console.WriteLine(productStore.ToString());
            Console.WriteLine("after");

   //         ProductVariant productVariants = new ProductVariant
			//{
				
			//	quantity = product.quantity,
			//	size = product.size,
			//	color = product.color,
			//	length = product.length,
			//	price = product.price,
			//	product = 

			//};
			
			
            String[] values = new String[] { "AYU", "TEST" };

            return Ok(new ApiResponseWrapper("", values));
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

		
		[HttpPost("AddItem")]
		public async Task<IActionResult> AddItemAsync([FromBody] CartInfo cartInfo)
		{
			var prodVariantList = _context.product_variants.ToList();
			var userList = _context.users.ToList();

			// new Carts { 
			// 	user = userList.First(c=>c.id == cartInfo.user_id),
			// 	product_variant = prodVariantList.First(c => c.id == cartInfo.product_variant_id),
			// }
			//_context.carts.Add(cart);
			await _context.SaveChangesAsync();

			//var result = _context.product_variants.First(prod => prod.id == cart.id);

			List<Carts> objList = new List<Carts>();

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
    }
}
