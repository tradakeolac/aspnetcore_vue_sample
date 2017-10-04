namespace Saleman.Service.Queries.Categories
{
    using Saleman.Data.Specifications;
    using Saleman.Model.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;


    public class GetCategoriesByStoresQuery : ExpressionQueryBaseSpecification<CategoryEntity>
    {
        protected readonly IEnumerable<Guid> Stores;

        public GetCategoriesByStoresQuery(IEnumerable<Guid> stores)
        {
            this.Stores = stores;
        }

        public override Expression<Func<CategoryEntity, bool>> Expression => (category) => this.Stores.Contains(category.StoreId);
    }
}
