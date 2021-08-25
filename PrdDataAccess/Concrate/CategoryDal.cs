using Core.Concrate;
using PrdDataAccess.Interface;
using PrdObject.Entity;

namespace PrdDataAccess.Concrate
{
    public class CategoryDal : EfRepository<Category, PrdContext>, ICategoryDal 
    {
        public CategoryDal(PrdContext prdContext) : base(prdContext)
        {
        }
    }
}
