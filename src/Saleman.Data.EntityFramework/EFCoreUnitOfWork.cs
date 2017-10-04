namespace Saleman.Data.EntityFramework
{
    using Microsoft.EntityFrameworkCore;
    using Saleman.Data.Exceptions;
    using Saleman.Data.Repositories;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// The Entity Framework implementation of IUnitOfWork
    /// </summary>
    public sealed class EFCoreUnitOfWork : IUnitOfWork, IAsyncUnitOfWork
    {
        /// <summary>
        /// The DbContext
        /// </summary>
        private SalemanDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the UnitOfWork class.
        /// </summary>
        /// <param name="context">The object context</param>
        public EFCoreUnitOfWork(SalemanDbContext context)
        {
            _dbContext = context;
        }

        /// <summary>
        /// Check current in transaction
        /// </summary>
        public bool IsInTransaction
        {
            get
            {
                return _dbContext.Database.CurrentTransaction != null;
            }
        }

        /// <summary>
        /// Begin new transaction
        /// </summary>
        public void BeginTransaction()
        {
            if (_dbContext.Database.CurrentTransaction != null)
                _dbContext.Database.BeginTransaction();
        }

        public async Task BeginTransactionAsync()
        {
            if (_dbContext.Database.CurrentTransaction != null)
                await _dbContext.Database.BeginTransactionAsync();
        }

        public void CommitTransaction()
        {
            if (_dbContext.Database.CurrentTransaction != null)
                _dbContext.Database.CurrentTransaction.Commit();
        }

        public async Task CommitTransactionAsync()
        {
            if (_dbContext.Database.CurrentTransaction != null)
                await Task.Factory.StartNew(() => _dbContext.Database.CurrentTransaction.Commit());
        }

        /// <summary>
        /// Disposes the current object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Roll back current transaction
        /// </summary>
        public void RollBackTransaction()
        {
            // Save changes with the default options
            _dbContext.Database.CurrentTransaction.Rollback();
        }

        public async Task RollBackTransactionAsync()
        {
            await Task.Factory.StartNew(() => _dbContext.Database.CurrentTransaction.Rollback());
        }

        public async Task SaveChangeAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch(DbUpdateException ex)
            {
                throw PersistanceException.CreateDefault(ex);
            }
        }

        /// <summary>
        /// Save all changes
        /// </summary>
        public void SaveChanges()
        {
            try
            {
                // Save changes with the default options
                _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {

                throw PersistanceException.CreateDefault(ex);
            }
        }

        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    _dbContext = null;
                }
            }
        }
    }
}