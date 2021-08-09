using Business.Interface;
using DataAccess.Interface;
using Object.Entity;
using Object.Request;
using Object.Response;
using System;
using System.Threading.Tasks;

namespace Business.Concrate
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
    }
}
