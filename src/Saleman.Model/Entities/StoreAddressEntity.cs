namespace Saleman.Model.Entities
{
    using System;

    public class StoreAddressEntity
    {
        public virtual Guid StoreDetailId { get; set; }
        public virtual StoreDetailEntity StoreDetail { get; set; }

        public virtual Guid AddressId { get; set; }
        public virtual AddressEntity Address { get; set; }
    }
}
