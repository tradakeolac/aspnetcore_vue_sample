namespace Saleman.Model.AutomapProfiles
{
    using AutoMapper;
    using Saleman.Model.Entities;
    using Saleman.Model.ServiceObjects;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            this.CreateMap<CategoryServiceObject, CategoryEntity>()
                .ForMember(m => m.Avatar, obj => obj.Ignore())
                .ForMember(m => m.AvatarId, obj => obj.MapFrom(src => src.ImageId))
                .ForMember(m => m.ParentCategory, obj => obj.Ignore())
                .ForMember(m => m.ParentCategoryId, obj => obj.MapFrom(src => src.ParentCategoryId))
                .ForMember(m => m.SubCategories, obj => obj.Ignore())
                .ForMember(m => m.Store, obj => obj.Ignore());

            this.CreateMap<CategoryEntity, CategoryServiceObject>()
                .ForMember(m => m.ImageId, obj => obj.MapFrom(src => src.AvatarId))
                .ForMember(m => m.ImageUrl, obj => obj.MapFrom(src => src.Avatar != null ? src.Avatar.Link : string.Empty));

            this.CreateMap<CategoryEntity, CategoryWithFullInformationServiceObject>()
                .ForMember(m => m.ImageId, obj => obj.MapFrom(src => src.AvatarId))
                .ForMember(m => m.ParentCategoryName, obj => obj.MapFrom(src => src.ParentCategory != null ? src.ParentCategory.Name : null))
                .ForMember(m => m.StoreName, obj => obj.MapFrom(src => src.Store != null ? src.Store.Name : null))
                .ForMember(m => m.ImageUrl, obj => obj.MapFrom(src => src.Avatar != null ? src.Avatar.Link : string.Empty));
        }
    }
}
