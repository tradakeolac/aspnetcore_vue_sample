namespace Saleman.Web.Infrastructure.AutomapProfiles
{
    using Saleman.Model.ServiceObjects;
    using Saleman.Web.ViewModel;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class StoreProfile : AutoMapper.Profile
    {
        public StoreProfile()
        {
            this.CreateMap<StoreViewModel, StoreServiceObject>();

            this.CreateMap<StoreServiceObject, StoreViewModel>()
                .ForMember(m => m.Addresses, obj => obj.Ignore());
        }
    }
}
