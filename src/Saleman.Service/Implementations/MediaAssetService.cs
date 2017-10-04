namespace Saleman.Service.Implementations
{
    using Model.Entities;
    using Model.ServiceObjects;
    using System;
    using Data.Repositories;
    using WebFramework.Infrastructure.Configurations;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Saleman.Data.Specifications;
    using System.Linq;

    public class MediaAssetService : SalemanServiceBase<MediaServiceObject, Guid, MediaAssetEntity>, IMediaAssetService
    {
        protected readonly IMediaAssetRepository MediaRepository;
        protected readonly IMediaStorageService MediaStorageService;

        public MediaAssetService(IAsyncUnitOfWork unitOfWork, IAsyncDataLoaderRepository dataLoader,
            IWebFrameworkConfiguration configuration, IMediaAssetRepository repository,
            IServiceObjectFactory objectFactory, IMediaAssetEntityFactory entityFactory,
            IMediaStorageService mediaStorage)
            : base(unitOfWork, dataLoader, configuration, repository, objectFactory, entityFactory)
        {
            this.MediaRepository = repository;
            this.MediaStorageService = mediaStorage;
        }

        public async override Task<MediaServiceObject> AddAsync(MediaServiceObject serviceObject)
        {
            var streamableServiceObject = serviceObject as MediaStreamableServiceObject;
            if (streamableServiceObject != null)
            {
                var mediaServiceObject = await this.MediaStorageService.StoreAsync(streamableServiceObject);
                if (!mediaServiceObject.IsNullObject())
                {
                    // update link & file name
                    serviceObject.Link = mediaServiceObject.Link;
                    serviceObject.FileName = mediaServiceObject.FileName;

                    return await base.AddAsync(serviceObject);
                }
            }

            return await Task.FromResult(MediaServiceObject.NullServiceObject);
        }

        public async override Task<MediaServiceObject> UpdateAsync(MediaServiceObject serviceObject)
        {
            var streamableServiceObject = serviceObject as MediaStreamableServiceObject;
            if (streamableServiceObject != null)
            {
                var objectToDelete = await this.GetByIdAsync(serviceObject.Id);

                if (!objectToDelete.IsNullObject())
                {
                    var deleteStatus = await this.MediaStorageService.DeleteAsync(this.ObjectFactory.Create<MediaStreamableServiceObject>(objectToDelete));

                    if (deleteStatus)
                    {
                        var storeResult = await this.MediaStorageService.StoreAsync(streamableServiceObject);
                        if (!storeResult.IsNullObject())
                        {
                            serviceObject.Link = storeResult.Link;
                            serviceObject.FileName = storeResult.FileName;
                            
                            return await base.UpdateAsync(serviceObject);
                        }
                    }
                }
            }

            return await Task.FromResult(MediaServiceObject.NullServiceObject);
        }

        public async override Task<ResultServiceObject> DeleteAsync(Guid id)
        {
            var serviceObject = await this.GetByIdAsync(id);
            if (!serviceObject.IsNullObject())
            {
                var deleteStatus = await this.MediaStorageService.DeleteAsync(serviceObject);

                if (deleteStatus)
                {
                    return await base.DeleteAsync(id);
                }
            }

            return await Task.FromResult(ResultServiceObject.Fail);
        }

        public override async Task<ResultServiceObject> DeleteAsync(IEnumerable<Guid> ids)
        {
            if(ids != null && ids.Any())
            {
                foreach(var id in ids)
                {
                    var serviceObject = await this.GetByIdAsync(id);
                    if(!serviceObject.IsNullObject())
                    {
                        var deleteStatus = await this.MediaStorageService.DeleteAsync(serviceObject);

                        if (deleteStatus)
                        {
                            await base.DeleteAsync(id);
                        }
                    }
                }

                return await Task.FromResult(ResultServiceObject.Success);
            }

            return await Task.FromResult(ResultServiceObject.Fail);
        }

        public async Task<IEnumerable<MediaServiceObject>> GetMediaByUserIdAsync(string userId)
        {
            ISpecification<MediaAssetEntity> queryByUserId = new ExpressionSpecification<MediaAssetEntity>(s => s.CreatedById == userId);

            var media = await this.MediaRepository.FindAsync<MediaAssetEntity>(queryByUserId);

            if(media != null && media.Any())
            {
                return media.Select(ObjectFactory.Create<MediaServiceObject>);
            }

            return Enumerable.Empty<MediaServiceObject>();
        }
    }
}
