namespace MarketPlace.Middleware
{
    public class HttpPagesTraversesCountMiddleware
    {
        private readonly RequestDelegate _next;
        private int goodsCreationPageCount;
        private int productPageCount;
        private int goodsRemovingPageCount;

        public int GoodsCreationPageCount { get => goodsCreationPageCount; }
        public int ProductPageCount { get => productPageCount; }
        public int GoodsRemovingPageCount { get => goodsRemovingPageCount; }

        public HttpPagesTraversesCountMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.ToString().Contains("GoodsCreation"))
                Interlocked.Increment(ref goodsCreationPageCount);

            if (context.Request.Path.ToString().Contains("Product"))
                Interlocked.Increment(ref productPageCount);

            if (context.Request.Path.ToString().Contains("GoodsRemoving"))
                Interlocked.Increment(ref goodsRemovingPageCount);

            await _next(context);
        }
    }
}
