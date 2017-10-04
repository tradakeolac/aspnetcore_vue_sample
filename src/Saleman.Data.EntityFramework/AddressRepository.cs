namespace Saleman.Data.EntityFramework
{
    using Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Specifications;
    using Model.Entities;
    using Microsoft.EntityFrameworkCore;

    public class AddressRepository : EFCoreRepository, IAddressRepository
    {
        public AddressRepository(SalemanDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<ProvinceEntity>> GetProvincesWithDistrictsAsync(ISpecification<ProvinceEntity> specification)
        {
            return await this.DbSet<ProvinceEntity>().Where(specification.ToExpression()).Include(p => p.Districts).ToListAsync();
        }
    }
}
