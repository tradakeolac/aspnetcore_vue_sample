namespace Saleman.Model.AutomapProfiles
{
    using Entities;
    using ServiceObjects;
    using AutoMapper;

    public class MediaAssetProfile : AutoMapper.Profile
    {
        public MediaAssetProfile()
        {
            CreateMap<MediaServiceObject, ImageMediaEntity>()
                .ForMember(m => m.CreatedBy, obj => obj.Ignore());

            CreateMap<MediaServiceObject, VideoMediaEntity>()
                .ForMember(m => m.CreatedBy, obj => obj.Ignore());

            CreateMap<MediaServiceObject, PdfMediaEntity>()
                .ForMember(m => m.CreatedBy, obj => obj.Ignore());

            CreateMap<MediaServiceObject, FileAssetEntity>()
                .ForMember(m => m.CreatedBy, obj => obj.Ignore());

            // Entity
            CreateMap<ImageMediaEntity, MediaServiceObject>()
                .ForMember(s => s.MediaType, obj => obj.MapFrom(s => MediaAssetEntity.MediaType.Image));


            CreateMap<VideoMediaEntity, MediaServiceObject>()
                .ForMember(s => s.MediaType, obj => obj.MapFrom(s => MediaAssetEntity.MediaType.Video));


            CreateMap<FileAssetEntity, MediaServiceObject>()
                .ForMember(s => s.MediaType, obj => obj.MapFrom(s => MediaAssetEntity.MediaType.Unknow));


            CreateMap<PdfMediaEntity, MediaServiceObject>()
                .ForMember(s => s.MediaType, obj => obj.MapFrom(s => MediaAssetEntity.MediaType.Pdf));

            CreateMap<MediaServiceObject, MediaAssetEntity>()
                .ConvertUsing<MediaConverter>();
        }

        class MediaConverter : ITypeConverter<MediaServiceObject, MediaAssetEntity>
        {
            public MediaAssetEntity Convert(MediaServiceObject source, MediaAssetEntity destination, ResolutionContext context)
            {
                switch (source.MediaType)
                {
                    case MediaAssetEntity.MediaType.Image:
                        return context.Mapper.Map<ImageMediaEntity>(source);
                    case MediaAssetEntity.MediaType.Video:
                        return context.Mapper.Map<VideoMediaEntity>(source);
                    case MediaAssetEntity.MediaType.Pdf:
                        return context.Mapper.Map<PdfMediaEntity>(source);
                    case MediaAssetEntity.MediaType.Unknow:
                        return context.Mapper.Map<FileAssetEntity>(source);
                }

                return null;
            }
        }
    }
}
