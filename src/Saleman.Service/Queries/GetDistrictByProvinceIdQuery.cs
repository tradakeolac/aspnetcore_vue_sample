
namespace Saleman.Service.Queries
{
    using Model.Entities;
    using Saleman.Data.Specifications;
    using System;
    using System.Linq.Expressions;

    public class GetDistrictByProvinceIdQuery : ExpressionQueryBaseSpecification<DistrictEntity>
    {
        readonly Guid provinceId;

        public GetDistrictByProvinceIdQuery(Guid provinceId)
        {
            this.provinceId = provinceId;
        }

        public override Expression<Func<DistrictEntity, bool>> Expression => district => district.ParentLocationId == provinceId;
    }
}
