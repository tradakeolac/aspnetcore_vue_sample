namespace Saleman.Data.Repositories
{
    using System;
    using System.Threading.Tasks;

    public interface IAsyncUnitOfWork : IDisposable
    {
        /// <summary>
        /// Save change as asynonous
        /// </summary>
        /// <returns></returns>
        Task SaveChangeAsync();

        /// <summary>
        /// The begin transaction.
        /// </summary>
        Task BeginTransactionAsync();

        /// <summary>
        /// The roll back transaction.
        /// </summary>
        Task RollBackTransactionAsync();

        /// <summary>
        /// The commit transaction.
        /// </summary>
        Task CommitTransactionAsync();
    }
}