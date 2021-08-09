using Core.Concrate;
using DataAccess.Interface;
using Object.Entity;

namespace DataAccess.Concrate
{
    public class ProductDal : EfRepository<Product, PrdContext>, IProductDal 
    {
        public ProductDal(PrdContext prdContext) : base(prdContext)
        {
        }
    }
}
