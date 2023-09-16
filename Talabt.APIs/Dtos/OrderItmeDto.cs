using Talabat.Core.Entitys.Order_Aggraget;

namespace Talabt.APIs.Dtos
{
    public class OrderItmeDto
    {
        public int Id { get; set; }

        public int ProdcutId { get; set; }

        public string ProdcutName { get; set; }
        public string ImageUrl { get; set; }
        public int Count { get; set; }

        public decimal Price { get; set; }

    }
}