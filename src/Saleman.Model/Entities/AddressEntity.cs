namespace Saleman.Model.Entities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddressEntity : EntityBase<Guid>
    {
        [MaxLength(300)]
        [Required]
        public virtual string Address { get; set; }

        [MaxLength(300)]
        public virtual string Lane { get; set; }

        [Required]
        public virtual Guid DistrictId { get; set; }
        public virtual DistrictEntity District { get; set; }
        public virtual ICollection<StoreAddressEntity> StoreAddress { get; set; }
    }
}
