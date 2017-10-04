using System;
using System.Threading.Tasks;
using Saleman.Data.Repositories;
using Saleman.Data.Specifications;
using Saleman.Model.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;

namespace Saleman.Data.EntityFramework
{

    public class StoreRepository : EFCoreRepository, IStoreRepository
    {
        public StoreRepository(SalemanDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<StoreEntity>> GetStoresWithDetailAsync(ISpecification<StoreEntity> criteria)
        {
            return await this.Find(criteria)
                .Include(s => s.Detail).ToListAsync();
        }

        public async Task<StoreEntity> GetStoreWithOwnerByAsync(ISpecification<StoreEntity> criteria)
        {
            return await this.Find(criteria)
                .Include(s => s.Detail)
                .Include(s => s.Detail.Owner)
                .FirstOrDefaultAsync();
        }
    }
}