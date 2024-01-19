using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Entities
{
    public class CustomerEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string Email{ get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [ForeignKey(nameof(AddressEntity))]
        public int AddressId { get; set; }

        public AddressEntity Address { get; set; } = null!;
    }
}
