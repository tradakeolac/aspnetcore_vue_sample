namespace Saleman.Service.Queries.Stores
{
    using Saleman.Data.Specifications;
    using Saleman.Model.Entities;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq.Expressions;

    public class GetStoreByOwnerIdQuery : ExpressionQueryBaseSpecification<StoreEntity>
    {
        protected readonly string OwnerId;

        public GetStoreByOwnerIdQuery(string ownerId)
        {
            this.OwnerId = ownerId;
        }
        public override Expression<Func<StoreEntity, bool>> Expression => (store) => store.Detail.OwnerId == OwnerId;
    }
}
