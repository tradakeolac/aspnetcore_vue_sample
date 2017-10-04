namespace Saleman.Model.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ProductEntity : EntityBase<Guid>
    {
        [Required]
        [MaxLength(300)]
        public virtual string Name { get; set; }

        [MaxLength(1000)]
        public virtual string Description { get; set; }

        public virtual ImageMediaEntity Avatar { get; set; }

        public virtual Guid? AvatarId { get; set; }

        [Required]
        public virtual Guid StoreId { get; set; }
        public virtual StoreEntity Store { get; set; }

        [Required]
        public virtual Guid CategoryId { get; set; }
        public virtual CategoryEntity Category { get; set; }

        public virtual ProductDetailEntity Detail { get; set; }
        public virtual SaleEntity Sale { get; set; }
    }
}
