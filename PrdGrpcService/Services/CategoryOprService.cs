using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using PrdBusiness.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PrdGrpcService.Services
{
    public class CategoryOprService : CategoryOpr.CategoryOprBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryOprService> _logger;
        public CategoryOprService(ILogger<CategoryOprService> logger, ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        public override async Task<CategoryCreateResponse> CreateCategory(CategoryCreateRequest request, ServerCallContext context)
        {
            var serviceResp = await _categoryService.CreateCategory(new PrdObject.Request.CategoryCreateRequest
            {
                Name = request.Name,
                ParentCategoryId = string.IsNullOrWhiteSpace(request.ParentCategoryId) ? null : new Guid(request.ParentCategoryId)
            });
            var response = new CategoryCreateResponse
            {
                Data = new Category
                {
                    Guid = serviceResp.Data.Guid.ToString() ?? null,
                    Name = serviceResp.Data.Name ?? null,
                    ParentCategoryGuid = serviceResp.Data.ParentCategoryGuid.ToString() ?? null,
                    Status = serviceResp.Data.Status,
                    IsDeleted = serviceResp.Data.IsDeleted
                },
                IsSuccess = serviceResp.IsSuccess,
                ResponseTime = Timestamp.FromDateTime(serviceResp.ResponseTime.ToUniversalTime())
            };
            _logger.LogInformation("request => {request} \n response => {response}", request, response);
            return response;
        }

        public async override Task<CategoryListResponse> GetFilteredCategories(CategoryListRequest request, ServerCallContext context)
        {
            CategoryListResponse response;
            try
            {
                var serviceResponse = await _categoryService.GetFilteredCategoriesAsync(new PrdObject.Request.CategoryListRequest { Name = request.Name });
                if (serviceResponse.IsSuccess)
                {

                    var responseData = serviceResponse.Data.Select(x => new Category
                    {
                        Guid = x.Guid.ToString(),
                        Name = x.Name,
                        ParentCategoryGuid = x.ParentCategoryGuid.ToString(),
                        Status = x.Status,
                        IsDeleted = x.IsDeleted
                    }).ToList();
                    response = new CategoryListResponse
                    {
                        IsSuccess = serviceResponse.IsSuccess,
                        ResponseTime = Timestamp.FromDateTime(serviceResponse.ResponseTime.ToUniversalTime())
                    };
                    response.Data.AddRange(responseData);
                }
                else
                {
                    throw serviceResponse.Exception;
                }
                _logger.LogInformation("request => {request} \n response => {response}", request, response);

            }
            catch (System.Exception e)
            {
                response = new CategoryListResponse
                {
                    IsSuccess = false,
                    ResponseTime = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime()),
                    Exception = new Exception { HResult = e.HResult, Message = e.Message, Source = e.Source, StackTrace = e.StackTrace }
                };
                _logger.LogError(e, "request => {request} \n error => {error}", request, e.Message);
            }
            return response;
        }
    }
}
