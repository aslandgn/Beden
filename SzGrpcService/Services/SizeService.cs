using Grpc.Core;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using SzBusiness.Interface;
using SzObject.Request;

namespace SzGrpcService
{
    public class SizeService: SizeOpr.SizeOprBase
    {
        private readonly ILogger<SizeService> _logger;
        private readonly ISizeService _sizeService;
        private readonly IMapper _mapper;
        public SizeService(ILogger<SizeService> logger, ISizeService sizeService, IMapper mapper)
        {
            _logger = logger;
            _sizeService = sizeService;
            _mapper = mapper;
        }

        public override async Task<CreateSizeResponse> CreateSize(CreateSizeRequest request, ServerCallContext context)
        {
            var serviceResponse = await _sizeService.CreateSizeAsync(_mapper.Map<SizeCreateRequest>(request));
            var response = _mapper.Map<CreateSizeResponse>(serviceResponse);
            _logger.LogInformation("request => {request} \n response => {response}", request, response);
            return response;
        }
    }
}
