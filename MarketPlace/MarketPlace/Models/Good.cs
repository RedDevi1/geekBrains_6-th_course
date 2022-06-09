namespace MarketPlace.Models
{
    public class Good : BaseEntity<int>
    {
        public string? Name { get; set; }
        public long Article { get; set; }
        public int BrandsId { get; set; }
        public int TypeOfGoodsId { get; set; }
        public int CategoriesId { get; set; }
        public double Price { get; set; }

    }
}
