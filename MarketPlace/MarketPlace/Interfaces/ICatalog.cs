using MarketPlace.Models;
using System.Collections.Concurrent;

namespace MarketPlace.Interfaces
{
    public interface ICatalog<T> where T : class
    {
        ConcurrentDictionary<int, T> GetAll();
        void Create(T entity, CancellationToken cancellationToken);
        void Update(T entity, CancellationToken cancellationToken);
        void Delete(long article, CancellationToken cancellationToken);
    }
}
