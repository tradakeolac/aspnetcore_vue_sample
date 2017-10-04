namespace Saleman.Data.Repositories
{
    using Model.Entities;
    using Saleman.Data.Specifications;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAsyncDataLoaderRepository
    {
        /// <summary>
        /// Finds entities based on provided criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> FindAsync<TEntity>(ISpecification<TEntity> criteria) where TEntity : EntityBase;

        /// <summary>
        /// Finds one entity based on provided criteria.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        Task<TEntity> FindOneAsync<TEntity>(ISpecification<TEntity> criteria) where TEntity : EntityBase;

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync<TEntity>(object id) where TEntity : EntityBase;

        /// <summary>
        /// Get result with paging
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="criteria"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAsync<TEntity>(ISpecification<TEntity> criteria, int page, int pageSize) where TEntity : EntityBase;

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : EntityBase;

        /// <summary>
        /// Counts the specified entities.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns></returns>
        Task<int> CountAsync<TEntity>() where TEntity : EntityBase;

        /// <summary>
        /// Counts entities satifying specification.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
        Task<int> CountAsync<TEntity>(ISpecification<TEntity> criteria) where TEntity : EntityBase;
    }
}