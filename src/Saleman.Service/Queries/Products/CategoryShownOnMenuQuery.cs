namespace Saleman.Service.Queries.Products
{
    using Saleman.Data.Specifications;
    using Saleman.Model.Entities;
    using System;
    using System.Linq.Expressions;

    public sealed class CategoryShownOnMenuQuery : ExpressionQueryBaseSpecification<CategoryEntity>
    {
        public override Expression<Func<CategoryEntity, bool>> Expression => c => c.ShowOnMenu;
    }
}
