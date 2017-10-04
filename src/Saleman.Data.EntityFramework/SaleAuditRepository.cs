namespace Saleman.Data.EntityFramework
{
    using Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Model.Entities;
    using Specifications;
    using Microsoft.EntityFrameworkCore;

    public class SaleAuditRepository : EFCoreRepository, ISaleAuditRepository
    {
        public SaleAuditRepository(SalemanDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<SaleAuditEntity> GetSaleAuditWithSaleByAsync(ISpecification<SaleAuditEntity> criteria)
        {
            return await this.Find(criteria)
                            .Include(au => au.Sale)
                            .FirstOrDefaultAsync();
        }
    }
}
