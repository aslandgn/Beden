using Core.Concrate;
using SzDataAccess.Interface;
using SzObject.Entity;

namespace SzDataAccess.Concrate
{
    public class SizeTypeDal : EfRepository<SizeType, SzContext>, ISizeTypeDal
    {
        public SizeTypeDal(SzContext szContext) : base(szContext) { }
    }
}
