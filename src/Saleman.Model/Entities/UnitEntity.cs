namespace Saleman.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class UnitEntity : EntityBase<Guid>
    {
        [Required]
        [MaxLength(300)]
        public virtual string Name { get; set; }

        [MaxLength(10)]
        public virtual string Symbol { get; set; }

        [MaxLength(1000)]
        public virtual string Description { get; set; }
    }
}
