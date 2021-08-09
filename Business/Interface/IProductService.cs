using Object.Request;
using Object.Response;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface IProductService
    {
        Task<ProductCreateResponse> CreateProduct(ProductCreateRequest request);
    }
}
