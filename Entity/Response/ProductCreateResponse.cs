using Object.Entity;
using System;

namespace Object.Response
{
    public class ProductCreateResponse: BaseResponse<Product>
    {
        public ProductCreateResponse(Product product) : base(product) { }
        public ProductCreateResponse(Exception exception) : base(exception) { }
    }
}
