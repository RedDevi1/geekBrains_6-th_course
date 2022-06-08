using MarketPlace.Interfaces;

namespace MarketPlace.Models
{
    public class GoodsCatalog : BaseEntity<int>, IGoodsCatalog
    {
        private readonly List<Good> goods = new();
        private readonly object _syncObj = new();

        public List<Good> Goods { get => goods; }

        public void Create(Good entity)
        {
            if (entity != null)
            {
                lock (_syncObj)
                {
                    try
                    {
                        goods.Add(entity);
                    }
                    catch (Exception exception)
                    {

                    }
                }                   
            }
        }
        public void Delete(int id)
        {
            lock (_syncObj)
            {
                try
                {
                    if (goods.Count > 0)
                    {
                        var deletingfGood = goods.Find(x => x.Id == id);
                        if (deletingfGood != null)
                        {
                            goods.Remove(deletingfGood);
                        }
                    }
                }
                catch (Exception exception)
                {

                }
            }               
        }

        public IReadOnlyList<Good> GetAll()
        {
            lock (_syncObj)
            {
                return goods;
            }               
        }

        public void Update(Good entity)
        {
            throw new NotImplementedException();
        }
    }
}
