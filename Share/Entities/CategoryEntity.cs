using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Share.Entities
{
    //[Index(nameof(CategoryName), IsUnique = true)]
    public class CategoryEntity
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; } = null!;

        [ForeignKey(nameof(SubCategory))]
        public int SubCategoryId { get; set; }
        public virtual SubCategoryEntity SubCategory { get; set; } = null!;

        public virtual ICollection<ProductEntity> Products { get; set; } = new HashSet<ProductEntity>();
    }
}
