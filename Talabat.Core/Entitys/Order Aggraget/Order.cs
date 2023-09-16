using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entitys.Order_Aggraget
{
    public class Order : BaseEntity
    {
        public Order()
        {

        }
        public Order(string buyerEmail, Address shaippingAddress, DliveryMethod dliveryMethod, ICollection<OrderItme> itmes, decimal subTotal, string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            PaymentIntentId = paymentIntentId;
            ShaippingAddress = shaippingAddress;
            DliveryMethod = dliveryMethod;
            Itmes = itmes;
            SubTotal = subTotal;
        }

        public string BuyerEmail { get; set; }

        public  DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending; 

        public Address ShaippingAddress { get; set; } 
        public int? DliveryMethodId { get; set; }
        public  DliveryMethod DliveryMethod { get; set; }

        public  ICollection<OrderItme> Itmes { get; set; }   = new HashSet<OrderItme>();


        public  decimal SubTotal { get; set; }

        public decimal GetTotal => SubTotal+DliveryMethod.Cost;

        public string PaymentIntentId { get; set; }  

    }
}
