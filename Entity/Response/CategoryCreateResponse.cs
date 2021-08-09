using Object.Entity;
using System;

namespace Object.Response
{
    public class CategoryCreateResponse: BaseResponse<Category>
    {
        public CategoryCreateResponse(Category category) : base(category) { }
        public CategoryCreateResponse(Exception exception) : base(exception) { }
    }
}
