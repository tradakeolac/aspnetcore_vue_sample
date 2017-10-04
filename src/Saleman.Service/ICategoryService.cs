
namespace Saleman.Service
{
    using Saleman.Model.ServiceObjects;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICategoryService : IService<CategoryServiceObject, Guid>
    {
        Task<IEnumerable<CategoryServiceObject>> GetCategoryShownOnMenuAsync();
        Task<IEnumerable<CategoryServiceObject>> GetCategoryByStoreAsync(Guid storeId);
        Task<IEnumerable<CategoryServiceObject>> GetCategoryByUserIdAsync(string userId);
    }
}
