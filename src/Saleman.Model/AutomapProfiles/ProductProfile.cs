namespace Saleman.Model.AutomapProfiles
{
    using AutoMapper;
    using Entities;
    using Saleman.Model.ServiceObjects;
    using System;

    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            this.CreateMap<ProductEntity, ProductServiceObject>()
                .ForMember(m => m.UnitId, obj => obj.MapFrom(src => src.Detail != null ? src.Detail.UnitId : null))
                .ForMember(m => m.CostPerUnit, obj => obj.MapFrom(src => src.Detail != null ? src.Detail.CostPerUnit : null))
                .ForMember(m => m.ProductDetailId, obj => obj.MapFrom(src => src.Detail != null ? src.Detail.Id : default(Guid?)));


            this.CreateMap<ProductServiceObject, ProductDetailEntity>()
                .ForMember(p => p.Unit, obj => obj.Ignore())
                .ForMember(p => p.Id, obj => obj.MapFrom(src => src.ProductDetailId))
                .ForMember(p => p.ProductId, obj => obj.MapFrom(src => src.Id))
                .ForMember(p => p.Product, obj => obj.Ignore())
                .ForMember(p => p.MediaAssets, obj => obj.Ignore());

            this.CreateMap<ProductServiceObject, ProductEntity>()
                .ForMember(p => p.Avatar, obj => obj.Ignore())
                .ForMember(p => p.Store, obj => obj.Ignore())
                .ForMember(p => p.Detail, obj => obj.Ignore())
                .ForMember(p => p.Category, obj => obj.Ignore())
                .ForMember(p => p.Sale, obj => obj.Ignore());
        }
    }
}