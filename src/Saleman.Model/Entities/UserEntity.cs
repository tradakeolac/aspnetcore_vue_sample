namespace Saleman.Model.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class UserEntity : EntityBase<string>
    {
        public virtual ICollection<StoreDetailEntity> Stores { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}
