namespace WebFramework.Infrastructure.Initialization
{
    using System;
    using System.Reflection;

    public interface IServiceCollectionBuilder
    {
        void AddScoped<TService, TImplement>() where TService : class where TImplement : class;

        void AddScoped<TService, TImplement>(string key) where TService : class where TImplement : class;

        void AddScoped<TService>(object implementInstance) where TService : class;

        void AddScoped<TService>(object implementInstance, string key) where TService : class;

        void AddScoped<TService>(Type implementType) where TService : class;

        void AddScoped<TService>(Type implementType, string key) where TService : class;

        void AddScopedAssemblies(params Assembly[] assemblies);

        void AddScopedAssemblies(string filter, params Assembly[] assemblies);
        void AddScopedAssemblies(string filter, string key, params Assembly[] assemblies);

        void AddSingleton<TService, TImplement>() where TService : class where TImplement : class;

        void AddSingleton<TService, TImplement>(string key) where TService : class where TImplement : class;

        void AddSingleton<TService>(object implementInstance) where TService : class;

        void AddSingleton<TService>(object implementInstance, string key) where TService : class;

        void AddSingletonGeneric(Type service, Type implement);
    }
}