
namespace Saleman.Model.ServiceObjects
{
    using System;

    public class CategoryServiceObject : ServiceObjectBase<Guid, CategoryServiceObject>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? ImageId { get; set; }
        public string ImageUrl { get; set; }
        public Guid StoreId { get; set; }
        public bool ShowOnMenu { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }

    public class CategoryWithFullInformationServiceObject : CategoryServiceObject
    {
        public string StoreName { get; set; }
        public string ParentCategoryName { get; set; }
    }
}
