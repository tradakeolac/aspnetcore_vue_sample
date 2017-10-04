using System;

namespace Saleman.Model.ServiceObjects
{

    public class StoreServiceObject : ServiceObjectBase<Guid, StoreServiceObject>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string OwnerId { get; set; }
    }
}