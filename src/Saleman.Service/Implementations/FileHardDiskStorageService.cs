namespace Saleman.Service.Implementations
{
    using System;
    using System.Threading.Tasks;
    using Model.ServiceObjects;
    using System.IO;
    using Microsoft.Extensions.FileProviders;
    using WebFramework.Infrastructure.Configurations;

    public class FileHardDiskStorageService : IMediaStorageService
    {
        protected readonly IWebFrameworkConfiguration Configuration;
        protected readonly IFileProvider FileProvider;

        public FileHardDiskStorageService(IWebFrameworkConfiguration configuration, IFileProvider fileProvider)
        {
            this.Configuration = configuration;
            this.FileProvider = fileProvider;
        }

        public async Task<bool> DeleteAsync(MediaServiceObject mediaServiceObject)
        {
            if (mediaServiceObject == null)
                return await Task.FromResult(false);

            if (mediaServiceObject.Id != Guid.Empty)
            {
                var file = FileProvider.GetFileInfo(GetRelativePath(mediaServiceObject));
                if (file.Exists)
                {
                    try
                    {
                        File.Delete(file.PhysicalPath);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public async Task<MediaServiceObject> StoreAsync(IStreamableServiceObject mediaServiceObject)
        {
            if (mediaServiceObject == null)
                return await Task.FromResult(MediaServiceObject.NullServiceObject);

            var savedMediaObject = new MediaServiceObject
            {
                // Update unique file name
                FileName = Guid.NewGuid().ToString() + "_" + mediaServiceObject.Name
            };

            using (var stream = new FileStream(GetPhysicalPath(savedMediaObject), FileMode.Create))
            {
                await mediaServiceObject.CopyToAsync(stream);
            }

            // Update link
            savedMediaObject.Link = Path.Combine("\\", GetRelativePath(savedMediaObject));

            return await Task.FromResult(savedMediaObject);
        }

        private string GetPhysicalPath(MediaServiceObject media)
        {
            return Path.Combine(this.FileProvider.GetFileInfo("/").PhysicalPath, GetRelativePath(media));
        }

        private string GetRelativePath(MediaServiceObject media) { return  Path.Combine(Configuration.FileStorageFolder, media.FileName); }
    }
}
