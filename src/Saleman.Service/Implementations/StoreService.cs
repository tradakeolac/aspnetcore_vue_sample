namespace Saleman.Service.Implementations
{
    using Model.Entities;
    using Model.ServiceObjects;
    using System;
    using System.Threading.Tasks;
    using Data.Repositories;
    using WebFramework.Infrastructure.Configurations;
    using System.Collections.Generic;
    using Saleman.Service.Queries.Stores;
    using System.Linq;
    using Saleman.Data.Exceptions;
    using Saleman.Service.Exceptions;

    public class StoreService : SalemanServiceBase<StoreServiceObject, Guid, StoreEntity>, IStoreService
    {
        protected readonly IStoreRepository StoreRepository;
        protected readonly IUserService UserService;

        public StoreService(IAsyncUnitOfWork unitOfWork, IAsyncDataLoaderRepository dataLoader,
            IWebFrameworkConfiguration configuration, IStoreRepository repository, IServiceObjectFactory objectFactory,
            IEntityFactory entityFactory, IUserService userService)
            : base(unitOfWork, dataLoader, configuration, repository, objectFactory, entityFactory)
        {
            this.StoreRepository = repository;
            this.UserService = userService;
        }

        public override async Task<StoreServiceObject> AddAsync(StoreServiceObject serviceObject)
        {
            var store = this.EntityFactory.Create<StoreEntity>(serviceObject);

            if (store == null)
                return StoreServiceObject.NullServiceObject;

            var detail = this.EntityFactory.Create<StoreDetailEntity>(serviceObject);

            if (detail == null)
                return StoreServiceObject.NullServiceObject;

            store.Detail = detail;

            this.StoreRepository.Add(store);

            await SaveChangeAsync<AddActionException>();

            return ObjectFactory.Create<StoreServiceObject>(store);
        }

        public async Task<IEnumerable<StoreServiceObject>> GetStoreBy(string userId)
        {
            if(!string.IsNullOrEmpty(userId))
            {
                var query = new GetStoreByOwnerIdQuery(userId);

                var stores = await this.StoreRepository.GetStoresWithDetailAsync(query);

                if (stores != null && stores.Any())
                    return stores.Select(ObjectFactory.Create<StoreServiceObject>);
            }

            return Enumerable.Empty<StoreServiceObject>();
        }
    }
}
