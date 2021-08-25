using System;

namespace PrdObject.Request
{
    public class ProductCreateRequest
    {
        public string Name { get; set; }
        public Guid CategoryGuId { get; set; }
        public Guid SizeTypeGuId { get; set; }
    }
}
