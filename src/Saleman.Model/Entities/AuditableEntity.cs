namespace Saleman.Model.Entities
{
    using System;

    public abstract class AuditableEntity : EntityBase<Guid>, IAuditableEntity
    {
        public virtual DateTime Created { get; set; }
        public virtual DateTime Updated { get; set; }
    }
}
