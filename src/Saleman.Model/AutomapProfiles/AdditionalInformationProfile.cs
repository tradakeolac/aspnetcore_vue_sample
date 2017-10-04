namespace Saleman.Model.AutomapProfiles
{
    using Saleman.Model.Entities;
    using Saleman.Model.ServiceObjects;
    using AutoMapper;

    public class AdditionalInformationProfile : AutoMapper.Profile
    {
        public AdditionalInformationProfile()
        {
            this.CreateMap<SocialInformationEntity, AdditionalInformationServiceObject>()
                .ForMember(m => m.Type, obj => obj.UseValue("social"));
            this.CreateMap<EmailInformationEntity, AdditionalInformationServiceObject>()
                .ForMember(m => m.Type, obj => obj.UseValue("email"));
            this.CreateMap<GenericInformationEntity, AdditionalInformationServiceObject>()
                .ForMember(m => m.Type, obj => obj.UseValue("general"));
            this.CreateMap<PhoneInformationEntity, AdditionalInformationServiceObject>()
                .ForMember(m => m.Type, obj => obj.UseValue("phone"));

            this.CreateMap<AdditionalInformationServiceObject, EmailInformationEntity>();
            this.CreateMap<AdditionalInformationServiceObject, SocialInformationEntity>();
            this.CreateMap<AdditionalInformationServiceObject, GenericInformationEntity>();
            this.CreateMap<AdditionalInformationServiceObject, PhoneInformationEntity>();


            this.CreateMap<AdditionalInformationServiceObject, AdditionalInformationEntity>()
                .ConvertUsing(new AdditionalConverter());

        }

        private class AdditionalConverter : ITypeConverter<AdditionalInformationServiceObject, AdditionalInformationEntity>
        {
            public AdditionalInformationEntity Convert(AdditionalInformationServiceObject source, 
                AdditionalInformationEntity destination, ResolutionContext context)
            {
                switch(source.Type)
                {
                    case "email":
                        return context.Mapper.Map<EmailInformationEntity>(source);
                    case "phone":
                        return context.Mapper.Map<PhoneInformationEntity>(source);
                    case "social":
                        return context.Mapper.Map<SocialInformationEntity>(source);
                    case "general":
                        return context.Mapper.Map<GenericInformationEntity>(source);
                    default: return null;
                }
            }
        }
    }
}
