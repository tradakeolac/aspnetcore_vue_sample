namespace Saleman.Web.Infrastructure.Factories
{
    using AutoMapper;
    using Saleman.Model.Entities;
    using Saleman.Model.ServiceObjects;
    using Saleman.Web.ViewModel;
    using Service.Exceptions;
    using System;
    using System.Diagnostics.Contracts;

    public class AutoMapperObjectFactory : IServiceObjectFactory, IEntityFactory, IViewModelFactory
    {
        protected readonly IMapper Mapper;

        public AutoMapperObjectFactory(IMapper mapper)
        {
            this.Mapper = mapper;
        }

        public virtual TDestination Create<TDestination>(object source) where TDestination : class
        {
            Guard.EnsureRequestNotNull(source, nameof(source) + $" map to type {typeof(TDestination).Name}");

            try
            {
                return Mapper.Map<TDestination>(source);
            }
            catch(Exception ex)
            {
                throw ex.ToBusinessException<MappingException>();
            }
        }
    }

    public class MediaAssetEntityFactory : AutoMapperObjectFactory, IMediaAssetEntityFactory
    {
        public MediaAssetEntityFactory(IMapper mapper) : base(mapper)
        {
        }

        public override TDestination Create<TDestination>(object source)
        {
            var mediaServiceObject = source as MediaServiceObject;
            if (mediaServiceObject == null)
                throw new ArgumentException("the source object is not MediaServiceObject").ToBusinessException<MappingException>();

            try
            {
                switch (mediaServiceObject.MediaType)
                {
                    case MediaAssetEntity.MediaType.Image:
                        return this.Mapper.Map<ImageMediaEntity>(mediaServiceObject) as TDestination;
                    case MediaAssetEntity.MediaType.Video:
                        return this.Mapper.Map<VideoMediaEntity>(mediaServiceObject) as TDestination;
                    case MediaAssetEntity.MediaType.Pdf:
                        return this.Mapper.Map<PdfMediaEntity>(mediaServiceObject) as TDestination;
                    default:
                        return this.Mapper.Map<FileAssetEntity>(mediaServiceObject) as TDestination;
                }
            }
            catch(Exception ex)
            {
                throw ex.ToBusinessException<MappingException>();
            }
        }
    }

    public class SaleAuditTransformer : ISaleAuditTransformer
    {
        protected readonly IMapper Mapper;

        public SaleAuditTransformer(IMapper mapper)
        {
            this.Mapper = mapper;
        }

        public TSaleAuditEntity Transform<TSaleAuditEntity>(SaleAuditEntity source)
            where TSaleAuditEntity : SaleAuditEntity
        {
            Guard.EnsureRequestNotNull(source, nameof(source) + $" map to type {nameof(SaleAuditEntity)}");

            try
            {
                return this.Mapper.Map<TSaleAuditEntity>(source);
            }
            catch(Exception ex)
            {
                throw ex.ToBusinessException<MappingException>();
            }
        }
    }
}