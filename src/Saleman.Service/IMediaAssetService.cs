namespace Saleman.Service
{
    using Model.ServiceObjects;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Define interface to work with multi-media asset (Video, Image..)
    /// </summary>
    public interface IMediaAssetService : IService<MediaServiceObject, Guid>
    {
        Task<IEnumerable<MediaServiceObject>> GetMediaByUserIdAsync(string userId);
    }

    public interface IMediaStorageService
    {
        Task<MediaServiceObject> StoreAsync(IStreamableServiceObject mediaServiceObject);
        Task<bool> DeleteAsync(MediaServiceObject mediaServiceObject);
    }
}
