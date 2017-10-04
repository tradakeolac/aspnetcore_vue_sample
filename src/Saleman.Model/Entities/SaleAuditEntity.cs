namespace Saleman.Model.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class SaleAuditEntity : AuditableEntity
    {
        [Required]
        public virtual long SaleId { get; set; }
        public virtual SaleEntity Sale { get; set; }

        [Required]
        public virtual string SalemanId { get; set; }
        public virtual UserEntity Saleman { get; set; }

        [Required]
        public virtual decimal Total { get; set; }
    }

    public class PendingSaleAuditEntity : SaleAuditEntity
    {

    }

    public class ApprovedSaleAuditEntity : SaleAuditEntity
    {

    }
}
