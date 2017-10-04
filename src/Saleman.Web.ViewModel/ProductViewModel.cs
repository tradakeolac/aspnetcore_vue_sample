namespace Saleman.Web.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ProductViewModel : ViewModelBase<Guid>
    {
        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public Guid? AvatarId { get; set; }

        public Guid StoreId { get; set; }

        public Guid? UnitId { get; set; }

        public decimal? CostPerUnit { get; set; }
    }
}
