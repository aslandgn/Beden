using Core.Concrate;
using SzDataAccess.Interface;
using SzObject.Entity;

namespace SzDataAccess.Concrate
{
    public class SizeDal : EfRepository<Size, SzContext>, ISizeDal
    {
        public SizeDal(SzContext szContext) : base(szContext) { }
    }
}
