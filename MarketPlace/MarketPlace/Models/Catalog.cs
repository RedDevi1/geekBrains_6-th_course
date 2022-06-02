namespace MarketPlace.Models
{
    public class Catalog : BaseEntity<int>
    {
        public List<Good> Goods { get; set; } = new();
    }
}
