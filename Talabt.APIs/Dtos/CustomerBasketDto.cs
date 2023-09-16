using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entitys;

namespace Talabt.APIs.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }

        public List<BasketItemDto> BasketItems { get; set; }

        public string? PaymentIntendId { get; set; }

        public string? CustomerSecrit { get; set; }

        public int? DeliverMethodId { get; set; }

        public decimal? SippingCost { get; set; }

        public CustomerBasketDto(string id)
        {
            Id = id;
        }
    }
}
