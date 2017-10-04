namespace Saleman.Caching.Initialization
{
    using WebFramework.Infrastructure.Attributes;
    using WebFramework.Infrastructure.Caching;
    using WebFramework.Infrastructure.Initialization;

    [InitializableModule]
    public class InitializationModule : IInitializableModule
    {
        public void Initialize(InitializationContext context)
        {
            context.Services.AddSingleton<ICacheProvider, MemCaching.MemCachingProvider>(Constants.CacheProviderKeys.MemCachekey);
            context.Services.AddSingleton<IAsynCacheProvider, MemCaching.MemCachingProvider>(Constants.CacheProviderKeys.MemCachekey);

            context.Services.AddSingleton<ICacheProvider, RedisCaching.RedisCachingProvider>(Constants.CacheProviderKeys.RedisCacheKey);
            context.Services.AddSingleton<IAsynCacheProvider, RedisCaching.RedisCachingProvider>(Constants.CacheProviderKeys.RedisCacheKey);

            context.Services.AddSingleton<ICacheProvider, InMemoryCaching.InMemoryCachingProvider>(Constants.CacheProviderKeys.InMemoryCaching);
            context.Services.AddSingleton<IAsynCacheProvider, InMemoryCaching.InMemoryCachingProvider>(Constants.CacheProviderKeys.InMemoryCaching);
        }

        public void UnInitialize(InitializationContext context)
        {
        }
    }
}