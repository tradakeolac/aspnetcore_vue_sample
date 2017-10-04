namespace Saleman.Caching.RedisCaching
{
    using WebFramework.Infrastructure.Caching;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class RedisCachingProvider : ICacheProvider, IAsynCacheProvider
    {
        public T Fetch<T>(string key, Func<T> retrieveData, DateTime? absoluteExpiry = null, TimeSpan? relativeExpiry = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Fetch<T>(string key, Func<IEnumerable<T>> retrieveData, DateTime? absoluteExpiry = null, TimeSpan? relativeExpiry = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> FetchAsync<T>(string key, Func<Task<T>> retrieveData, DateTime? absoluteExpiry = null, TimeSpan? relativeExpiry = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> FetchAsync<T>(string key, Func<Task<IEnumerable<T>>> retrieveData, DateTime? absoluteExpiry = null, TimeSpan? relativeExpiry = null)
        {
            throw new NotImplementedException();
        }
    }
}
