using MarketPlace.Models;
using System.Collections.Concurrent;

namespace MarketPlace.Interfaces
{
    public interface ICatalog<T> where T : class
    {
        ConcurrentDictionary<int, T> GetAll();
        void Create(T entity);
        void Update(T entity);
        void Delete(long article);
    }
}
