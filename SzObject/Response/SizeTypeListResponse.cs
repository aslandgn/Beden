using CommonObject.Response;
using System;
using System.Collections.Generic;
using SzObject.Entity;

namespace SzObject.Response
{
    public class SizeTypeListResponse :  BaseResponse<List<SizeType>>
    {
        public SizeTypeListResponse(List<SizeType> sizeTypes) : base(sizeTypes) { }
        public SizeTypeListResponse(Exception exception) : base(exception) { }
    }
}
