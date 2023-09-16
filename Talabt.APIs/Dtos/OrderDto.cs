namespace Talabt.APIs.Dtos
{
    public class OrderDto
    {
        public int DeliveryMethodId { get; set; }   

        public AddreesDto ShippingAddrees { get; set; }

        public  string BasketId { get; set; }
    }
}
