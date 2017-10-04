namespace Saleman.Service.Implementations
{
    using Data.Repositories;
    using WebFramework.Infrastructure.Configurations;
    using Model.Entities;
    using Model.ServiceObjects;
    using System;
    using System.Threading.Tasks;
    using Exceptions;

    public class SaleAuditService : SalemanServiceBase<SaleAuditServiceObject, Guid, SaleAuditEntity>, ISaleAuditService
    {
        protected readonly ISaleService SaleService;
        protected readonly ISaleAuditRepository SaleAuditRepository;
        protected readonly IStoreRepository StoreRepository;
        protected readonly ISaleAuditTransformer SaleAuditTransformer;

        public SaleAuditService(IAsyncUnitOfWork unitOfWork, IAsyncDataLoaderRepository dataLoader,
            IWebFrameworkConfiguration configuration, ISaleAuditRepository repository, IServiceObjectFactory objectFactory,
            IEntityFactory entityFactory, ISaleService saleService, IStoreRepository storeRepository,
            ISaleAuditTransformer transformer)
            : base(unitOfWork, dataLoader, configuration, repository, objectFactory, entityFactory)
        {
            this.SaleService = saleService;
            this.SaleAuditRepository = repository;
            this.StoreRepository = storeRepository;
            this.SaleAuditTransformer = transformer;
        }

        public async override Task<SaleAuditServiceObject> AddAsync(SaleAuditServiceObject serviceObject)
        {
            var sale = await this.SaleService.GetByIdAsync(serviceObject.SaleId);

            if (sale.IsNullObject())
                return SaleAuditServiceObject.NullServiceObject;

            var saleAudit = this.EntityFactory.Create<PendingSaleAuditEntity>(serviceObject);

            this.Repository.Add(saleAudit);

            await this.UnitOfWork.SaveChangeAsync();

            // update id
            serviceObject.Id = saleAudit.Id;

            return serviceObject;
        }

        public async Task<bool> ApproveAsync(Guid saleAuditId, string storeOwnerId)
        {
            var saleAuditByIdQuery = new Queries.GetSaleAuditByIdQuery(saleAuditId);
            var audit = await this.SaleAuditRepository.GetSaleAuditWithSaleByAsync(saleAuditByIdQuery);

            if (audit == null)
                throw new NotFoundObjectException("Can not find the SaleAudit", null);

            var storeByIdQuery = new Queries.GetStoreByIdQuery(audit.Sale.StoreId);

            var store = await this.StoreRepository.GetStoreWithOwnerByAsync(storeByIdQuery);

            if (store == null || store.Detail == null)
                throw new NotFoundObjectException("Can not find the Store", null);

            if (store.Detail.OwnerId.Equals(storeOwnerId, StringComparison.OrdinalIgnoreCase))
            {
                var approveEntity = this.SaleAuditTransformer.Transform<ApprovedSaleAuditEntity>(audit);

                this.SaleAuditRepository.Update(approveEntity);

                await this.UnitOfWork.SaveChangeAsync();

                return true;
            }

            return false;
        }
    }
}