using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace webapi.Models
{
    public class ProductVariant
    {
        [Key]
        public int id { get; set; }
        public int quantity { get; set; }
        public string? size { get; set; }
        public string color { get; set; }
        public string? length { get; set; }
        public double price { get; set; }
        [ForeignKey("product_id")]
        public Products product { get; set; } = null!; // Required reference navigation to principal
        public DateTime? deleted_at { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? moodified_at { get; set; }
        public int? modified_by { get; set; }
        public DateTime created_at { get; set; }
        public int created_by { get; set; }
    }
}
