using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Share.Entities
{
    //[Index(nameof(ManufacturerName), IsUnique = true)]
    public class ManufacturerEntity
    {
        [Key]
        public int ManufacturerId { get; set; }

        [Required]
        public string ManufacturerName { get; set; } = null!;

        public virtual ICollection<ProductEntity> Products { get; set; } = new HashSet<ProductEntity>();
    }
}
