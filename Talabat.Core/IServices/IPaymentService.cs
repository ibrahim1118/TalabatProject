using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entitys;
using Talabat.Core.Entitys.Order_Aggraget;

namespace Talabat.Core.IServices
{
    public interface IPaymentService
    {
        public Task<CustomerBasket> CreatOrUpdatePaymentIntent(string basketId);
        public Task<Order> UpdatePaymentIntendToSucceedOrFiled(string paymentIntendId, bool IsSucceeued); 
    }


}
