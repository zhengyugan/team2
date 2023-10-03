using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
	public class OrderItems
	{
        public int id { get; set; }
        [ForeignKey("order_id")]
        public required Orders order { get; set; }
        [ForeignKey("product_variant_id")]
        public required ProductVariant product_variant { get; set; }
        public int quantity { get; set; }
		public DateTime? deleted_at { get; set; }
		public int? deleted_by { get; set; }
		public DateTime? moodified_at { get; set; }
		public int? modified_by { get; set; }
		public DateTime created_at { get; set; }
		public int created_by { get; set; }
	}
}
