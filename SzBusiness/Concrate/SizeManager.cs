using SzBusiness.Interface;
using SzDataAccess.Interface;

namespace SzBusiness.Concrate
{
    public class SizeManager : ISizeService
    {
        private readonly ISizeDal _sizeDal;
        public SizeManager(ISizeDal sizeDal)
        {
            _sizeDal = sizeDal;
        }
    }
}
