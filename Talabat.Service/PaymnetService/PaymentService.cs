using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entitys;
using Talabat.Core.Entitys.Order_Aggraget;
using Talabat.Core.IRepositiry;
using Talabat.Core.IServices;
using Talabat.Core.Spcifications;
using Product = Talabat.Core.Entitys.Product;

namespace Talabat.Service.PaymnetService
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepositiry basketRepositiry;
        private readonly IUnitOfWork unitOfWork;

        public PaymentService(IConfiguration configuration
            ,IBasketRepositiry basketRepositiry,
            IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            this.basketRepositiry = basketRepositiry;
            this.unitOfWork = unitOfWork;
        }
        public async Task<CustomerBasket> CreatOrUpdatePaymentIntent(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripeSetting:Secretkey"]; 
            
            var basket = await basketRepositiry.GetBasket(basketId); 
            
            if (basket == null)
            {
                return null; 
            }
            var sippingConst = 0m;
            if (basket.DeliverMethodId.HasValue)
            {
                var deliverMethod = await unitOfWork.Repostitry<DliveryMethod>().GetByIdAysnc(basket.DeliverMethodId.Value);
                basket.SippingCost = deliverMethod.Cost; 
                sippingConst= deliverMethod.Cost;
            }

            if (basket?.BasketItems?.Count> 0) 
            {
                foreach (var item in basket.BasketItems)
                {
                    var product = await unitOfWork.Repostitry<Product>().GetByIdAysnc(item.Id);
                    if (product.Price!=item.Price)
                        item.Price = product.Price;
                }
            }
            PaymentIntent paymentIntent; 

           var service = new  PaymentIntentService();
            if (string.IsNullOrEmpty(basket.PaymentIntendId))
            {
                var option = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.BasketItems.Sum(it => it.Price * it.NumberOfItme * 100) + (long)sippingConst,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card"}
                };
                paymentIntent = await service.CreateAsync(option);
                basket.PaymentIntendId = paymentIntent.Id;
                basket.CustomerSecrit = paymentIntent.ClientSecret; 
            }
            else
            {
                var option = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.BasketItems.Sum(it => it.Price * it.NumberOfItme * 100) + (long)sippingConst
                };
                await service.UpdateAsync(basket.PaymentIntendId , option);
            }
            await basketRepositiry.UpdataOrCreae(basket); 

            return basket;

        }

        public async Task<Order> UpdatePaymentIntendToSucceedOrFiled(string paymentIntendId, bool IsSucceeued)
        {
            var spac = new OrderSpacification(paymentIntendId); 
            var order =await unitOfWork.Repostitry<Order>().GetByIdSpacAysnc(spac);

            if (IsSucceeued)
            {
                order.OrderStatus = OrderStatus.paymentReceived; 
            }
            else
            {
                order.OrderStatus= OrderStatus.paymentFieled;
            }
            await unitOfWork.Repostitry<Order>().Update(order);

            await unitOfWork.IsComplet(); 
            return order;

        }
    }
}
