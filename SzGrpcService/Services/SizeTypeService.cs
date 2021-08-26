using Grpc.Core;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using SzBusiness.Interface;
using SzObject.Request;

namespace SzGrpcService
{
    public class SizeTypeService : SizeTypeOpr.SizeTypeOprBase
    {
        private readonly ILogger<SizeTypeService> _logger;
        private readonly ISizeTypeService _sizeService;
        private readonly IMapper _mapper;
        public SizeTypeService(ILogger<SizeTypeService> logger, ISizeTypeService sizeService, IMapper mapper)
        {
            _logger = logger;
            _sizeService = sizeService;
            _mapper = mapper;
        }

        public override async Task<CreateSizeTypeResponse> CreateSizeType(CreateSizeTypeRequest request, ServerCallContext context)
        {
            var serviceResponse = await _sizeService.CreateSizeTypeAsync(_mapper.Map<SizeTypeCreateRequest>(request));
            var response = _mapper.Map<CreateSizeTypeResponse>(serviceResponse);
            _logger.LogInformation("request => {request} \n response => {response}", request, response);
            return response;
        }
    }
}
