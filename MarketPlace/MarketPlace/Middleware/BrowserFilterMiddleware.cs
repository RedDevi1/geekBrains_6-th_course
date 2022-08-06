namespace MarketPlace.Middleware
{
    public class BrowserFilterMiddleware
    {
        private readonly RequestDelegate _next;

        public BrowserFilterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (!context.Request.Headers.UserAgent.ToString().ToLower().Contains("edge"))
            {
                await context.Response.WriteAsync("Your browser is not supported. Please use the Edge browser");
                return;
            }
            await _next(context);
        }
    }
}
