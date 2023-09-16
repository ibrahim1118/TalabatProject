using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using System.Text.RegularExpressions;
using Talabat.Core.IServices;

namespace Talabt.APIs.Helper
{
    public class ChashingAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int liveTile;

        public ChashingAttribute(int liveTile)
        {
            this.liveTile = liveTile;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var CachingService = context.HttpContext.RequestServices.GetRequiredService<ICashingService>();

            var key = GenreatKey(context.HttpContext.Request);

            var response = await CachingService.GetCashingData(key);

            if (!string.IsNullOrEmpty(response))
            {
                var contentResult = new ContentResult()
                {
                    Content = response,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contentResult;
                return;
            }
            var ExecutedEndPintRespons =  await next();

            if (ExecutedEndPintRespons.Result is OkObjectResult okObjectResult)
            {
                await CachingService.SetCashing(key, okObjectResult, TimeSpan.FromSeconds(liveTile)); 
            }

        }
        private string GenreatKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path);
            foreach (var (k ,v) in request.Query.OrderBy(x=>x.Key))
            {
                key.Append($"|{k}-{v}"); 
            }
            return key.ToString();
            
        }
    }

        
    
}
