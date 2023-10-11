using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
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

        [HttpGet("ByDate/{startDate}/{endDate}")]
        public IActionResult ByDate(string startDate, string endDate)
        {
            DateTime start;
            DateTime end;

            if (!DateTime.TryParseExact(startDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out start))
                return BadRequest(new ApiResponseWrapper($"'{startDate}' is not in an acceptable  Date value/format.", new object[0]));

            if (!DateTime.TryParseExact(endDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out end))
                return BadRequest(new ApiResponseWrapper($"'{endDate}' is not in an acceptable Date value/format.", new object[0]));

            var query = Enumerable.Range(0, (end - start).Days + 1)
                .Select(date => start.AddDays(date))
                .GroupJoin(_context.orders.Where(order => order.order_status == "completed"),
                    date_list => date_list.ToShortDateString(),
                    order => order.created_at!.ToShortDateString(),
                    (date_list, order_group) => new
                    {
                        Date = date_list,
                        Total = order_group.Any() ? order_group.Sum(order => order.total) : 0
                    }).ToList();

            return Ok(new ApiResponseWrapper("", query.ToArray()));
        }

        [HttpGet("GetOrder/{id}", Name = "GetOrder")]
        public IActionResult GetOrder(int id)
        {
            var order = _context.orders.Include(o => o.users)
                .First(o => o.id == id);

            return Ok(order);
        }

        [HttpGet("Latest")]
        public async Task<IActionResult> LatestAsync()
        {
            var query = await _context.orders.Include(order => order.users)
                .Where(order => order.order_status == "Completed")
                .OrderByDescending(order => order.created_at)
                .FirstOrDefaultAsync();
            
            if (query == null)
            {
                return NotFound(new ApiResponseWrapper("No record found.", new object[0]));
            }

            List<Orders> latestRecordList = new List<Orders> { query };

            return Ok(new ApiResponseWrapper("", latestRecordList.ToArray()));
        }
    }
}
