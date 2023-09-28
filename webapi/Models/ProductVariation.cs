using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
	public class ProductVariation
	{
        [Key]
        public int inventory_id { get; set; }
        public int quantity { get; set; }
        public string size { get; set; }

        public string color { get; set; }

        public string length { get; set; }

        public double price { get; set; }

        public string image_url { get; set; }
        public DateTime created_at  { get; set; }

        public DateTime modified_at { get; set; }
        public DateTime deleted_at { get; set; }
    }
}
