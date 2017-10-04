namespace Saleman.Service
{
    using Data.Repositories;
    using WebFramework.Infrastructure.Configurations;
    using Model.ServiceObjects;
    using Model.Entities;

    public abstract class AuthenticationServiceBase : SalemanServiceBase
    {
        protected AuthenticationServiceBase(IAsyncUnitOfWork unitOfWork, IAsyncDataLoaderRepository dataLoader,
            IWebFrameworkConfiguration configuration, IRepository repository, IServiceObjectFactory objectFactory,
            IEntityFactory entityFactory) : base(unitOfWork, dataLoader, configuration, repository, objectFactory, entityFactory)
        {
        }        
    }
}