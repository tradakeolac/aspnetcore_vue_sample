namespace Saleman.Caching.MemCaching
{
    using Enyim.Caching;
    using WebFramework.Infrastructure.Caching;
    using WebFramework.Infrastructure.Ultility;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MemCachingProvider : ICacheProvider, IAsynCacheProvider
    {

        protected readonly IMemcachedClient Client;
        protected readonly IDateTimeAdapter DateTimeAdapter;

        public MemCachingProvider(IMemcachedClient client, IDateTimeAdapter dateTimeAdapter)
        {
            this.Client = client;
            this.DateTimeAdapter = dateTimeAdapter;
        }

        public virtual T Fetch<T>(string key, Func<T> retrieveData, DateTime? absoluteExpiry = null, TimeSpan? relativeExpiry = null)
        {
            return FetchAndCache<T>(key, retrieveData, absoluteExpiry, relativeExpiry);
        }

        public virtual IEnumerable<T> Fetch<T>(string key, Func<IEnumerable<T>> retrieveData, DateTime? absoluteExpiry = null, TimeSpan? relativeExpiry = null)
        {
            return FetchAndCache(key, retrieveData, absoluteExpiry, relativeExpiry);
        }

        public virtual Task<T> FetchAsync<T>(string key, Func<Task<T>> retrieveData, DateTime? absoluteExpiry = null, TimeSpan? relativeExpiry = null)
        {
            return FetchAndCacheAsync(key, retrieveData, absoluteExpiry, relativeExpiry);
        }

        public virtual Task<IEnumerable<T>> FetchAsync<T>(string key, Func<Task<IEnumerable<T>>> retrieveData, DateTime? absoluteExpiry = null, TimeSpan? relativeExpiry = null)
        {
            return FetchAndCacheAsync(key, retrieveData, absoluteExpiry, relativeExpiry);
        }


        #region Private and sub methods
        
        private T FetchAndCache<T>(string key, Func<T> retrieveData, DateTime? absoluteExpiry, TimeSpan? relativeExpiry)
        {
            T value;
            if (!TryGetValue(key, out value))
            {
                value = retrieveData != null ? retrieveData.Invoke() : default(T);
                this.Client.Add(key, value, ExpiredInSeconds(absoluteExpiry, relativeExpiry));
            }
            return value;
        }

        private async Task<T> FetchAndCacheAsync<T>(string key, Func<Task<T>> retrieveData, DateTime? absoluteExpiry, TimeSpan? relativeExpiry)
        {
            T value = await this.Client.GetValueAsync<T>(key);
            if (EqualityComparer<T>.Default.Equals(value, default(T)))
            {
                value = await retrieveData?.Invoke();
                await this.Client.AddAsync(key, value, ExpiredInSeconds(absoluteExpiry, relativeExpiry));
            }

            return await Task.FromResult(value);
        }

        private int ExpiredInSeconds(DateTime? absoluteExpirey, TimeSpan? relativeExpiry)
        {
            if (absoluteExpirey.HasValue)
                return (int)(absoluteExpirey.Value - DateTimeAdapter.Now).TotalSeconds;

            if (relativeExpiry.HasValue)
                return (int)relativeExpiry.Value.TotalSeconds;

            return 0;
        }

        private bool TryGetValue<T>(string key, out T value)
        {
            value = this.Client.Get<T>(key);

            if(value.Equals(default(T)))
            {
                return false;
            }

            return true;
        }
        

        #endregion
    }
}
