using Saleman.Data.Specifications;
using Saleman.Model.Entities;
using Saleman.Service.Queries.Products;
using System;
using Xunit;

namespace Saleman.Service.Tests
{
    public class FilterProductInPriceRangeQueryTests
    {
        [Fact]
        public void ProductDetail_In_Range_Should_Be_Valid()
        {
            // Arrange
            ISpecification<ProductDetailEntity> query = new FilterProductInPriceRangeQuery(100, 200);

            var satisfiedProduct = new ProductDetailEntity() { CostPerUnit = 150 };

            // Act
            var isSatisfied = query.IsSatisfiedBy(satisfiedProduct);
            
            // Assert
            Assert.True(isSatisfied, string.Format("The product should be valid with price {0}, with filter in range {1}-{2}", satisfiedProduct.CostPerUnit, 100, 200));
        }

        [Fact]
        public void ProductDetail_In_Range_Should_Be_In_Valid()
        {
            // Arrange
            ISpecification<ProductDetailEntity> query = new FilterProductInPriceRangeQuery(100, 200);

            var satisfiedProduct = new ProductDetailEntity() { CostPerUnit = 250 };

            // Act
            var isSatisfied = query.IsSatisfiedBy(satisfiedProduct);

            // Assert
            Assert.False(isSatisfied, string.Format("The product should be invalid with price {0}, with filter in range {1}-{2}", satisfiedProduct.CostPerUnit, 100, 200));
        }
    }
}
