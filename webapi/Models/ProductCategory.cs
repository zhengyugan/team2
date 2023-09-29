using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
	public class ProductCategory
	{

        [Key]
        public int category_id { get; set; }
        public string category_name { get; set; }
        public string category_description { get; set; }
        public DateTime delete_at { get; set; }
        public DateTime moodified_at { get; set; }
        public DateTime created_at { get; set; }
    }
}
