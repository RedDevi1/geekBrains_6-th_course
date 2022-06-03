using MarketPlace.Interfaces;

namespace MarketPlace.Models
{
    public class GoodsCatalog : BaseEntity<int>, IGoodsCatalog
    {
        private readonly List<Good> goods = new();

        public List<Good> Goods { get => goods; }

        public void Create(Good entity)
        {
            if (entity != null)
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
        public void Delete(int id)
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

        public IReadOnlyList<Good> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Good entity)
        {
            throw new NotImplementedException();
        }
    }
}
