namespace Saleman.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class StoreEntity : EntityBase<Guid>
    {
        [Required]
        [MaxLength(300)]
        public virtual string Name { get; set; }

        [MaxLength(1000)]
        public virtual string Description { get; set; }

        public virtual StoreDetailEntity Detail { get; set; }
    }

    public class StoreDetailEntity : EntityBase<Guid>
    {
        public virtual StoreEntity Store { get; set; }
        
        [Required]
        public virtual string OwnerId { get; set; }
        public virtual UserEntity Owner { get; set; }

        public virtual ICollection<ProductEntity> Products { get; set; }
        public virtual ICollection<StoreAddressEntity> StoreAddress { get; set; }
        public virtual ICollection<AdditionalInformationEntity> AdditionalInformation { get; set; }
    }
}
