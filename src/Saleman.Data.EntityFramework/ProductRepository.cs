namespace Saleman.Data.EntityFramework
{
    using Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductRepository : EFCoreRepository, IProductRepository
    {
        public ProductRepository(SalemanDbContext dbContext) : base(dbContext)
        {
        }
    }
}
