using Google.Protobuf.WellKnownTypes;
using Mapster;
using System;
using System.Collections.Generic;

namespace SzGrpcService.Mappings
{
    public static class SzMapper
    {
        public static void Register(TypeAdapterConfig config)
        {
            #region Size
            config.NewConfig<CreateSizeRequest, SzObject.Request.SizeCreateRequest>()
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.SizeTypeGuid, src => new Guid(src.SizeTypeGuid))
                .Map(dest => dest.Order, src => src.Order)
                ;

            config.NewConfig<SzObject.Response.SizeCreateResponse, CreateSizeResponse>()
                .Map(dest => dest.IsSuccess, src => src.IsSuccess)
                .Map(dest => dest.ResponseTime, src => Timestamp.FromDateTime(src.ResponseTime.ToUniversalTime()))
                ;

            config.NewConfig<SzObject.Entity.Size, Size>()
                .Map(dest => dest.Guid, src => src.Guid.ToString())
                .Map(dest => dest.SizeTypeGuid, src => src.SizeTypeGuid.ToString())
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Order, src => src.Order)
                .Map(dest => dest.Status, src => src.Status)
                .Map(dest => dest.IsDeleted, src => src.IsDeleted)
                ;
            #endregion

            #region SizeType
            config.NewConfig<CreateSizeTypeRequest, SzObject.Request.SizeTypeCreateRequest>()
                .Map(dest => dest.Name, src => src.Name)
                ;

            config.NewConfig<SzObject.Response.SizeTypeCreateResponse, CreateSizeTypeResponse>()
                .Map(dest => dest.IsSuccess, src => src.IsSuccess)
                .Map(dest => dest.ResponseTime, src => Timestamp.FromDateTime(src.ResponseTime.ToUniversalTime()))
                ;

            config.NewConfig<SzObject.Response.SizeTypeListResponse, GetActiveSizeTypesResponse>()
                .Map(dest => dest.IsSuccess, src => src.IsSuccess)
                .Map(dest => dest.ResponseTime, src => Timestamp.FromDateTime(src.ResponseTime.ToUniversalTime()))
                .AfterMapping((source, dest) => dest.Data.AddRange(source.Data.Adapt<List<SizeType>>()))
            ;

            config.NewConfig<SzObject.Entity.SizeType, SizeType>()
                .Map(dest => dest.Guid, src => src.Guid.ToString())
                .Map(dest => dest.Name, src => src.Name)
                .Map(dest => dest.Status, src => src.Status)
                .Map(dest => dest.IsDeleted, src => src.IsDeleted)
                ;
            #endregion

            #region Common
            config.NewConfig<System.Exception, Exception>()
                .Map(dest => dest.Message, src => src.Message)
                .Map(dest => dest.HResult, src => src.HResult)
                .Map(dest => dest.Source, src => src.Source)
                .Map(dest => dest.StackTrace, src => src.StackTrace)
                ;
            #endregion
        }

    }
}
