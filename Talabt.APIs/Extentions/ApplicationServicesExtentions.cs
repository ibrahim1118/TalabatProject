using Microsoft.AspNetCore.Mvc;
using Talabat.Core.IRepositiry;
using Talabat.Core.IServices;
using Talabat.Repositiry.Repositiry;
using Talabat.Service;
using Talabat.Service.OrderService;
using Talabat.Service.PaymnetService;
using Talabt.APIs.Erorr;
using Talabt.APIs.Helper;

namespace Talabt.APIs.Extentions
{
    public static class ApplicationServicesExtentions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services) 
        {
           /* services.AddScoped(typeof(IBaseRepositiry<>), typeof(BaseRepositiry<>));*/
            services.AddAutoMapper(o => o.AddProfile(new MappingProFiles()));
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddSingleton<ICashingService, CashingService>();

            services.AddScoped<IOrderService ,OrderService>();  
            // Valdtion Erorr
            services.Configure<ApiBehaviorOptions>(option =>
              option.InvalidModelStateResponseFactory = (actionContext) =>
              {
                  var err = actionContext.ModelState.Where(m => m.Value.Errors.Count > 0)
                  .SelectMany(e => e.Value.Errors).Select(e => e.ErrorMessage).ToList();

                  var ValdationErorr = new VialdtionErorrResponse() { Erorrs = err };

                  return new BadRequestObjectResult(ValdationErorr);
              });

            services.AddCors(Option =>
            {
                Option.AddPolicy("MyPlicy", option => {
                    option.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();


                 }
                ) ;
            });
            return services;
        }
    }
}
