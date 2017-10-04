namespace Saleman.Caching.InMemoryCaching
{
    using Microsoft.Extensions.Caching.Memory;
    using WebFramework.Infrastructure.Caching;
    using WebFramework.Infrastructure.Ultility;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using WebFramework.Infrastructure.Configurations;

    public class InMemoryCachingProvider : ICacheProvider, IAsynCacheProvider
    {
        private readonly IMemoryCache Cache;
        private readonly IDateTimeAdapter DateTimeAdapter;
        private readonly IWebFrameworkConfiguration Configuration;

        public InMemoryCachingProvider(IMemoryCache cache, IDateTimeAdapter dateTimeAdapter, IWebFrameworkConfiguration configuration)
        {
            this.Cache = cache;
            this.DateTimeAdapter = dateTimeAdapter;
            this.Configuration = configuration;
        }

        public T Fetch<T>(string key, Func<T> retrieveData, DateTime? absoluteExpiry, TimeSpan? relativeExpiry = null)
        {
            return FetchAndCache(key, retrieveData, absoluteExpiry, relativeExpiry);
        }

        public IEnumerable<T> Fetch<T>(string key, Func<IEnumerable<T>> retrieveData, DateTime? absoluteExpiry, TimeSpan? relativeExpiry = null)
        {
            return FetchAndCache(key, retrieveData, absoluteExpiry, relativeExpiry);
        }

        public Task<T> FetchAsync<T>(string key, Func<Task<T>> retrieveData, DateTime? absoluteExpiry, TimeSpan? relativeExpiry = null)
        {
            return FetchAndCacheAsync(key, retrieveData, absoluteExpiry, relativeExpiry);
        }

        public Task<IEnumerable<T>> FetchAsync<T>(string key, Func<Task<IEnumerable<T>>> retrieveData, DateTime? absoluteExpiry, TimeSpan? relativeExpiry = null)
        {
            return FetchAndCacheAsync(key, retrieveData, absoluteExpiry, relativeExpiry);
        }

        private T FetchAndCache<T>(string key, Func<T> retrieveData, DateTime? absoluteExpiry = null, TimeSpan? relativeExpiry = null)
        {
            T cacheEntry;

            // Look for cache key.
            if (!Cache.TryGetValue(key, out cacheEntry))
            {
                cacheEntry = retrieveData != null ? retrieveData.Invoke() : default(T);

                // Set cache options.
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // Keep in cache for this time, reset time if accessed.
                    .SetSlidingExpiration(TimeSpan.FromSeconds(ExpiredInSeconds(absoluteExpiry, relativeExpiry)));

                // Save data in cache.
                Cache.Set(key, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry;
        }


        private async Task<T> FetchAndCacheAsync<T>(string key, Func<Task<T>> retrieveData, DateTime? absoluteExpiry, TimeSpan? relativeExpiry)
        {
            return await
            Cache.GetOrCreateAsync(key, entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(ExpiredInSeconds(absoluteExpiry, relativeExpiry));
                return retrieveData.Invoke();
            });
        }

        private int ExpiredInSeconds(DateTime? absoluteExpirey, TimeSpan? relativeExpiry)
        {
            if (absoluteExpirey.HasValue)
                return (int)(absoluteExpirey.Value - DateTimeAdapter.Now).TotalSeconds;

            if (relativeExpiry.HasValue)
                return (int)relativeExpiry.Value.TotalSeconds;

            return this.Configuration.CacheExpiration;
        }
    }
}
