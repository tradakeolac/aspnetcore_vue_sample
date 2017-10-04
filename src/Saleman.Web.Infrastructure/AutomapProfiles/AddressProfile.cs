namespace Saleman.Web.Infrastructure.AutomapProfiles
{
    using AutoMapper;
    using Model.Entities;
    using Model.ServiceObjects;
    using Saleman.Web.ViewModel;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AddressProfile : AutoMapper.Profile
    {
        public AddressProfile()
        {
            this.CreateMap<DistrictEntity, LocationServiceObject>();
            this.CreateMap<ProvinceEntity, LocationServiceObject>()
                .ForMember(des => des.ParentLocationId, obj => obj.Ignore());
            this.CreateMap<AddressEntity, AddressServiceObject>()
                .ForMember(des => des.ParentLocationId, obj => obj.Ignore())
                .ForMember(des => des.District, obj => obj.MapFrom(src => Mapper.Map<LocationServiceObject>(src.District)))
                .ForMember(des => des.Province, obj => obj.MapFrom(src => Mapper.Map<LocationServiceObject>(src.District.ParentLocation)));

            this.CreateMap<LocationServiceObject, DistrictEntity>()
                .ForMember(des => des.ParentLocation, obj => obj.Ignore());

            this.CreateMap<LocationServiceObject, ProvinceEntity>()
                .ForMember(des => des.Districts, obj => obj.Ignore());
        }
    }

    public class AdditionalInformationProfile : Profile
    {
        public AdditionalInformationProfile()
        {
            this.CreateMap<AdditionalInformationServiceObject, AdditionalInformationViewModel>();
            this.CreateMap<AdditionalInformationViewModel, AdditionalInformationServiceObject> ();
        }
    }
}
