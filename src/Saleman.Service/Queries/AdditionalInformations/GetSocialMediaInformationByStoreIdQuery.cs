namespace Saleman.Service.Queries.AdditionalInformations
{
    using System;
    using System.Linq.Expressions;
    using Saleman.Data.Specifications;
    using Saleman.Model.Entities;

    public class GetSocialMediaInformationByStoreIdQuery : ExpressionQueryBaseSpecification<AdditionalInformationEntity>
    {
        private readonly Guid StoreId;

        public GetSocialMediaInformationByStoreIdQuery(Guid storeId)
        {
            this.StoreId = storeId;
        }

        public override Expression<Func<AdditionalInformationEntity, bool>> Expression => entity => entity is SocialInformationEntity && entity.StoreDetailId == StoreId;
    }

    public class GetStoreMediaInformationByStoreIdQuery : ExpressionQueryBaseSpecification<AdditionalInformationEntity>
    {
        private readonly Guid StoreId;

        public GetStoreMediaInformationByStoreIdQuery(Guid storeId)
        {
            this.StoreId = storeId;
        }

        public override Expression<Func<AdditionalInformationEntity, bool>> Expression
        {
            get
            {
                return entity => entity.StoreDetailId == StoreId && (entity is SocialInformationEntity || entity is EmailInformationEntity || entity is PhoneInformationEntity);
            }
        }
    }
}
