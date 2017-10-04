namespace Saleman.Service
{
    using Saleman.Model.ServiceObjects;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IService
    {

    }

    public interface IService<TServiceObject, TKey> : IService
        where TServiceObject : ServiceObjectBase<TKey, TServiceObject>, new()
    {
        Task<TServiceObject> AddAsync(TServiceObject serviceObject);
        Task<IEnumerable<TServiceObject>> GetAllAsync();
        Task<TServiceObject> GetByIdAsync(TKey id);
        Task<TServiceObject> UpdateAsync(TServiceObject serviceObject);
        Task<ResultServiceObject> DeleteAsync(TKey id);
        Task<ResultServiceObject> DeleteAsync(IEnumerable<TKey> ids);
    }
}