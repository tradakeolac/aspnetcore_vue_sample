using System;

namespace Saleman.Model.Entities
{

    public interface IAuditableEntity
    {
        DateTime Created { get; set; }
        DateTime Updated { get; set; }
    }
}