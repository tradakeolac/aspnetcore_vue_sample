namespace Saleman.Model.ServiceObjects
{
    using System;

    public class ProductServiceObject : ServiceObjectBase<Guid, ProductServiceObject>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? AvatarId { get; set; }
        public Guid StoreId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? ProductDetailId { get; set; }
        public decimal? CostPerUnit { get; set; }
    }
}
