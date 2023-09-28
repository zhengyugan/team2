using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
	public class Product
	{
        [Key]
        public int product_id { get; set; }
        public string  name { get; set; }
        public string description { get; set; }
        public string sku { get; set; }
        public int category_id { get; set; }
        public int inventory_id { get; set; }
        public int discount_id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime moodified_at { get; set; }
        public DateTime delete_at { get; set; }
    }
}
