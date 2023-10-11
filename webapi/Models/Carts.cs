using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
	public class Carts
	{
        [Key]
        public int id { get; set; }
        [ForeignKey("user_id")]
        public required Users user { get; set; }
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
