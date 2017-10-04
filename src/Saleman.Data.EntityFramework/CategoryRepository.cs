namespace Saleman.Data.EntityFramework
{
    using Saleman.Data.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Saleman.Data.Specifications;
    using Saleman.Model.Entities;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class CategoryRepository : EFCoreRepository, ICategoryRepository
    {
        public CategoryRepository(SalemanDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<CategoryEntity>> FindCategoryWithReferencesAsync(ISpecification<CategoryEntity> criteria)
        {
            return await this.Find(criteria)
                .Include(c => c.Avatar)
                .Include(c => c.ParentCategory)
                .Include(c => c.Store)
                .ToListAsync();
        }

        public async Task<IEnumerable<CategoryEntity>> FindCategoryWithSubCategoriesAsync(ISpecification<CategoryEntity> criteria)
        {
            return await this.Find(criteria).Include(c => c.SubCategories).ToListAsync();
        }
    }
}
