using System;
using System.Linq;
using System.Threading.Tasks;
using SzBusiness.Interface;
using SzDataAccess.Interface;
using SzObject.Entity;
using SzObject.Request;
using SzObject.Response;

namespace SzBusiness.Concrate
{
    public class SizeTypeManager : ISizeTypeService
    {
        private readonly ISizeTypeDal _sizeTypeDal;
        public SizeTypeManager(ISizeTypeDal sizeTypeDal)
        {
            _sizeTypeDal = sizeTypeDal;
        }
        public async Task<SizeTypeCreateResponse> CreateSizeTypeAsync(SizeTypeCreateRequest request)
        {
            SizeTypeCreateResponse createResponse;
            try
            {
                var sizeType = new SizeType(request.Name);
                var serviceResponse = await _sizeTypeDal.AddAsync(sizeType);
                createResponse = new SizeTypeCreateResponse(serviceResponse);
            }
            catch (Exception e)
            {
                createResponse = new SizeTypeCreateResponse(e);
            }
            return createResponse;
        }

        public async Task<SizeTypeListResponse> GetActiveSizeTypes()
        {
            SizeTypeListResponse sizeTypeListResponse;
            try
            {
                var serviceResponse = await _sizeTypeDal.GetListWithQueryAsync(x => x.Status && !x.IsDeleted);
                sizeTypeListResponse = new SizeTypeListResponse(serviceResponse.ToList());
            }
            catch (Exception e)
            {
                sizeTypeListResponse = new SizeTypeListResponse(e);
            }
            return sizeTypeListResponse;
        }
    }
}
