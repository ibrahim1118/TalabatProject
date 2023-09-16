using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entitys
{
    public class CustomerBasket
    {
        public string Id { get; set; }

        public List<BasketItem> BasketItems { get; set; }

        public  string PaymentIntendId { get; set; }

        public  string CustomerSecrit { get; set; } 

        public int? DeliverMethodId { get; set; }

        public decimal? SippingCost { get; set; }
        public CustomerBasket(string id) 
        {
            Id = id; 
        }
    }
}
