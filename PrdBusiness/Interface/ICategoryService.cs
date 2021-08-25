using PrdObject.Request;
using PrdObject.Response;
using System.Threading.Tasks;

namespace PrdBusiness.Interface
{
    public interface ICategoryService
    {
        Task<CategoryCreateResponse> CreateCategory(CategoryCreateRequest request);
        Task<CategoryListResponse> GetFilteredCategoriesAsync(CategoryListRequest request);
    }
}
