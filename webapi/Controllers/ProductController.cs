using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("{id}", Name = "getProduct")]
        public IActionResult getProduct(int id)
        {
            var product = _context.products.Where(product => product.id == id).ToList();

            if(product.Count > 0) 
            {
                return Ok(new ApiResponseWrapper("", product.ToArray()));
            }
            else
            {
                return NotFound(new ApiResponseWrapper("Product not found!", product.ToArray()));
            }
        }

        [HttpPost]
        public IActionResult storeProduct([FromBody] Products product)
        {
            _context.products.Add(product);
            _context.SaveChanges();

            List<Products> objList = new List<Products>();
            objList.Add(product);

            return Ok(new ApiResponseWrapper("", objList.ToArray()));
        }
    }
}
