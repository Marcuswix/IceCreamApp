using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Share.Entities
{
    public class ProductImagesEntity
    {
        [Key, Column(Order = 0)]
        [ForeignKey(nameof(Product))]
        public string ArticleNumber { get; set; } = null!;
        public virtual ProductEntity Product { get; set; } = null!;

        [Key, Column(Order = 1)]
        [ForeignKey(nameof(Images))]
        public int ImageId { get; set; }
        public virtual ImagesEntity Images { get; set; } = null!;
    }
}

