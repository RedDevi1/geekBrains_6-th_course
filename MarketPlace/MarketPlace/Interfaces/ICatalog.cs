using MarketPlace.Models;

namespace MarketPlace.Interfaces
{
    public interface ICatalog<T> where T : class
    {
        IReadOnlyList<T> GetAll();
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
