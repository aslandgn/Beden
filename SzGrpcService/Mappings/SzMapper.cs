using Google.Protobuf.WellKnownTypes;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using SzObject.Request;
using SzObject.Response;

namespace SzGrpcService.Mappings
{
    public class SzMapper: IRegister
    {
        public void Register(TypeAdapterConfig config)
        {

            config.NewConfig<CreateSizeRequest, SizeCreateRequest>()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.SizeTypeGuid, src => new Guid(src.SizeTypeGuid))
                .Map(dest => dest.Order, src => src.Order)
                ;

            config.NewConfig<SizeCreateResponse, CreateSizeResponse>()
                .Map(dest => dest.IsSuccess, src => src.IsSuccess)
                .Map(dest => dest.ResponseTime, src => Timestamp.FromDateTime(src.ResponseTime.ToUniversalTime()))
                ;

            config.NewConfig<CreateSizeTypeRequest, SizeTypeCreateRequest>()
                .Map(dest => dest.Name, src => src.Name)
                ;

            config.NewConfig<SizeTypeCreateResponse, CreateSizeTypeResponse>()
                .Map(dest => dest.IsSuccess, src => src.IsSuccess)
                .Map(dest => dest.ResponseTime, src => Timestamp.FromDateTime(src.ResponseTime.ToUniversalTime()))
                ;

            config.NewConfig<SizeTypeListResponse, GetActiveSizeTypesResponse>()
                .Map(dest => dest.IsSuccess, src => src.IsSuccess)
                .Map(dest => dest.ResponseTime, src => Timestamp.FromDateTime(src.ResponseTime.ToUniversalTime()))
                .AfterMapping((source, dest) => dest.Data.AddRange(source.Data.Adapt<List<SizeType>>()))
            ;

            config.NewConfig<System.Exception, Exception>()
                .Map(dest => dest.Message, src => src.Message)
                .Map(dest => dest.HResult, src => src.HResult)
                .Map(dest => dest.Source, src => src.Source)
                .Map(dest => dest.StackTrace, src => src.StackTrace)
                ;

            config.NewConfig<SzObject.Entity.Size, Size>()
                .Map(dest => dest.Guid, src => src.Guid.ToString())
                .Map(dest => dest.SizeTypeGuid, src => src.SizeTypeGuid.ToString())
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Order, src => src.Order)
                .Map(dest => dest.Status, src => src.Status)
                .Map(dest => dest.IsDeleted, src => src.IsDeleted)
                ;

            config.NewConfig<SzObject.Entity.SizeType, SizeType>()
                .Map(dest => dest.Guid, src => src.Guid.ToString())
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Status, src => src.Status)
                .Map(dest => dest.IsDeleted, src => src.IsDeleted)
                ;
        }

    }
}
