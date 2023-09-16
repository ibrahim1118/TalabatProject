using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entitys.Order_Aggraget;

namespace Talabat.Core.IServices
{
    public interface IOrderService
    {
        public Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliverMthodid, Address address);

        public Task<Order> GetOrderByIdforUser(string Email, int OrderId);

        public Task<IReadOnlyList<Order>> GetOrdersForUser(string Email); 

    }
}
