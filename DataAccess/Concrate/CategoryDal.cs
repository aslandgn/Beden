using Core.Concrate;
using DataAccess.Interface;
using Object.Entity;

namespace DataAccess.Concrate
{
    public class CategoryDal : EfRepository<Category, PrdContext>, ICategoryDal 
    {
        public CategoryDal(PrdContext prdContext) : base(prdContext)
        {
        }
    }
}
