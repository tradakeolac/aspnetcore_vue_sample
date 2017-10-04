namespace Saleman.Data.EntityFramework
{
    using Repositories;
    public class AdditionalInformationRepository : EFCoreRepository, IAdditionalInformationRepository
    {
        public AdditionalInformationRepository(SalemanDbContext dbContext) : base(dbContext)
        {
        }
    }
}
