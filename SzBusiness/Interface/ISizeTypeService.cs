using System.Threading.Tasks;
using SzObject.Request;
using SzObject.Response;

namespace SzBusiness.Interface
{
    public interface ISizeTypeService
    {
        Task<SizeTypeCreateResponse> CreateSizeTypeAsync(SizeTypeCreateRequest request);
        Task<SizeTypeListResponse> GetActiveSizeTypes();
    }
}
