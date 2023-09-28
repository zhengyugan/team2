using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
	public class Cart
	{
        [Key]
        public int cart_id { get; set; }
        public int user_id { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
        public DateTime created_at { get; set; }
        public DateTime modified_at { get; set; }
    }
}
