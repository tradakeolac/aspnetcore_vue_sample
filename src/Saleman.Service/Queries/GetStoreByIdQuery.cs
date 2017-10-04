namespace Saleman.Service.Queries
{
    using Data.Specifications;
    using Model.Entities;
    using System;
    using System.Linq.Expressions;

    public class GetStoreByIdQuery : ExpressionQueryBaseSpecification<StoreEntity>
    {
        readonly Guid storeId;

        public GetStoreByIdQuery(Guid storeId)
        {
            this.storeId = storeId;
        }

        public override Expression<Func<StoreEntity, bool>> Expression => store => store.Id == storeId;
    }
}
