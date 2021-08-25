using PrdObject.Entity;
using System;
using System.Collections.Generic;

namespace PrdObject.Response
{
    public class ProductListResponse : BaseResponse<List<Product>>
    {
        public ProductListResponse (List<Product> categoryList) : base(categoryList) { }
        public ProductListResponse(Exception exception) : base(exception) { }
    }
}
