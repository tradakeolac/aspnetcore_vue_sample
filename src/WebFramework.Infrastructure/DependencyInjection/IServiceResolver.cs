namespace WebFramework.Infrastructure.DependencyInjection
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public interface IServiceResolver<TService> where TService : class
    {
        TService Resolve(string key);
    }
}
