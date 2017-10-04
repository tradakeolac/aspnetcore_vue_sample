namespace Saleman.Model.AutomapProfiles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Entities;
    using ServiceObjects;

    public class SaleAuditProfile : Profile
    {
        public SaleAuditProfile()
        {
            CreateMap<SaleAuditEntity, PendingSaleAuditEntity>();
            CreateMap<SaleAuditEntity, ApprovedSaleAuditEntity>();

            CreateMap<SaleAuditServiceObject, PendingSaleAuditEntity>()
                .ForMember(sa => sa.Sale, obj => obj.Ignore())
                .ForMember(sa => sa.Saleman, obj => obj.Ignore())
                .ForMember(sa => sa.Created, obj => obj.Ignore())
                .ForMember(sa => sa.Updated, obj => obj.Ignore())
                .ForMember(sa => sa.SalemanId, obj => obj.MapFrom(src => src.SalesmanId))
                .ForMember(sa => sa.Total, obj => obj.MapFrom(src => src.TotalAmount));
        }
    }
}
