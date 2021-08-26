using Google.Protobuf.WellKnownTypes;
using Mapster;
using System;
using SzObject.Request;
using SzObject.Response;

namespace SzGrpcService.Mappings
{
    public static class SzMapper
    {
        public static void Initialize()
        {

            TypeAdapterConfig<CreateSizeRequest, SizeCreateRequest>.NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.SizeTypeGuid, src => new Guid(src.SizeTypeGuid))
                .Map(dest => dest.Order, src => src.Order)
                ;

            TypeAdapterConfig<SizeCreateResponse, CreateSizeResponse>.NewConfig()
                .Map(dest => dest.IsSuccess, src => src.IsSuccess)
                .Map(dest => dest.ResponseTime, src => Timestamp.FromDateTime(src.ResponseTime))
                ;

            TypeAdapterConfig<CreateSizeTypeRequest, SizeTypeCreateRequest>.NewConfig()
                .Map(dest => dest.Name, src => src.Name)
                ;

            TypeAdapterConfig<SizeTypeCreateResponse, CreateSizeTypeResponse>.NewConfig()
                .Map(dest => dest.IsSuccess, src => src.IsSuccess)
                .Map(dest => dest.ResponseTime, src => Timestamp.FromDateTime(src.ResponseTime))
                ;

            TypeAdapterConfig<System.Exception, Exception>.NewConfig()
                .Map(dest => dest.Message, src => src.Message)
                .Map(dest => dest.HResult, src => src.HResult)
                .Map(dest => dest.Source, src => src.Source)
                .Map(dest => dest.StackTrace, src => src.StackTrace)
                ;

            TypeAdapterConfig<SzObject.Entity.Size, Size>.NewConfig()
                .Map(dest => dest.Guid, src => src.Guid.ToString())
                .Map(dest => dest.SizeTypeGuid, src => src.SizeTypeGuid.ToString())
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Order, src => src.Order)
                .Map(dest => dest.Status, src => src.Status)
                .Map(dest => dest.IsDeleted, src => src.IsDeleted)
                ;

            TypeAdapterConfig<SzObject.Entity.SizeType, SizeType>.NewConfig()
                .Map(dest => dest.Guid, src => src.Guid.ToString())
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Status, src => src.Status)
                .Map(dest => dest.IsDeleted, src => src.IsDeleted)
                ;
        }
    }
}
