using System.ComponentModel.DataAnnotations;

namespace Talabt.APIs.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int NumberOfItme { get; set; }
        [Required]
        [Range(0.1, double.MaxValue)]    
        public decimal Price { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
    }
}