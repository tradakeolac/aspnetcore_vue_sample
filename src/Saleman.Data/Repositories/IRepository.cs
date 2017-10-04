namespace Saleman.Data.Repositories
{
    using Model.Entities;
    using Saleman.Data.Specifications;

    public interface IRepository : IDataLoaderRepository
    {
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        void Add<TEntity>(TEntity entity) where TEntity : EntityBase;

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        void Delete<TEntity>(TEntity entity) where TEntity : EntityBase;

        /// <summary>
        /// Deletes entities which satify specificatiion
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="criteria">The criteria.</param>
        void Delete<TEntity>(ISpecification<TEntity> criteria) where TEntity : EntityBase;

        /// <summary>
        /// Delete entity by its primary key
        /// </summary>
        /// <typeparam name="TEntity">The type of entity</typeparam>
        /// <param name="key">The primary key</param>
        void Delete<TEntity>(object key) where TEntity : EntityBase;

        /// <summary>
        /// Updates changes of the existing entity.
        /// The caller must later call SaveChanges() on the repository explicitly to save the entity to database
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        void Update<TEntity>(TEntity entity) where TEntity : EntityBase;


        /// <summary>
        /// Updates changes of the existing entity.
        /// The caller must later call SaveChanges() on the repository explicitly to save the entity to database
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        void Update<TEntity>(TEntity entity, TEntity newEntity) where TEntity : EntityBase;
    }
}