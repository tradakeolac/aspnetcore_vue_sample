namespace Saleman.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductDetailEntity : EntityBase<Guid>
    {
        public virtual Guid? UnitId { get; set; }
        public virtual UnitEntity Unit { get; set; }
        public virtual decimal? CostPerUnit { get; set; }

        [Required]
        public virtual Guid ProductId { get; set; }
        public virtual ProductEntity Product { get; set; }

        public virtual ICollection<MediaProductEntity> MediaAssets { get; set; }
    }

    public class MediaProductEntity : EntityBase
    {
        public virtual Guid MediaId { get; set; }
        public virtual MediaAssetEntity Media { get; set; }
        public virtual Guid ProductDetailId { get; set; }
        public virtual ProductDetailEntity ProductDetail { get; set; }
    }
}
