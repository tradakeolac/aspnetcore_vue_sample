namespace Saleman.Web.ViewModel
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CategoryViewModel : ViewModelBase<Guid>
    {
        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        public Guid? ImageId { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public Guid StoreId { get; set; }
        public bool ShowOnMenu { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }

    public class CategoryFullInformationViewModel : CategoryViewModel
    {
        public string StoreName { get; set; }
        public string ParentCategoryName { get; set; }
    }
}
