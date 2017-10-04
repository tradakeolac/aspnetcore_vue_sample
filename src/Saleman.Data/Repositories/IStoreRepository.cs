namespace Saleman.Data.Repositories
{
    using Saleman.Model.Entities;
    using Specifications;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IStoreRepository : IRepository
    {
        Task<IEnumerable<StoreEntity>> GetStoresWithDetailAsync(ISpecification<StoreEntity> criteria);
        Task<StoreEntity> GetStoreWithOwnerByAsync(ISpecification<StoreEntity> criteria);
    }
}