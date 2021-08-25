using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TestUi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost("GetFilteredCategories")]
        public async Task<CategoryListResponse> GetFilteredCategoriesAsync(CategoryListRequest request)
        {
            return await _categoryService.GetFilteredCategoriesAsync(request);
        }

        [HttpPost("CreateCategory")]
        public async Task<CategoryCreateResponse> CreateCategory(CategoryCreateRequest request)
        {
            return await _categoryService.CreateCategory(request);
        }
    }
}
