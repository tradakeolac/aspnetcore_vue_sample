namespace Saleman.Data.EntityFramework.Initialization
{
    using Saleman.Data.Repositories;
    using WebFramework.Infrastructure.Attributes;
    using WebFramework.Infrastructure.Initialization;
    using System.Collections.Generic;
    using System.Reflection;

    [InitializableModule]
    public class InitializationModule : IInitializableModule
    {
        public void Initialize(InitializationContext context)
        {
            // Unit Of work
            context.Services.AddScoped<IAsyncUnitOfWork, EFCoreUnitOfWork>(Constants.ServiceKeys.EntityFramework);
            context.Services.AddScoped<IUnitOfWork, EFCoreUnitOfWork>(Constants.ServiceKeys.EntityFramework);
            context.Services.AddScoped<SalemanDbContext, SalemanDbContext>(Constants.ServiceKeys.EntityFramework);

            // Repository
            var assemblies = new List<Assembly>();
            assemblies.Add(Assembly.Load(new AssemblyName("Saleman.Data")));
            assemblies.Add(Assembly.Load(new AssemblyName("Saleman.Data.EntityFramework")));
            context.Services.AddScopedAssemblies("Repository", Constants.ServiceKeys.EntityFramework, assemblies.ToArray());
        }

        public void UnInitialize(InitializationContext context)
        {
        }
    }
}
