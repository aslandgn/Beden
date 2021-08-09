using Business.Interface;
using DataAccess.Interface;
using Object.Entity;
using Object.Request;
using Object.Response;
using System;
using System.Threading.Tasks;

namespace Business.Concrate
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public async Task<CategoryCreateResponse> CreateCategory(CategoryCreateRequest request)
        {
            CategoryCreateResponse createResponse;
            try
            {
                bool parentExist = false;
                if (request.ParentCategoryId != null)
                {
                    var parent = await _categoryDal.GetFirstOrDefaultWithQueryAsync(x => x.Guid == request.ParentCategoryId);
                    if (parent != null)
                    {
                        parentExist = true;
                    }
                }
                Category category;
                if (parentExist)
                {
                    category = new Category(request.Name, request.ParentCategoryId);
                }
                else
                {
                    category = new Category(request.Name, null);
                }
                var serviceResponse = await _categoryDal.AddAsync(category);
                createResponse = new CategoryCreateResponse(serviceResponse);
            }
            catch (Exception e)
            {
                createResponse = new CategoryCreateResponse(e);
            }
            return createResponse;
        }
        public async Task<CategoryListResponse> GetFilteredCategoriesAsync(CategoryListRequest request)
        {
            CategoryListResponse listResponse;
            try
            {
                var serviceResponse = await _categoryDal.GetListWithQueryAsync(x => x.Status && !x.IsDeleted);
                listResponse = new CategoryListResponse(serviceResponse);
            }
            catch (Exception e)
            {
                listResponse = new CategoryListResponse(e);
            }
            return listResponse;
        }
    }
}
