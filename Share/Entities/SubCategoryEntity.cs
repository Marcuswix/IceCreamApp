using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Share.Entities
{
    public class SubCategoryEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string SubcategoryName { get; set; } = null!;

        public virtual ICollection<CategoryEntity> Category { get; set; } = new HashSet<CategoryEntity>();
    }
}
