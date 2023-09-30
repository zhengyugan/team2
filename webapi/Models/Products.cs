using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
	public class Products
	{
        [Key]
        public int id { get; set; }
        public string  name { get; set; }
        public string desc { get; set; }
        public int product_category_id { get; set; }
        public string url { get; set; }
		public DateTime deleted_at { get; set; }
		public int deleted_by { get; set; }
		public DateTime moodified_at { get; set; }
		public int modified_by { get; set; }
		public DateTime created_at { get; set; }
		public int created_by { get; set; }
	}
}
