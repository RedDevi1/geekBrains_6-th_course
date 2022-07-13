using MarketPlace.Interfaces;

namespace MarketPlace.Domain.Services
{
    public class MetricsService : IMetricsService
    {
        private int goodsCreationPageCount;
        private int productPageCount;
        private int goodsRemovingPageCount;
        public string GoodsCreationPageUrl { get; set; } = "";
        public string ProductPageUrl { get; set; } = "";
        public string GoodsRemovingPageUrl { get; set; } = "";
        public int GoodsCreationPageCount { get => goodsCreationPageCount; }
        public int ProductPageCount { get => productPageCount; }
        public int GoodsRemovingPageCount { get => goodsRemovingPageCount; }

        public int GoodsCreationPageCountIncrement()
        {
            return Interlocked.Increment(ref goodsCreationPageCount);
        }
        public int GoodsRemovingPageCountIncrement()
        {
            return Interlocked.Increment(ref goodsRemovingPageCount);
        }
        public int ProductPageCountIncrement()
        {
            return Interlocked.Increment(ref productPageCount);
        }
    }
}
