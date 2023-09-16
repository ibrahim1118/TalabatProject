using System.Net;
using System.Text.Json;

namespace Talabt.APIs.Erorr
{
    public class ExceptionMiddelWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddelWare> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddelWare(RequestDelegate next , ILogger<ExceptionMiddelWare> logger , IHostEnvironment env )
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task Invoke(HttpContext  context)
        {
            try
            {
                await next.Invoke(context); 
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                context.Response.ContentType = "Application/Json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                var respons = env.IsDevelopment() ? new ExcepationRespons((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()) :
                    new ExcepationRespons((int)HttpStatusCode.InternalServerError);

                var json = JsonSerializer.Serialize(respons);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
