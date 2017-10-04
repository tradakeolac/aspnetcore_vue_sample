namespace Saleman.Service.Implementations
{
    using Saleman.Model.ServiceObjects;
    using Saleman.Model.Entities;
    using System;
    using Saleman.Data.Repositories;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Linq;
    using WebFramework.Infrastructure.Configurations;
    using Saleman.Service.Exceptions;
    using WebFramework.Infrastructure.Caching;
    using WebFramework.Infrastructure.DependencyInjection;

    public class AdditionalInformationService : SalemanServiceBase<AdditionalInformationServiceObject, Guid, AdditionalInformationEntity>, IAdditionalInformationService
    {
        protected readonly IAdditionalInformationRepository AdditionalInformationRepository;
        protected readonly IServiceResolver<IAsynCacheProvider> CacheProviderResolver;

        public AdditionalInformationService(IAsyncUnitOfWork unitOfWork, IAsyncDataLoaderRepository dataLoader,
            IWebFrameworkConfiguration configuration, IAdditionalInformationRepository repository, IServiceObjectFactory objectFactory,
            IServiceResolver<IAsynCacheProvider> asynCacheProviderResolver, IEntityFactory entityFactory) 
            : base(unitOfWork, dataLoader, configuration, repository, objectFactory, entityFactory)
        {
            this.AdditionalInformationRepository = repository;
            this.CacheProviderResolver = asynCacheProviderResolver;
        }

        public async Task<IEnumerable<AdditionalInformationServiceObject>> GetSocialInformationAsync(Guid storeId)
        {
            var key = string.Format("SocialInformationCollection_Store_{0}", storeId);
            var query = new Queries.AdditionalInformations.GetSocialMediaInformationByStoreIdQuery(storeId);

            try
            {
                var socialInformation = await this.GetDefaultCacheProvider()?.FetchAsync(key, () => this.AdditionalInformationRepository.FindAsync(query), null, null);

                return socialInformation != null ? socialInformation.Select(this.ObjectFactory.Create<AdditionalInformationServiceObject>) : Enumerable.Empty<AdditionalInformationServiceObject>();
            }
            catch(MappingException ex)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new GetActionException("Can not get the social information", ex);
            }
        }

        public async Task<IEnumerable<AdditionalInformationServiceObject>> GetStoreContactAsync(Guid storeId)
        {
            var query = new Queries.AdditionalInformations.GetStoreMediaInformationByStoreIdQuery(storeId);
            var key = string.Format("StoreContactCollection_Store_{0}", storeId);

            try
            {
                var contactInformation = await this.GetDefaultCacheProvider()?.FetchAsync(key, () => this.AdditionalInformationRepository.FindAsync(query), null, null);

                return contactInformation != null ? contactInformation.Select(this.ObjectFactory.Create<AdditionalInformationServiceObject>) : Enumerable.Empty<AdditionalInformationServiceObject>();
            }
            catch (MappingException ex)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new GetActionException("Cannot get the contact information.", ex);
            }
        }

        private IAsynCacheProvider GetDefaultCacheProvider() => this.CacheProviderResolver.Resolve(Configuration.CacheProvider);
    }
}
