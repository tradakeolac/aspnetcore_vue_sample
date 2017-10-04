namespace Saleman.Service.Queries.Products
{
    using Saleman.Data.Specifications;
    using Saleman.Model.Entities;
    using System;
    using System.Linq.Expressions;

    public sealed class SearchProductByNameQuery
        : ExpressionQueryBaseSpecification<ProductEntity>
    {
        readonly string SearchText;

        public SearchProductByNameQuery(string searchText)
        {
            this.SearchText = searchText;
        }

        public override Expression<Func<ProductEntity, bool>> Expression => product => product.Name == SearchText;
    }
}
