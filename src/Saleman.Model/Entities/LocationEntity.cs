using System;
using System.ComponentModel.DataAnnotations;

namespace Saleman.Model.Entities
{

    public abstract class LocationEntity : EntityBase<Guid>
    {
        [Required]
        [MaxLength(300)]
        public virtual string Name { get; set; }
    }


    public abstract class LocationEntity<TLocation> : LocationEntity
        where TLocation : LocationEntity
    {
        public virtual Guid? ParentLocationId { get; set; }
        public virtual TLocation ParentLocation { get; set; }
    }

    public enum LocationType
    {
        District = 1,
        Province = 2,
    }
}