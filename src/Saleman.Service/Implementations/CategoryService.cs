namespace Saleman.Service.Implementations
{
    using Saleman.Model.Entities;
    using Saleman.Model.ServiceObjects;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Saleman.Data.Repositories;
    using WebFramework.Infrastructure.Configurations;
    using System.Threading.Tasks;
    using Saleman.Data.Specifications;
    using Saleman.Service.Queries.Products;
    using System.Linq;
    using Saleman.Service.Queries.Categories;

    public class CategoryService : SalemanServiceBase<CategoryServiceObject, Guid, CategoryEntity>, ICategoryService
    {
        protected readonly ICategoryRepository CategoryRepository;
        protected readonly IStoreService StoreService;

        public CategoryService(IAsyncUnitOfWork unitOfWork, ICategoryRepository dataLoader,
            IWebFrameworkConfiguration configuration, ICategoryRepository repository, IServiceObjectFactory objectFactory,
            IEntityFactory entityFactory, IStoreService storeService)
            : base(unitOfWork, dataLoader, configuration, repository, objectFactory, entityFactory)
        {
            this.CategoryRepository = repository;
            this.StoreService = storeService;
        }

        public async Task<IEnumerable<CategoryServiceObject>> GetCategoryByStoreAsync(Guid storeId)
        {
            ISpecification<CategoryEntity> query = new GetCategoryByStoreIdQuery(storeId);

            var categories = await this.AsyncDataLoader.FindAsync(query);

            if(categories != null && categories.Any())
            {
                return categories.Select(ObjectFactory.Create<CategoryServiceObject>);
            }

            return Enumerable.Empty<CategoryServiceObject>();
        }

        public async Task<IEnumerable<CategoryServiceObject>> GetCategoryByUserIdAsync(string userId)
        {
            var stores = await this.StoreService.GetStoreBy(userId);

            if(stores != null && stores.Any())
            {
                var storeIds = stores.Select(s => s.Id).ToList();

                ISpecification<CategoryEntity> getCategoriesBelongStoresQuery = new GetCategoriesByStoresQuery(storeIds);

                var categories = await this.CategoryRepository.FindCategoryWithReferencesAsync(getCategoriesBelongStoresQuery);

                if(categories != null && categories.Any())
                {
                    return categories.Select(ObjectFactory.Create<CategoryWithFullInformationServiceObject>);
                }
            }

            return Enumerable.Empty<CategoryServiceObject>();
        }

        public async Task<IEnumerable<CategoryServiceObject>> GetCategoryShownOnMenuAsync()
        {
            ISpecification<CategoryEntity> query = new CategoryShownOnMenuQuery();

            var categories = await this.CategoryRepository.FindCategoryWithSubCategoriesAsync(query);

            if(categories != null && categories.Any())
            {
                return categories.Select(this.ObjectFactory.Create<CategoryServiceObject>);
            }

            return Enumerable.Empty<CategoryServiceObject>();
        }
    }
}
