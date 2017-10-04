namespace Saleman.Service.Queries
{
    using Data.Specifications;
    using Model.Entities;
    using System;
    using System.Linq.Expressions;

    public class GetSaleAuditByIdQuery : ExpressionQueryBaseSpecification<SaleAuditEntity>
    {
        readonly Guid auditId;

        public GetSaleAuditByIdQuery(Guid auditId)
        {
            this.auditId = auditId;
        }

        public override Expression<Func<SaleAuditEntity, bool>> Expression => audit => audit.Id == auditId;
    }
}
