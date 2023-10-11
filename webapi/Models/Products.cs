using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models
{
    public class Products
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        [ForeignKey("product_category_id")]
        public required ProductCategories product_category { get; set; }
        public ICollection<ProductVariant> product_variants { get; set; } = new List<ProductVariant>(); // Collection navigation containing dependents
        public string url { get; set; }
        public string sizing_type { get; set; }
        public DateTime? deleted_at { get; set; }
        public int? deleted_by { get; set; }
        public DateTime? moodified_at { get; set; }
        public int? modified_by { get; set; }
        public DateTime created_at { get; set; }
        public int created_by { get; set; }
    }

    public class StoreProductDto
    {
        public int id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public double Price { get; set; }
        public int ProductCategoryId { get; set; }
        public String SizingType { get; set; }
        public List<ProductVariantDto> ProductVariants { get; set; }
    }

    public class ProductVariantDto
    {
        public int ProductVariantId{ get; set; }
        public String Color { get; set; }
        public String? Length { get; set; }
        public String? Size { get; set; }
        public int Quantity { get; set; }
    }

}
