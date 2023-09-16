using Talabat.Core.Entitys.Order_Aggraget;

namespace Talabt.APIs.Dtos
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } 

        public string OrderStatus { get; set; } 

        public Address ShaippingAddress { get; set; }

        public string deliverMethod { get; set; }

        public decimal Cost { get; set; }

        public ICollection<OrderItmeDto> Itmes { get; set; } 

        public decimal SubTotal { get; set; }

        public decimal GetTotal => SubTotal + Cost;

        public string PaymentIntentId { get; set; } 

    }
}
