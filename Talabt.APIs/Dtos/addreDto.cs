using System.ComponentModel.DataAnnotations;

namespace Talabt.APIs.Dtos
{
    public class AddreesDto
    {

        public int id { get; set; }
        [Required]
        public string FName { get; set; }
        [Required]
        public string LName { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Cuntry { get; set; }

    }
}
