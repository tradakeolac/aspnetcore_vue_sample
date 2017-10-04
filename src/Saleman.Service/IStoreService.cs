namespace Saleman.Service
{
    using Model.ServiceObjects;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IStoreService : IService<StoreServiceObject, Guid>
    {
        Task<IEnumerable<StoreServiceObject>> GetStoreBy(string userId);
    }
}
