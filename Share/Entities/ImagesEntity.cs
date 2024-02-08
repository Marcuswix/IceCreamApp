using System.ComponentModel.DataAnnotations;

namespace Share.Entities
{
    public class ImagesEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; } = null!;

        // Navigation property
        public virtual ICollection<ProductImagesEntity> ProductImages { get; set; } = new List<ProductImagesEntity>();
    }
}

