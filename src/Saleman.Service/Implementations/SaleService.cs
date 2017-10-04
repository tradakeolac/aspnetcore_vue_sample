namespace Saleman.Service.Implementations
{
    using Model.Entities;
    using Model.ServiceObjects;
    using Data.Repositories;
    using WebFramework.Infrastructure.Configurations;

    public class SaleService : SalemanServiceBase<SaleServiceObject, long, SaleEntity>, ISaleService
    {
        protected readonly ISaleCodeService SaleCodeService;

        public SaleService(IAsyncUnitOfWork unitOfWork, IAsyncDataLoaderRepository dataLoader,
            IWebFrameworkConfiguration configuration, IRepository repository, IServiceObjectFactory objectFactory,
            IEntityFactory entityFactory, ISaleCodeService saleCodeService)
            : base(unitOfWork, dataLoader, configuration, repository, objectFactory, entityFactory)
        {
            this.SaleCodeService = saleCodeService;
        }
    }
}
