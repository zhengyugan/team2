using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
	public class ProductVariant
	{
        [Key]
        public int id { get; set; }
        public int quantity { get; set; }
        public string size { get; set; }
        public string color { get; set; }
        public string length { get; set; }
        public double price { get; set; }
        [ForeignKey("product_id")]
        public required Products product { get; set; }
		public DateTime? deleted_at { get; set; }
		public int? deleted_by { get; set; }
		public DateTime? moodified_at { get; set; }
		public int? modified_by { get; set; }
		public DateTime created_at { get; set; }
		public int created_by { get; set; }
	}
}
