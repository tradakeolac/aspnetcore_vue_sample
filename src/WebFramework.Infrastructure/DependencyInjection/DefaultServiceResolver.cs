namespace WebFramework.Infrastructure.DependencyInjection
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class DefaultServiceResolver<TService> : IServiceResolver<TService> where TService : class
    {
        protected readonly IEnumerable<Lazy<TService, Metadata.ProviderKeyMetadata>> LazyServices;

        public DefaultServiceResolver(IEnumerable<Lazy<TService, Metadata.ProviderKeyMetadata>> lazies)
        {
            this.LazyServices = lazies;
        }

        public virtual TService Resolve(string key)
        {
            if (LazyServices == null)
                return null;
            var service = this.LazyServices.FirstOrDefault(s => s.Metadata.Provider == key);
            return service?.Value;
        }
    }
}
