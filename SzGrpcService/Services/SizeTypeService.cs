using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MapsterMapper;
using System.Threading.Tasks;
using SzBusiness.Interface;
using SzObject.Request;

namespace SzGrpcService
{
    public class SizeTypeService : SizeTypeOpr.SizeTypeOprBase
    {
        private readonly ISizeTypeService _sizeService;
        private readonly IMapper _mapper;
        public SizeTypeService(ISizeTypeService sizeService, IMapper mapper)
        {
            _sizeService = sizeService;
            _mapper = mapper;
        }

        public override async Task<CreateSizeTypeResponse> CreateSizeType(CreateSizeTypeRequest request, ServerCallContext context)
        {
            var serviceResponse = await _sizeService.CreateSizeTypeAsync(_mapper.Map<SizeTypeCreateRequest>(request));
            var response = _mapper.Map<CreateSizeTypeResponse>(serviceResponse);
            return response;
        }

        public override async Task<GetActiveSizeTypesResponse> GetActiveSizeTypes(Empty request, ServerCallContext context)
        {
            var serviceResponse = await _sizeService.GetActiveSizeTypes();
            var response = _mapper.Map<GetActiveSizeTypesResponse>(serviceResponse);
            return response;
        }
    }
}
