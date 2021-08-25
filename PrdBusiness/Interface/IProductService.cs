using PrdObject.Request;
using PrdObject.Response;
using System.Threading.Tasks;

namespace PrdBusiness.Interface
{
    public interface IProductService
    {
        Task<ProductCreateResponse> CreateProduct(ProductCreateRequest request);
        Task<ProductListResponse> GetFilteredProductsAsync(ProductListRequest request);
    }
}
