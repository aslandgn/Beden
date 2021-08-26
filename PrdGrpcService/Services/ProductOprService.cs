using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using PrdBusiness.Interface;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PrdGrpcService.Services
{
    public class ProductOprService : ProductOpr.ProductOprBase
    {
        private readonly ILogger<ProductOprService> _logger;
        private readonly IProductService _productService;
        public ProductOprService(ILogger<ProductOprService> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public override async Task<ProductCreateResponse> CreateProduct(ProductCreateRequest request, ServerCallContext context)
        {
            var serviceResp = await _productService.CreateProduct(new PrdObject.Request.ProductCreateRequest
            {
                Name = request.Name,
                CategoryGuId = new Guid(request.CategoryGuId),
                SizeTypeGuId = new Guid(request.SizeTypeGuId),
            });
            var response = new ProductCreateResponse
            {
                Data = new Product
                {
                    Guid = serviceResp.Data.Guid.ToString(),
                    Name = serviceResp.Data.Name,
                    CategoryGuId = serviceResp.Data.CategoryGuid.ToString(),
                    SizeTypeGuId = serviceResp.Data.SizeTypeGuid.ToString(),
                    Status = serviceResp.Data.Status,
                    IsDeleted = serviceResp.Data.IsDeleted
                },
                IsSuccess = serviceResp.IsSuccess,
                ResponseTime = Timestamp.FromDateTime(serviceResp.ResponseTime)
            };
            _logger.LogInformation(MethodBase.GetCurrentMethod().Name, " ---- request => {request} ---- response => {response}", request, response);
            return response;
        }

        public override async Task<ProductListResponse> GetFilteredProducts(ProductListRequest request, ServerCallContext context)
        {

            ProductListResponse response;
            try
            {
                var serviceResponse = await _productService.GetFilteredProductsAsync(new PrdObject.Request.ProductListRequest { Name = request.Name });
                if (serviceResponse.IsSuccess)
                {

                    var responseData = serviceResponse.Data.Select(x => new Product
                    {
                        Guid = x.Guid.ToString(),
                        Name = x.Name,
                        CategoryGuId = x.CategoryGuid.ToString(),
                        SizeTypeGuId = x.SizeTypeGuid.ToString(),
                        Status = x.Status,
                        IsDeleted = x.IsDeleted
                    }).ToList();
                    response = new ProductListResponse
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
                _logger.LogInformation(MethodBase.GetCurrentMethod().Name, " ---- request => {request} ---- response => {response}", request, response);

            }
            catch (System.Exception e)
            {
                response = new ProductListResponse
                {
                    IsSuccess = false,
                    ResponseTime = Timestamp.FromDateTime(DateTime.Now.ToUniversalTime()),
                    Exception = new Protos.Exception { HResult = e.HResult, Message = e.Message, Source = e.Source, StackTrace = e.StackTrace }
                };
            }
            return response;
        }
    }
}
