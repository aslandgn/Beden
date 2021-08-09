using Object.Request;
using Object.Response;
using System.Threading.Tasks;

namespace Business.Interface
{
    public interface ICategoryService
    {
        Task<CategoryCreateResponse> CreateCategory(CategoryCreateRequest request);
        Task<CategoryListResponse> GetFilteredCategoriesAsync(CategoryListRequest request);
    }
}
