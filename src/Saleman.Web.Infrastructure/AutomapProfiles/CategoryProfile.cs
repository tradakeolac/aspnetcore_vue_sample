namespace Saleman.Web.Infrastructure.AutomapProfiles
{
    using AutoMapper;
    using Saleman.Model.ServiceObjects;
    using Saleman.Web.ViewModel;

    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            this.CreateMap<CategoryViewModel, CategoryServiceObject>();
            this.CreateMap<CategoryServiceObject, CategoryViewModel>();

            this.CreateMap<CategoryWithFullInformationServiceObject, CategoryFullInformationViewModel>()
                ;
        }
    }
}
