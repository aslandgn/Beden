using CommonObject.Response;
using PrdObject.Entity;
using System;

namespace PrdObject.Response
{
    public class CategoryCreateResponse: BaseResponse<Category>
    {
        public CategoryCreateResponse(Category category) : base(category) { }
        public CategoryCreateResponse(Exception exception) : base(exception) { }
    }
}
