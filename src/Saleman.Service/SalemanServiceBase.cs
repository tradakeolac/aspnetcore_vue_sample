namespace Saleman.Service
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WebFramework.Infrastructure.Configurations;
    using Model.Entities;
    using Model.ServiceObjects;
    using Saleman.Data.Repositories;
    using System.Linq;
    using Exceptions;
    using Saleman.Data.Exceptions;

    public abstract class SalemanServiceBase
    {
        protected readonly IAsyncDataLoaderRepository AsyncDataLoader;
        protected readonly IAsyncUnitOfWork UnitOfWork;
        protected readonly IWebFrameworkConfiguration Configuration;
        protected readonly IServiceObjectFactory ObjectFactory;
        protected readonly IEntityFactory EntityFactory;
        protected readonly IRepository Repository;

        protected SalemanServiceBase(IAsyncUnitOfWork unitOfWork, IAsyncDataLoaderRepository dataLoader,
            IWebFrameworkConfiguration configuration, IRepository repository,
            IServiceObjectFactory objectFactory, IEntityFactory entityFactory)
        {
            this.UnitOfWork = unitOfWork;
            this.AsyncDataLoader = dataLoader;
            this.Configuration = configuration;
            this.ObjectFactory = objectFactory;
            this.EntityFactory = entityFactory;
            this.Repository = repository;
        }
    }

    public abstract class SalemanServiceBase<TServiceObject, TKey, TEntity> : SalemanServiceBase, IService<TServiceObject, TKey>
        where TServiceObject : ServiceObjectBase<TKey, TServiceObject>, new()
        where TEntity : EntityBase
    {

        protected SalemanServiceBase(IAsyncUnitOfWork unitOfWork, IAsyncDataLoaderRepository dataLoader,
            IWebFrameworkConfiguration configuration, IRepository repository,
            IServiceObjectFactory objectFactory, IEntityFactory entityFactory)
            : base(unitOfWork, dataLoader, configuration, repository, objectFactory, entityFactory)
        {

        }

        public virtual async Task<TServiceObject> AddAsync(TServiceObject serviceObject)
        {
            var entity = this.EntityFactory.Create<TEntity>(serviceObject);

            this.Repository.Add(entity);

            await SaveChangeAsync<AddActionException>();

            return this.ObjectFactory.Create<TServiceObject>(entity);
        }

        public virtual async Task<IEnumerable<TServiceObject>> GetAllAsync()
        {
            var entities = await this.AsyncDataLoader.GetAllAsync<TEntity>();
            var serviceObjects = new List<TServiceObject>();
            if (entities != null && entities.Any())
            {
                serviceObjects.AddRange(entities.Select(s => this.ObjectFactory.Create<TServiceObject>(s)));
            }

            return serviceObjects;
        }

        public virtual async Task<TServiceObject> GetByIdAsync(TKey id)
        {
            var entity = await this.AsyncDataLoader.GetByIdAsync<TEntity>(id);
            if (entity == null)
                return ServiceObjectBase<TKey, TServiceObject>.NullServiceObject;

            return this.ObjectFactory.Create<TServiceObject>(entity);
        }

        public virtual async Task<TServiceObject> UpdateAsync(TServiceObject serviceObject)
        {
            var entity = await this.AsyncDataLoader.GetByIdAsync<TEntity>(serviceObject.Id);

            if (entity == null)
                throw new ArgumentException().ToBusinessException<NotFoundObjectException>($"Can not find the Unit object with id {serviceObject.Id}.");

            var newEntity = this.EntityFactory.Create<TEntity>(serviceObject);

            this.Repository.Update(entity, newEntity);

            await SaveChangeAsync<UpdateActionException>();

            return serviceObject;
        }

        public virtual async Task<ResultServiceObject> DeleteAsync(TKey id)
        {
            this.Repository.Delete<TEntity>(id);

            await SaveChangeAsync<DeleteActionException>();

            return ResultServiceObject.Success;
        }
        
        public virtual async Task<ResultServiceObject> DeleteAsync(IEnumerable<TKey> ids)
        {
            foreach(var id in ids)
            {
                this.Repository.Delete<TEntity>(id);
            }

            await SaveChangeAsync<DeleteActionException>();

            return ResultServiceObject.Success;
        }

        protected virtual async Task SaveChangeAsync<TBusinessException>()
            where TBusinessException : BusinessException
        {
            try
            {
                await this.UnitOfWork.SaveChangeAsync();
            }
            catch (PersistanceException ex)
            {
                throw ex.ToBusinessException<TBusinessException>();
            }
        }
    }
}