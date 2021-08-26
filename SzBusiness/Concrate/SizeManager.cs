using System;
using System.Threading.Tasks;
using SzBusiness.Interface;
using SzDataAccess.Interface;
using SzObject.Entity;
using SzObject.Request;
using SzObject.Response;

namespace SzBusiness.Concrate
{
    public class SizeManager : ISizeService
    {
        private readonly ISizeDal _sizeDal;
        public SizeManager(ISizeDal sizeDal)
        {
            _sizeDal = sizeDal;
        }

        public async Task<SizeCreateResponse> CreateSizeAsync(SizeCreateRequest request)
        {
            SizeCreateResponse createResponse;
            try
            {
                var size = new Size { };
                var serviceResponse = await _sizeDal.AddAsync(size);
                createResponse = new SizeCreateResponse(serviceResponse);
            }
            catch (Exception e)
            {
                createResponse = new SizeCreateResponse(e);
            }
            return createResponse;
        }
    }
}
