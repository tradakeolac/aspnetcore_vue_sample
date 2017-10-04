
namespace Saleman.Model.ServiceObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SaleServiceObject : ServiceObjectBase<long, SaleServiceObject>
    {
    }

    public class SaleAuditServiceObject : ServiceObjectBase<Guid, SaleAuditServiceObject>
    {
        public long SaleId { get; set; }
        public decimal TotalAmount { get; set; }
        public string SalesmanId { get; set; }
    }
}
