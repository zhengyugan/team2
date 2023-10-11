using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using webapi.Models;

namespace webapi.Controllers
{
    public class OrderUpdateModel
    {
        public decimal? Total { get; set; }
        public int? ModifiedBy { get; set; }
    }

    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private readonly DataContext _context;

        public CartController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}", Name = "GetCartItem")]
        public IActionResult Get(int id)
        {
            //var cartItem = _context.carts.Where(cart => cart.user.id == id).ToList();

            var cartItem = (from carts in _context.carts
                            join product_variants in _context.product_variants on carts.product_variant.id equals product_variants.id
                            join products in _context.products on product_variants.product.id equals products.id
                            where carts.moodified_at == null
                            select new
                            {
                                products.url,
                                carts.id,
                                products.name,
                                product_variants.size,
                                product_variants.color,
                                product_variants.length,
                                product_variants.price,
                                carts.quantity,
                                carts.deleted_at,
                                carts.moodified_at
                            }).ToList();

            if (cartItem.Count > 0)
            {
                return Ok(new ApiResponseWrapper("", cartItem.ToArray()));
            }
            else
            {
                return NotFound(new ApiResponseWrapper("No items in cart!", cartItem.ToArray()));
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Carts cartItem)
        {
            if (cartItem == null)
            {
                return BadRequest();
            }

            _context.carts.Add(cartItem);
            _context.SaveChanges();

            return CreatedAtRoute("GetCartItem", new { id = cartItem.id }, cartItem);
        }

        [HttpPut("{id}")]
        public IActionResult Delete(int id)
        {
            var cartItem = _context.carts.Find(id);

            if (cartItem == null)
            {
                return NotFound();
            }

            //Update the deleted_at column
            cartItem.deleted_at = DateTime.UtcNow;
            //cartItem.deleted_by = userId;
            _context.SaveChanges();

            //Deleted successfully
            return NoContent();
        }

        [HttpPut("{id}/create-order")]
        public IActionResult CreateOrder(int userid, [FromBody] OrderUpdateModel orderUpdate)
        {
            var userList = _context.users.ToList();
            //var cartItem = _context.carts.Find(c => c.user.id = userid);
            //var cartList = _context.carts.Find(id);
            // Create a new order
            var newOrder = new Orders
            {
                //total = (float)orderUpdate.Total,
                //payment_status = "Completed",
                //order_status = "In progress",
                //modified_at = DateTime.UtcNow,
                //modified_by = orderUpdate.ModifiedBy

                users = userList.First(c => c.id == userid),
                total = (float)orderUpdate.Total,
                payment_status = "Completed",
                order_status = "In progress",
                moodified_at = DateTime.UtcNow,
                modified_by = orderUpdate.ModifiedBy
            };

            // Add the new order to the context
            _context.orders.Add(newOrder);
            //cartItem.moodified_at = DateTime.UtcNow;

            // Save changes to the database
            _context.SaveChanges();

            return Ok(newOrder);
        }

        [HttpGet("AddMockItem")]
        public async Task<IActionResult> AddMockItemAsync()
        {
            var prodVariantList = _context.product_variants.ToList();
            var userList = _context.users.ToList();

            var cart = new List<Carts>
            {
                new Carts 
                {
                user = userList.First(c => c.id == 3),
                product_variant = prodVariantList.First(c => c.id == 3),
                quantity = 5
                },

                new Carts
                {
                user = userList.First(c => c.id == 3),
                product_variant = prodVariantList.First(c => c.id == 1),
                quantity = 6
                },

                new Carts
                {
                user = userList.First(c => c.id == 3),
                product_variant = prodVariantList.First(c => c.id == 2),
                quantity = 1
                },
            };
            _context.carts.AddRange(cart);
                        
            await _context.SaveChangesAsync();
            List<Carts> objList = new List<Carts>();


            return Ok(new ApiResponseWrapper("Success", objList.ToArray()));

        }
    }
}
