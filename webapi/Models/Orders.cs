using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
	public class Orders
	{
        public int id { get; set; }
        [ForeignKey("user_id")]
        public required Users users { get; set; }
        public float? total { get; set; }
        public int? payment_id { get; set; }
        public string payment_status { get; set; }
        public string order_status { get; set; }
		public DateTime? deleted_at { get; set; }
		public int? deleted_by { get; set; }
		public DateTime? moodified_at { get; set; }
		public string? modified_by { get; set; }
		public DateTime created_at { get; set; }
		public int created_by { get; set; }
	}
}
