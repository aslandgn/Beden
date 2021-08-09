using Business.Interface;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PrdGrpcService
{
    public class CategoryOprService : CategoryOpr.CategoryOprBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<GreeterService> _logger;
        public CategoryOprService(ILogger<GreeterService> logger, ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        public override async Task<CategoryCreateResponse> CreateCategory(CategoryCreateRequest request, ServerCallContext context)
        {
            var serviceResp = await _categoryService.CreateCategory(new Object.Request.CategoryCreateRequest
            {
                Name = request.Name,
                ParentCategoryId = new Guid(request.ParentCategoryId)
            });
            var response = new CategoryCreateResponse
            {
                Data = new Category
                {
                    Guid = serviceResp.Data.Guid.ToString(),
                    Name = serviceResp.Data.Name,
                    ParentCategoryGuid = serviceResp.Data.ParentCategoryGuid.ToString(),
                    Status = serviceResp.Data.Status,
                    IsDeleted = serviceResp.Data.IsDeleted
                },
                IsSuccess = serviceResp.IsSuccess,
                ResponseTime = Timestamp.FromDateTime(serviceResp.ResponseTime)
            };
            _logger.LogInformation("CreateCategory ---- request => {request} ---- response => {response}", request, response);
            return response;
        }

        public async override Task<CategoryListResponse> GetFilteredCategories(CategoryListRequest request, ServerCallContext context)
        {
            CategoryListResponse response;
            try
            {
                var serviceResponse = await _categoryService.GetFilteredCategoriesAsync(new Object.Request.CategoryListRequest { Name = request.Name });
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
                _logger.LogInformation("GetFilteredCategories ---- request => {request} ---- response => {response}", request, response);

            }
            catch (System.Exception e)
            {
                response = new CategoryListResponse
                {
                    IsSuccess = false,
                    ResponseTime = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime()),
                    Exception = new Exception
                    {
                        HResult = e.HResult,
                        Message = e.Message,
                        Source = e.Source,
                        StackTrace = e.StackTrace
                    }
                };
            }
            return response;
        }
    }
}
