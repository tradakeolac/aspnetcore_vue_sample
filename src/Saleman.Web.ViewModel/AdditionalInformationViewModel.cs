namespace Saleman.Web.ViewModel
{
    using System;

    public class AdditionalInformationViewModel : ViewModelBase<Guid>
    {
        public string Name { get; set; }

        public string Information { get; set; }

        public Guid StoreDetailId { get; set; }

        public string Type { get; set; }
    }
}
