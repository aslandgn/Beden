using CommonObject.Response;
using System;
using SzObject.Entity;

namespace SzObject.Response
{
    public class SizeCreateResponse: BaseResponse<Size>
    {
        public SizeCreateResponse(Size size): base(size) { }
        public SizeCreateResponse(Exception exception): base(exception) { }
    }
}
