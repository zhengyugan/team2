using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
	public class ProductVariation
	{
        [Key]
        public int inventory_id { get; set; }
        public int inventory_quantity { get; set; }
        public string inventory_size { get; set; }

        public string inventory_color { get; set; }

        public string inventory_length { get; set; }

        public double inventory_price { get; set; }

        public string inventory_image_url { get; set; }
        public DateTime created_at  { get; set; }

        public DateTime modified_at { get; set; }
        public DateTime deleted_at { get; set; }
    }
}
