namespace Saleman.Web.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class StoreViewModel : ViewModelBase<Guid>
    {
        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public string OwnerId { get; set; }

        public ICollection<AddressViewModel> Addresses { get; set; }
    }
}