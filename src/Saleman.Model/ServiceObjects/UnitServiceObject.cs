namespace Saleman.Model.ServiceObjects
{
    using System;

    public class UnitServiceObject : ServiceObjectBase<Guid, UnitServiceObject>
    {
        public virtual string Name { get; set; }

        public virtual string Symbol { get; set; }

        public virtual string Description { get; set; }
    }
}
