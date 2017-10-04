
namespace Saleman.Service.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Model.ServiceObjects;
    using Data.Repositories;
    using WebFramework.Infrastructure.Configurations;
    using Model.Entities;
    using Exceptions;

    public class UnitService : SalemanServiceBase<UnitServiceObject, Guid, UnitEntity>, IUnitService
    {
        protected readonly IUnitRepository UnitRepository;

        public UnitService(IAsyncUnitOfWork unitOfWork, IAsyncDataLoaderRepository dataLoader,
            IWebFrameworkConfiguration configuration, IServiceObjectFactory objectFactory,
            IEntityFactory entityFactory, IUnitRepository unitRepository)
            : base(unitOfWork, dataLoader, configuration, unitRepository, objectFactory, entityFactory)
        {

        }
    }
}
