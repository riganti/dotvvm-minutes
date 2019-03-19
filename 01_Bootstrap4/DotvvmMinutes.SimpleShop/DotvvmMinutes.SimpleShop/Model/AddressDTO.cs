using System.ComponentModel.DataAnnotations;

namespace DotvvmMinutes.SimpleShop.Model
{
    public class AddressDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        public string Address2 { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Zip { get; set; }

    }
}