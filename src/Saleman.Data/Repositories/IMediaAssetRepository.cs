namespace Saleman.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IMediaAssetRepository : IRepository, IAsyncDataLoaderRepository
    {
    }

    public interface IFileAssetRepository : IRepository, IAsyncDataLoaderRepository
    {

    }
}
