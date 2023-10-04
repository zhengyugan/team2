using Microsoft.AspNetCore.Mvc;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly DataContext _context;

        public CustomerController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = _context.users.OrderBy(c => c.id);
            return Ok(data);
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get(int id)
        {
            var customer = _context.users.Find(id);
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Users user)
        {
            if(user == null)
            {
                return BadRequest();
            }

            _context.users.Add(user);
            _context.SaveChanges();

            return CreatedAtRoute("GetCustomer", new { id = user.id }, user);
        }
    }
}
