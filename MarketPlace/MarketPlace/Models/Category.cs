namespace MarketPlace.Models
{
    public class Category : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Discount { get; set; }
    }
}
