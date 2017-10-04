namespace WebFramework.Infrastructure.Caching
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ICacheProvider
    {
        T Fetch<T>(string key, Func<T> retrieveData, DateTime? absoluteExpiry = null, TimeSpan? relativeExpiry = null);
        IEnumerable<T> Fetch<T>(string key, Func<IEnumerable<T>> retrieveData, DateTime? absoluteExpiry = null, TimeSpan? relativeExpiry = null);
    }

    public interface IAsynCacheProvider
    {
        Task<T> FetchAsync<T>(string key, Func<Task<T>> retrieveData, DateTime? absoluteExpiry = null, TimeSpan? relativeExpiry = null);
        Task<IEnumerable<T>> FetchAsync<T>(string key, Func<Task<IEnumerable<T>>> retrieveData, DateTime? absoluteExpiry = null, TimeSpan? relativeExpiry = null);
    }
}
