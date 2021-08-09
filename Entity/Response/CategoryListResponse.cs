using Object.Entity;
using System;
using System.Collections.Generic;

namespace Object.Response
{
    public class CategoryListResponse : BaseResponse<List<Category>>
    {
        public CategoryListResponse (List<Category> categoryList) : base(categoryList) { }
        public CategoryListResponse(Exception exception) : base(exception) { }
    }
}
