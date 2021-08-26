using SzBusiness.Interface;
using SzDataAccess.Interface;

namespace SzBusiness.Concrate
{
    public class SizeTypeManager : ISizeTypeService
    {
        private readonly ISizeTypeDal _sizeTypeDal;
        public SizeTypeManager(ISizeTypeDal sizeTypeDal)
        {
            _sizeTypeDal = sizeTypeDal;
        }
    }
}
