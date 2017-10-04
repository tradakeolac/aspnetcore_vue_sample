namespace Saleman.Model.AutomapProfiles
{
    using Saleman.Model.Entities;
    using Saleman.Model.ServiceObjects;

    public class StoreProfile : AutoMapper.Profile
    {
        public StoreProfile()
        {
            // StoreServiceObject -> Store Entity
            this.CreateMap<StoreServiceObject, StoreEntity>()
                .ForMember(m => m.Detail, obj => obj.Ignore());

            this.CreateMap<StoreServiceObject, StoreDetailEntity>()
                .ForMember(m => m.Store, obj => obj.Ignore())
                .ForMember(m => m.Owner, obj => obj.Ignore())
                .ForMember(m => m.Products, obj => obj.Ignore())
                .ForMember(m => m.StoreAddress, obj => obj.Ignore())
                .ForMember(m => m.AdditionalInformation, obj => obj.Ignore());

            // Store Entity -> StoreServiceObject
            this.CreateMap<StoreEntity, StoreServiceObject>()
                .ForMember(m => m.OwnerId, obj => obj.Ignore());
        }
    }
}
