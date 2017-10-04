namespace Saleman.Web.Infrastructure.DependencyInjection
{
    using Autofac;
    using WebFramework.Infrastructure.Initialization;
    using WebFramework.Infrastructure.Metadata;
    using System;
    using System.Reflection;

    public class AutofacServiceCollection : IServiceCollectionBuilder
    {
        protected ContainerBuilder ContainerBuilder;

        public AutofacServiceCollection(ContainerBuilder builder)
        {
            ContainerBuilder = builder;
        }

        public void AddSingleton<TService>(object implementInstance) where TService : class
        {
            ContainerBuilder.RegisterInstance(implementInstance)
                .As<TService>()
                .SingleInstance();
        }

        public void AddSingleton<TService, TImplement>()
            where TService : class
            where TImplement : class
        {
            ContainerBuilder.RegisterType<TImplement>()
                            .As<TService>()
                            .InstancePerLifetimeScope();
        }

        public void AddScoped<TService>(object implementInstance) where TService : class
        {
            ContainerBuilder.RegisterInstance(implementInstance)
                .As<TService>()
                .InstancePerLifetimeScope();
        }

        public void AddScoped<TService>(object implementInstance, string key) where TService : class
        {
            ContainerBuilder.RegisterInstance(implementInstance)
                .As<TService>()
                .WithMetadata<ProviderKeyMetadata>(m => m.For(me => me.Provider, key))
                .InstancePerDependency();
        }

        public void AddScoped<TService, TImplement>()
            where TService : class
            where TImplement : class
        {
            ContainerBuilder.RegisterType<TImplement>()
                .As<TService>()
                .InstancePerLifetimeScope();
        }

        public void AddScoped<TService, TImplement>(string key)
            where TService : class
            where TImplement : class
        {
            ContainerBuilder.RegisterType<TImplement>()
                .As<TService>()
                .WithMetadata<ProviderKeyMetadata>(m => m.For(me => me.Provider, key))
                .InstancePerLifetimeScope();
        }

        public void AddScopedAssemblies(params Assembly[] assemblies)
        {
            ContainerBuilder.RegisterAssemblyTypes(assemblies)
                  .AsImplementedInterfaces()
                  .InstancePerLifetimeScope();
        }

        public void AddScopedAssemblies(string filter, params Assembly[] assemblies)
        {
            ContainerBuilder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.Contains(filter))
                  .AsImplementedInterfaces()
                  .InstancePerLifetimeScope();
        }

        public void AddScoped<TService>(Type implementType) where TService : class
        {
            ContainerBuilder.RegisterType(implementType).As<TService>().InstancePerLifetimeScope();
        }

        public void AddScoped<TService>(Type implementType, string key) where TService : class
        {
            ContainerBuilder.RegisterType(implementType)
                .As<TService>()
                .WithMetadata<ProviderKeyMetadata>(m => m.For(me => me.Provider, key))
                .InstancePerLifetimeScope();
        }

        public void AddSingleton<TService, TImplement>(string key)
            where TService : class
            where TImplement : class
        {
            ContainerBuilder.RegisterType<TImplement>()
                .As<TService>()
                .WithMetadata<ProviderKeyMetadata>(m => m.For(me => me.Provider, key))
                .SingleInstance();
        }

        public void AddSingleton<TService>(object implementInstance, string key) where TService : class
        {
            ContainerBuilder.RegisterInstance(implementInstance)
                .As<TService>()
                .WithMetadata<ProviderKeyMetadata>(m => m.For(me => me.Provider, key))
                .SingleInstance();
        }

        public void AddSingletonGeneric(Type service, Type implement)
        {
            ContainerBuilder.RegisterGeneric(implement)
                .As(service)
                .SingleInstance();
        }

        public void AddScopedAssemblies(string filter, string key, params Assembly[] assemblies)
        {
            ContainerBuilder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.Contains(filter))
                  .AsImplementedInterfaces()
                  .WithMetadata<ProviderKeyMetadata>(m => m.For(me => me.Provider, key))
                  .InstancePerLifetimeScope();
        }
    }
}