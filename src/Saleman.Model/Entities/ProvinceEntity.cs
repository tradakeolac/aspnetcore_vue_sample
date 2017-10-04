namespace Saleman.Model.Entities
{
    using System.Collections;
    using System.Collections.Generic;


    public class ProvinceEntity : LocationEntity
    {
        public virtual ICollection<DistrictEntity> Districts { get; set; }
    }
}