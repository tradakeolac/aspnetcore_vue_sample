namespace Saleman.Data.Repositories
{
    using Saleman.Data.Specifications;
    using Saleman.Model.Entities;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICategoryRepository : IRepository, IAsyncDataLoaderRepository
    {
        Task<IEnumerable<CategoryEntity>> FindCategoryWithSubCategoriesAsync(ISpecification<CategoryEntity> criteria);
        Task<IEnumerable<CategoryEntity>> FindCategoryWithReferencesAsync(ISpecification<CategoryEntity> criteria);
    }
}
