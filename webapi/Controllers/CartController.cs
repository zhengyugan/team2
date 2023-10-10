using Microsoft.AspNetCore.Mvc;
using webapi.Models;

namespace webapi.Controllers
{
    public class OrderUpdateModel
    {
        public decimal? Total { get; set; }
        public int? PaymentId { get; set; }
        public string? ModifiedBy { get; set; }
    }

    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private readonly DataContext _context;

        public CartController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _context.carts.OrderBy(c => c.id);
            return Ok(data);
        }

        [HttpGet("{id}", Name = "GetCartItem")]
        public IActionResult Get(int id)
        {
            var cartItem = _context.carts.Find(id);
            return Ok(cartItem);
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

        [HttpGet("{id}/productvariant", Name = "GetProductVariantDetails")]
        public IActionResult GetProductVariantDetails(int id)
        {
            var cartItem = _context.carts.Find(id);

            if (cartItem == null)
            {
                return NotFound();
            }

            var productVariantId = cartItem.product_variant.id;
            var productVariant = _context.product_variants.FirstOrDefault(pv => pv.id == productVariantId);

            if (productVariant == null)
            {
                return NotFound("Product variant not found.");
            }

            var productVariantDetails = new
            {
                Color = productVariant.color,
                Size = productVariant.size,
                Length = productVariant.length
            };

            return Ok(productVariantDetails);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cartItem = _context.carts.Find(id);

            if (cartItem == null)
            {
                return NotFound();
            }

            //Update the deleted_at column
            cartItem.deleted_at = DateTime.UtcNow;
            _context.SaveChanges();

            //Deleted successfully
            return NoContent();
        }

        [HttpPut("{id}/update-order")]
        public IActionResult UpdateOrder(int id, [FromBody] OrderUpdateModel orderUpdate)
        {
            var order = _context.orders.Find(id);

            if (order == null)
            {
                return NotFound();
            }

            // Update the order properties
            order.total = (float)orderUpdate.Total;
            order.payment_id = orderUpdate.PaymentId;
            order.payment_status = "completed";
            order.order_status = "in progress";
            order.moodified_at = DateTime.UtcNow;
            order.modified_by = orderUpdate.ModifiedBy;

            _context.SaveChanges();

            return Ok(order);
        }

    }
}
