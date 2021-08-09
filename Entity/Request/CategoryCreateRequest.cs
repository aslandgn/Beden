using System;

namespace Object.Request
{
    public class CategoryCreateRequest
    {
        public string Name { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}
