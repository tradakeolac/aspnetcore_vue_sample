namespace Saleman.Web.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AddressViewModel : ViewModelBase<Guid>
    {
        [Required]
        [MaxLength(300)]
        public string Address { get; set; }

        [MaxLength(300)]
        public string Lane { get; set; }

        [Required]
        public Guid DistrictId { get; set; }

        public Guid ProvinceId { get; set; }
    }

    public class LocationViewModel : ViewModelBase<Guid>
    {
        [Required]
        [MaxLength(300)]
        public string Name { get; set; }
    }
}
