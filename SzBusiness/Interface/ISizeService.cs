using System.Threading.Tasks;
using SzObject.Request;
using SzObject.Response;

namespace SzBusiness.Interface
{
    public interface ISizeService
    {
        Task<SizeCreateResponse> CreateSizeAsync(SizeCreateRequest request);
    }
}
