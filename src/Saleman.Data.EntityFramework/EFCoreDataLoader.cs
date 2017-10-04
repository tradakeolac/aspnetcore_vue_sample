namespace Saleman.Data.EntityFramework
{
    using Saleman.Data.Repositories;
    using Saleman.Data.Specifications;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Model.Entities;

    public class EFCoreDataLoader : IDataLoaderRepository, IAsyncDataLoaderRepository
    {
        protected SalemanDbContext DbContext;

        protected DbSet<TEntity> DbSet<TEntity>() where TEntity : EntityBase
        {
            return DbContext.Set<TEntity>();
        }

        public EFCoreDataLoader(SalemanDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public int Count<TEntity>() where TEntity : EntityBase
        {
            return this.DbSet<TEntity>().Count();
        }

        public int Count<TEntity>(ISpecification<TEntity> criteria) where TEntity : EntityBase
        {
            return this.DbSet<TEntity>().AsQueryable().Count(criteria.ToExpression());
        }

        public IQueryable<TEntity> Find<TEntity>(ISpecification<TEntity> criteria) where TEntity : EntityBase
        {
            return this.DbSet<TEntity>().Where(criteria.ToExpression());
        }

        public TEntity FindOne<TEntity>(ISpecification<TEntity> criteria) where TEntity : EntityBase
        {
            return this.DbSet<TEntity>().FirstOrDefault(criteria.ToExpression());
        }

        public IQueryable<TEntity> Get<TEntity>(ISpecification<TEntity> criteria, int page, int pageSize) where TEntity : EntityBase
        {
            return this.DbSet<TEntity>().Where(criteria.ToExpression()).Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : EntityBase
        {
            return DbSet<TEntity>().AsQueryable();
        }

        public TEntity GetById<TEntity>(object id) where TEntity : EntityBase
        {
            return DbSet<TEntity>().Find(id);
        }

        public async Task<IEnumerable<TEntity>> FindAsync<TEntity>(ISpecification<TEntity> criteria) where TEntity : EntityBase
        {
            return await DbSet<TEntity>().Where(criteria.ToExpression()).ToListAsync().ConfigureAwait(false);
        }

        public Task<TEntity> FindOneAsync<TEntity>(ISpecification<TEntity> criteria) where TEntity : EntityBase
        {
            return DbSet<TEntity>().FirstOrDefaultAsync(criteria.ToExpression());
        }

        public Task<TEntity> GetByIdAsync<TEntity>(object id) where TEntity : EntityBase
        {
            return DbSet<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAsync<TEntity>(ISpecification<TEntity> criteria, int page, int pageSize) where TEntity : EntityBase
        {
            return await this.DbSet<TEntity>().Where(criteria.ToExpression()).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : EntityBase
        {
            return await DbSet<TEntity>().ToListAsync();
        }

        public Task<int> CountAsync<TEntity>() where TEntity : EntityBase
        {
            return DbSet<TEntity>().CountAsync();
        }

        public Task<int> CountAsync<TEntity>(ISpecification<TEntity> criteria) where TEntity : EntityBase
        {
            return DbSet<TEntity>().CountAsync(criteria.ToExpression());
        }
    }
}