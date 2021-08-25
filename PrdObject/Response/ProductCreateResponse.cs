using PrdObject.Entity;
using System;

namespace PrdObject.Response
{
    public class ProductCreateResponse: BaseResponse<Product>
    {
        public ProductCreateResponse(Product product) : base(product) { }
        public ProductCreateResponse(Exception exception) : base(exception) { }
    }
}
