namespace Saleman.Data.EntityFramework
{
    using Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MediaAssetRepository : EFCoreRepository, IMediaAssetRepository, IFileAssetRepository
    {
        public MediaAssetRepository(SalemanDbContext dbContext) : base(dbContext)
        {
        }
    }
}
