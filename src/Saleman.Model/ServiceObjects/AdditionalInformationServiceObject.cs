namespace Saleman.Model.ServiceObjects
{
    using System;

    public class AdditionalInformationServiceObject : ServiceObjectBase<Guid, AdditionalInformationServiceObject>
    {
        public string Name { get; set; }

        public string Information { get; set; }

        public Guid StoreDetailId { get; set; }

        public string Type { get; set; }
    }
}
