namespace Saleman.Service.Queries.Categories
{
    using Saleman.Data.Specifications;
    using Saleman.Model.Entities;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Linq;

    public class GetCategoryByStoreIdQuery : ExpressionQueryBaseSpecification<CategoryEntity>
    {
        protected readonly Guid StoreId;

        public GetCategoryByStoreIdQuery(Guid storeId)
        {
            this.StoreId = storeId;
        }

        public override Expression<Func<CategoryEntity, bool>> Expression => (category) => category.StoreId == StoreId;
    }

}
