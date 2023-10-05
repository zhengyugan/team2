using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly DataContext _context;

        public OrderController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{pageIndex:int}/{pageSize:int}")]
        public IActionResult Get(int pageIndex, int pageSize)
        {
            var data = _context.orders?.Include(o => o.users)
                .OrderByDescending(c => c.created_at);

            var page = new PaginatedResponse<Orders>(data, pageIndex, pageSize);
            
            var totalCount = (int)((data != null && data.Any()) ? (data?.Count()) : 0);
            var totalPages = Math.Ceiling((double)totalCount / pageSize);

            var response = new
            {
                Page = page,
                TotalPages = totalPages
            };

            return Ok(response);
        }

        /*[HttpGet("ByState")]
        public IActionResult ByState()
        {
            var orders = _context.orders.Include(o => o.users).ToList();

            var groupedResult = orders.GroupBy(o => o.users.State)
                .ToList()
                .Select(grp => new
                {
                    State = grp.Key,
                    Total = grp.Sum(x => x.Total)
                }).OrderByDescending(res => res.Total)
                .ToList();

            return Ok(groupedResult);
        }*/

        [HttpGet("ByCustomer/{n}")]
        public IActionResult ByCustomer(int n)
        {
            var orders = _context.orders?.Include(o => o.users).ToList();

            var groupedResult = orders?.GroupBy(o => o.users.id)
                .ToList()
                .Select(grp => new
                {
                    Name = $"{_context.users?.Find(grp.Key)?.last_name} {_context.users?.Find(grp.Key)?.first_name}",
                    Total = grp.Sum(x => x.total)
                }).OrderByDescending(res => res.Total)
                .Take(n)
                .ToList();

            return Ok(groupedResult);
        }

        [HttpGet("GetOrder/{id}", Name = "GetOrder")]
        public IActionResult GetOrder(int id)
        {
            var order = _context.orders.Include(o => o.users)
                .First(o => o.id == id);

            return Ok(order);
        }
    }
}
