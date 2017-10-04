namespace Saleman.Web.Infrastructure.AutomapProfiles
{
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Model.ServiceObjects;
    using Saleman.Web.ViewModel;

    public class MediaProfile : Profile
    {
        public MediaProfile()
        {
            this.CreateMap<IFormFile, MediaServiceObject>()
                .ConstructUsing(CreateMediaStream)
                .ForAllMembers(src => src.Ignore());

            this.CreateMap<MediaServiceObject, MediaViewModel>();
            this.CreateMap<MediaServiceObject, MediaStreamableServiceObject>();
        }

        private static MediaServiceObject CreateMediaStream(IFormFile formFile)
        {
            if (formFile == null)
                return MediaServiceObject.NullServiceObject;

            return new MediaStreamableServiceObject(formFile.CopyToAsync)
            {
                Name = formFile.FileName
            };
        }
    }
}
