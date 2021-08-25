using PrdBusiness.Interface;
using PrdDataAccess.Interface;
using PrdObject.Entity;
using PrdObject.Request;
using PrdObject.Response;
using System;
using System.Threading.Tasks;

namespace PrdBusiness.Concrate
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public async Task<ProductCreateResponse> CreateProduct(ProductCreateRequest request)
        {
            ProductCreateResponse createResponse;
            try
            {
                var product = new Product { };
                var serviceResponse = await _productDal.AddAsync(product);
                createResponse = new ProductCreateResponse(serviceResponse);
            }
            catch (Exception e)
            {
                createResponse = new ProductCreateResponse(e);
            }
            return createResponse;
        }

        public async Task<ProductListResponse> GetFilteredProductsAsync(ProductListRequest request)
        {
            ProductListResponse listResponse;
            try
            {
                var serviceResponse = await _productDal.GetListWithQueryAsync(x => x.Status && !x.IsDeleted);
                listResponse = new ProductListResponse(serviceResponse);
            }
            catch (Exception e)
            {
                listResponse = new ProductListResponse(e);
            }
            return listResponse;
        }
    }
}
