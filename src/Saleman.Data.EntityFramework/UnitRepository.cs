
using Saleman.Data.Repositories;

namespace Saleman.Data.EntityFramework
{

    public class UnitRepository : EFCoreRepository, IUnitRepository
    {
        public UnitRepository(SalemanDbContext dbContext) : base(dbContext)
        {
        }
    }
}
