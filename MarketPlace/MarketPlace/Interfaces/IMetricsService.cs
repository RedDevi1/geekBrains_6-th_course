namespace MarketPlace.Interfaces
{
    public interface IMetricsService : IService
    {
        public string GoodsCreationPageUrl { get; set; }
        public string ProductPageUrl { get; set; }
        public string GoodsRemovingPageUrl { get; set; }
        public int GoodsCreationPageCount { get; }
        public int ProductPageCount { get; }
        public int GoodsRemovingPageCount { get; }
        public int GoodsCreationPageCountIncrement();
        public int GoodsRemovingPageCountIncrement();
        public int ProductPageCountIncrement();
    }
}
