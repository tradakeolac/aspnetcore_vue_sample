﻿namespace Saleman.Data.Repositories
{
    using Model.Entities;
    using Specifications;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ISaleAuditRepository : IRepository
    {
        Task<SaleAuditEntity> GetSaleAuditWithSaleByAsync(ISpecification<SaleAuditEntity> criteria);
    }
}
