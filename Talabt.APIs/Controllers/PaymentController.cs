using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Stripe;
using System.Text.RegularExpressions;
using Talabat.Core.Entitys;
using Talabat.Core.IServices;

namespace Talabt.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService paymentService;
        private const string endpointSecret = "whsec_bb5f2745e6ad6aa4658810b74080dc5a86232f8aded9a2d1405e9e1948179058";
        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
        { 
           var basket = await paymentService.CreatOrUpdatePaymentIntent(basketId);
           if (basket == null) {
                return BadRequest(); 
            }
           return Ok(basket);
        }
        [HttpPost("webhook")]
        public async Task<IActionResult> Stripewebhook()
        {

            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
           
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);

            var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
            if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
            {
                 await paymentService.UpdatePaymentIntendToSucceedOrFiled(paymentIntent.Id, true);
            }
            else if (stripeEvent.Type == Events.PaymentIntentSucceeded)
            {
               await paymentService.UpdatePaymentIntendToSucceedOrFiled(paymentIntent.Id, false);

            }
            return Ok();
            
            

        }

    }
}
