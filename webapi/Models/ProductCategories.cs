using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
	public class ProductCategories
	{

        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
		public DateTime? deleted_at { get; set; }
		public int? deleted_by { get; set; }
		public DateTime? moodified_at { get; set; }
		public int? modified_by { get; set; }
		public DateTime created_at { get; set; }
		public int created_by { get; set; }
	}
}
