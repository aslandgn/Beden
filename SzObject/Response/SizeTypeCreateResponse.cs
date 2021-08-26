using CommonObject.Response;
using System;
using SzObject.Entity;

namespace SzObject.Response
{
    public class SizeTypeCreateResponse : BaseResponse<SizeType>
    {
        public SizeTypeCreateResponse(SizeType sizeType) : base(sizeType) { }
        public SizeTypeCreateResponse(Exception exception) : base(exception) { }
    }
}
