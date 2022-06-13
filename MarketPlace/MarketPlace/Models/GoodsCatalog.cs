using MarketPlace.Interfaces;
using System.Collections.Concurrent;

namespace MarketPlace.Models
{
    public class GoodsCatalog : BaseEntity<int>, IGoodsCatalog
    {
        private readonly ConcurrentDictionary<int, Good> goodsCatalog = new();
        private readonly object _syncObj = new();

        public ConcurrentDictionary<int, Good> Goods { get => goodsCatalog; }

        public void Create(Good entity)
        {
            if (entity != null)
            {
                goodsCatalog.TryAdd(entity.Id, entity);
            }
        }
        public void Delete(long article)
        {
            if (goodsCatalog.Count > 0)
            {
                var removingGood = goodsCatalog.FirstOrDefault(t => t.Value.Article == article);
                goodsCatalog.TryRemove(removingGood.Key, out _);
            }
        }

        public ConcurrentDictionary<int, Good> GetAll()
        {
            return goodsCatalog;
        }

        public void Update(Good entity)
        {
            throw new NotImplementedException();
        }
    }
}
