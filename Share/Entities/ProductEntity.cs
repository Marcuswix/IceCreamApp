using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Share.Entities
{
    public class ProductEntity
    {
        [Key]
        public string ArticleNumber { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string? SpecificationAsJson { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int? ProductImageId { get; set; }
        public virtual ICollection<ProductImagesEntity>? ProductImageUrl { get; set; } = new List<ProductImagesEntity>();

        [Required]
        [ForeignKey(nameof(CategoryEntity))]
        public int CategoryId { get; set; }
        public virtual CategoryEntity Category { get; set; } = null!;

        [ForeignKey(nameof(Manufacturer))]
        public int ManufacturerId {  get; set; }
        public virtual ManufacturerEntity Manufacturer { get; set; } = null!;
    }
}
