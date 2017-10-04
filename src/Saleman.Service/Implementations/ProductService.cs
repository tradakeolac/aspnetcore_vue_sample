namespace Saleman.Service.Implementations
{
    using Model.Entities;
    using Model.ServiceObjects;
    using System;
    using Data.Repositories;
    using WebFramework.Infrastructure.Configurations;

    public class ProductService : SalemanServiceBase<ProductServiceObject, Guid, ProductEntity>, IProductService
    {
        public ProductService(IAsyncUnitOfWork unitOfWork, IAsyncDataLoaderRepository dataLoader,
            IWebFrameworkConfiguration configuration, IRepository repository, IServiceObjectFactory objectFactory,
            IEntityFactory entityFactory) : base(unitOfWork, dataLoader, configuration, repository, objectFactory, entityFactory)
        {
        }
    }
}
