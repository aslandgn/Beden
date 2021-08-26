using System;

namespace SzObject.Request
{
    public class SizeCreateRequest
    {
        public string Name { get; set; }
        public Guid SizeTypeGuid{ get; set; }
        public short Order { get; set; }
    }
}
