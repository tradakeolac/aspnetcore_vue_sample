using System;
namespace Saleman.Service
{
    using Model.ServiceObjects;
    using System.Threading.Tasks;

    public interface ISaleAuditService : IService<SaleAuditServiceObject, Guid>
    {
        Task<bool> ApproveAsync(Guid saleAuditId, string storeOwnerId);
    }
}