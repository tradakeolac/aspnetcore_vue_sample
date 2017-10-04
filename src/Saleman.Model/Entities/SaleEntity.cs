namespace Saleman.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SaleEntity : EntityBase<long>
    {
        [Required]
        public virtual Guid ProductId { get; set; }
        public virtual ProductEntity Product { get; set; }
        public virtual decimal Discount { get; set; }
        public virtual Guid StoreId { get; set; }
        public virtual StoreEntity Store { get; set; }
        public virtual ICollection<SaleAuditEntity> Audits { get; set; }
    }
}
