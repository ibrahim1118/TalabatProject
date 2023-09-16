using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entitys.Order_Aggraget;

namespace Talabat.Core.Spcifications
{
    public class OrderSpacification : Spacification<Order>
    {
        public OrderSpacification(string Emil , int? id=null):
            base(o=>(o.BuyerEmail==Emil)
             &&(!id.HasValue||id==o.Id))
        {
            Includes.Add(o=>o.DliveryMethod);
            Includes.Add(o => o.Itmes);
            OrderBy = o => o.OrderDate; 
        }

        public OrderSpacification(string paymentId):base(o=>o.PaymentIntentId==paymentId)
        {
            Includes.Add(o => o.DliveryMethod);
            Includes.Add(o => o.Itmes);
        }
    }
}
