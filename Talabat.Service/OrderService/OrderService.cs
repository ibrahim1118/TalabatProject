using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entitys;
using Talabat.Core.Entitys.Order_Aggraget;
using Talabat.Core.IRepositiry;
using Talabat.Core.IServices;
using Talabat.Core.Spcifications;

namespace Talabat.Service.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepositiry basketRepositiry;
        private readonly IUnitOfWork unitOfWork;

        public OrderService(IBasketRepositiry basketRepositiry,
            IUnitOfWork unitOfWork
            )
        {
            this.basketRepositiry = basketRepositiry;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliverMthodid, Address address)
        {
            //Get Items Basket
            var basket = await basketRepositiry.GetBasket(BasketId);
          
            var orderItems = new List<OrderItme>();
            decimal TotalPric = 0; 
            
            if (basket?.BasketItems?.Count > 0)
            {
                foreach (var item in basket.BasketItems)
                {
                    var product = await unitOfWork.Repostitry<Product>().GetByIdAysnc(item.Id);

                    var productItmeOrder = new ProductItmeOrder(product.Id, product.Name, product.ImageUrl);

                    var ordreItem = new OrderItme(productItmeOrder, item.NumberOfItme, product.Price); 
                     
                    orderItems.Add(ordreItem);
                    TotalPric+= (ordreItem.Price* ordreItem.Count);
                }
            }
            var deliverMthode = await unitOfWork.Repostitry<DliveryMethod>().GetByIdAysnc(DeliverMthodid);
            var spac = new OrderSpacification(basket.PaymentIntendId); 
            var existingorder = await unitOfWork.Repostitry<Order>().GetByIdSpacAysnc(spac);
            if (existingorder != null)
            {
                await unitOfWork.Repostitry<Order>().Delete(existingorder);
            }
            var order = new Order(BuyerEmail, address, deliverMthode, orderItems, TotalPric,basket.PaymentIntendId);
            await unitOfWork.Repostitry<Order>().AddAysnc(order);
            int res = await unitOfWork.IsComplet();
            if (res == 0)
                return null;
            return order;   
        }

        public async Task<Order> GetOrderByIdforUser(string Email, int OrderId)
        {
            var spac = new OrderSpacification(Email, OrderId);
            var order = await unitOfWork.Repostitry<Order>().GetByIdSpacAysnc(spac);
            return order; 
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUser(string Email)
        {
            var spac = new  OrderSpacification(Email);
            var order = await unitOfWork.Repostitry<Order>().GetSpacAllAysnc(spac);
            return order;
        }
    }
}
