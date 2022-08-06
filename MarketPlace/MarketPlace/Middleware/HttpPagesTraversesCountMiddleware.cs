using MarketPlace.Domain.Services;
using MarketPlace.Interfaces;

namespace MarketPlace.Middleware
{
    public class HttpPagesTraversesCountMiddleware
    {
        private readonly RequestDelegate _next;
        public HttpPagesTraversesCountMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IMetricsService service)
        {
            if (context.Request.Path.ToString().Contains("GoodsCreation"))
            { 
                service.GoodsCreationPageUrl = context.Request.Path.ToString();
                service.GoodsCreationPageCountIncrement(); 
            }

            if (context.Request.Path.ToString().Contains("Product"))
            { 
                service.ProductPageUrl = context.Request.Path.ToString();
                service.ProductPageCountIncrement(); 
            }

            if (context.Request.Path.ToString().Contains("GoodsRemoving"))
            { 
                service.GoodsRemovingPageUrl = context.Request.Path.ToString();
                service.GoodsRemovingPageCountIncrement(); 
            }

            await _next(context);
        }
    }
}
