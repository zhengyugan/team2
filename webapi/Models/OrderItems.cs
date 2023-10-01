namespace webapi.Models
{
	public class OrderItems
	{
        public int id { get; set; }
        public int order_id { get; set; }
        public int product_variant_id { get; set; }
        public int quantity { get; set; }
		public DateTime? deleted_at { get; set; }
		public int? deleted_by { get; set; }
		public DateTime? moodified_at { get; set; }
		public int? modified_by { get; set; }
		public DateTime created_at { get; set; }
		public int created_by { get; set; }
	}
}
