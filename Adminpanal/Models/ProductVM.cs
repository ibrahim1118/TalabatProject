using Talabat.Core.Entitys;

namespace Adminpanal.Models
{
    public class ProductVM
    {
        public  int Id { get; set; }    
        public string Name { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public IFormFile? image { get; set; }
        public int ProductBrandId { get; set; }
        public ProductBrand? productBrand { get; set; }

        public int ProductTypeId { get; set; }
        public ProductType? productType { get; set; }
    }
}
