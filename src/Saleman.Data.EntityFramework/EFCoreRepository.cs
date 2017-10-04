namespace Saleman.Data.EntityFramework
{
    using Saleman.Data.Repositories;
    using Saleman.Data.Specifications;
    using Microsoft.EntityFrameworkCore;
    using System;
    using Model.Entities;

    public class EFCoreRepository : EFCoreDataLoader, IRepository
    {
        public EFCoreRepository(SalemanDbContext dbContext) : base(dbContext)
        {
        }

        public void Add<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            DbSet<TEntity>().Add(entity);
        }

        public void Delete<TEntity>(object key) where TEntity : EntityBase
        {
            var entity = this.GetById<TEntity>(key);
            DbSet<TEntity>().Remove(entity);
        }

        public void Delete<TEntity>(ISpecification<TEntity> criteria) where TEntity : EntityBase
        {
            var entities = this.Find(criteria);
            DbSet<TEntity>().RemoveRange(entities);
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            DbSet<TEntity>().Remove(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Update<TEntity>(TEntity entity, TEntity newEntity) where TEntity : EntityBase
        {
            DbContext.Entry(entity).CurrentValues.SetValues(newEntity);
        }
    }
}