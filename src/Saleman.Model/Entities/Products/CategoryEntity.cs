namespace Saleman.Model.Entities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CategoryEntity : EntityBase<Guid>
    {
        [MaxLength(300)]
        [Required]
        public virtual string Name { get; set; }

        [MaxLength(1000)]
        public virtual string Description { get; set; }

        public virtual bool ShowOnMenu { get; set; }

        public virtual ImageMediaEntity Avatar { get; set; }

        public virtual Guid? AvatarId { get; set; }

        public virtual Guid? ParentCategoryId { get; set; }
        public virtual CategoryEntity ParentCategory { get; set; }

        public virtual ICollection<CategoryEntity> SubCategories { get; set; }

        [Required]
        public virtual Guid StoreId { get; set; }
        public virtual StoreEntity Store { get; set; }

    }
}
