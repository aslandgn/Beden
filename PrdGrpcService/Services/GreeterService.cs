using PrdBusiness.Interface;
using Greet;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace PrdGrpcService
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger, ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            var response = new HelloReply
            {
                Message = "Hello " + request.Name
            };
            var serviceResp = await _categoryService.GetFilteredCategoriesAsync(new PrdObject.Request.CategoryListRequest { });
            var data = serviceResp.Data.FirstOrDefault();
            response.Message = data.Guid + data.Name;
            return response;
        }
    }
}
