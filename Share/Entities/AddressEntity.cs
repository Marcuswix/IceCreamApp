using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Entities
{
    public class AddressEntity
    {
        [Key]
        public int AddressId {  get; set; }

        [Required]
        public string SteetName { get; set; } = null!;

        [Required]
        public int PostalCode { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; } = null!;

        public virtual ICollection<CustomerEntity> Customer { get; set; } = null!;
    }
}
