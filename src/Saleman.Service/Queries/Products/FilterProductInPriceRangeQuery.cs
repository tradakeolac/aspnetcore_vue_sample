namespace Saleman.Service.Queries.Products
{

    using Saleman.Data.Specifications;
    using Saleman.Model.Entities;
    using System;
    using System.Linq.Expressions;

    public sealed class FilterProductInPriceRangeQuery : ExpressionQueryBaseSpecification<ProductDetailEntity>
    {
        readonly decimal? LowestPrice;
        readonly decimal? HightestPrice;

        public FilterProductInPriceRangeQuery(decimal? lowestPrice = null, decimal? hightesPrices = null)
        {
            this.LowestPrice = lowestPrice;
            this.HightestPrice = hightesPrices;
        }

        public override Expression<Func<ProductDetailEntity, bool>> Expression
        {
            get
            {
                if (this.LowestPrice.HasValue && this.HightestPrice.HasValue)
                    return product => product.CostPerUnit >= LowestPrice.Value && product.CostPerUnit <= HightestPrice.Value;

                if (this.LowestPrice.HasValue)
                    return product => product.CostPerUnit >= LowestPrice.Value;

                if (this.HightestPrice.HasValue)
                    return product => product.CostPerUnit <= HightestPrice.Value;

                return product => product.CostPerUnit.HasValue;
            }
        }
    }
}
