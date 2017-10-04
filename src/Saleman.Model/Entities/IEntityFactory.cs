using System;

namespace Saleman.Model.Entities
{
    public interface IEntityFactory
    {
        TEntity Create<TEntity>(object serviceObject) where TEntity : class;
    }

    public interface IMediaAssetEntityFactory : IEntityFactory
    {
    }

    public interface IFileEntityFactory : IEntityFactory
    {

    }

    public interface ISaleAuditTransformer
    {
        TSaleAuditEntity Transform<TSaleAuditEntity>(SaleAuditEntity source) where TSaleAuditEntity : SaleAuditEntity;
    }
}