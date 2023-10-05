using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace webapi
{
    public class PaginatedResponse<T>
    {
        public PaginatedResponse(IEnumerable<T> data, int i, int len)
        {
            Data = data?.Skip((i - 1) * len).Take(len).ToList();
            Total = (int)((data != null && data.Any()) ? (data?.Count()) : 0);
        }

        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}