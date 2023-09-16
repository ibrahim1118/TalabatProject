using System.ComponentModel.DataAnnotations;

namespace Talabt.APIs.Dtos
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public  string Email { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public  string DispalyName { get; set; }
        [Required]
        public  string PhoneNumber { get; set; }
    }
}
