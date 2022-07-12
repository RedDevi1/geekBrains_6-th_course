using MarketPlace.DomainEvents;
using MarketPlace.Models;

namespace MarketPlace.Domain
{
    public class ProductAdded : IDomainEvent
    {
        public Good Good { get; }

        public ProductAdded(Good good)
        {
            Good = good;
        }
    }
}
