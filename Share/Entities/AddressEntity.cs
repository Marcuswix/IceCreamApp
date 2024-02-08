using System.ComponentModel.DataAnnotations;

namespace Share.Entities
{
    public class AddressEntity
    {
        [Key]
        public int AddressId {  get; set; }

        [Required]
        public string StreetName { get; set; } = null!;

        [Required]
        public int PostalCode { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; } = null!;

        public virtual ICollection<CustomerEntity> Customer { get; set; } = null!;
    }
}
