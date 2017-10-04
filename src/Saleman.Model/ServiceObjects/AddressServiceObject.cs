namespace Saleman.Model.ServiceObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class LocationServiceObjectBase<TNullServiceObject> : ServiceObjectBase<Guid, TNullServiceObject>
        where TNullServiceObject : LocationServiceObjectBase<TNullServiceObject>, new()
    {
        public Guid? ParentLocationId { get; set; }
    }

    public class LocationServiceObject : LocationServiceObjectBase<LocationServiceObject>
    {
        public string Name { get; set; }
    }

    public class AddressServiceObject : LocationServiceObjectBase<AddressServiceObject>
    {
        public string Address { get; set; }

        public string Lane { get; set; }
        public LocationServiceObject District { get; set; }
        public LocationServiceObject Province { get; set; }
    }
}
