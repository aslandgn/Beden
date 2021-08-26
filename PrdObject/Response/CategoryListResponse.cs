using CommonObject.Response;
using PrdObject.Entity;
using System;
using System.Collections.Generic;

namespace PrdObject.Response
{
    public class CategoryListResponse : BaseResponse<List<Category>>
    {
        public CategoryListResponse (List<Category> categoryList) : base(categoryList) { }
        public CategoryListResponse(Exception exception) : base(exception) { }
    }
}
