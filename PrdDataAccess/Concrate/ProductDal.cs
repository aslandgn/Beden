using Core.Concrate;
using PrdDataAccess.Interface;
using PrdObject.Entity;

namespace PrdDataAccess.Concrate
{
    public class ProductDal : EfRepository<Product, PrdContext>, IProductDal 
    {
        public ProductDal(PrdContext prdContext) : base(prdContext)
        {
        }
    }
}
