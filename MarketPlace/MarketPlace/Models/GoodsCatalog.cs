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

                try
                {
                    goodsCatalog.TryAdd(entity.Id, entity);
                }
                catch (Exception exception)
                {

                }

            }
        }
        public void Delete(long article)
        {

            try
            {
                if (goodsCatalog.Count > 0)
                {
                    var removingGood = goodsCatalog.FirstOrDefault(t => t.Value.Article == article);
                    goodsCatalog.TryRemove(removingGood.Key, out _);
                }
            }
            catch (Exception exception)
            {

            }

        }

        public ConcurrentDictionary<int, Good> GetAll()
        {
            lock (_syncObj)
            {
                return goodsCatalog;
            }
        }

        public void Update(Good entity)
        {
            throw new NotImplementedException();
        }
    }
}
