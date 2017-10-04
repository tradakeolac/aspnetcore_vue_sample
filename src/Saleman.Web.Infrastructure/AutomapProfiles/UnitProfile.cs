namespace Saleman.Web.Infrastructure.AutomapProfiles
{
    using Model.Entities;
    using Model.ServiceObjects;
    using Saleman.Web.ViewModel;
    using System;

    public class UnitProfile : AutoMapper.Profile
    {
        public UnitProfile()
        {
            this.CreateMap<UnitViewModel, UnitServiceObject>()
                .ForMember(m => m.Id, obj => obj.MapFrom(src => Guid.NewGuid()));
            this.CreateMap<UnitServiceObject, UnitViewModel>();

            this.CreateMap<UnitServiceObject, UnitEntity>();
            this.CreateMap<UnitEntity, UnitServiceObject>();
        }
    }
}
