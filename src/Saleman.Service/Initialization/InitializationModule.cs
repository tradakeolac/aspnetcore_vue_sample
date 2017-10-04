namespace Saleman.Service.Initialization
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using WebFramework.Infrastructure.Attributes;
    using WebFramework.Infrastructure.Initialization;
    using System.Reflection;

    [InitializableModule]
    public class InitializationModule : IInitializableModule
    {
        public void Initialize(InitializationContext context)
        {
            var assembly = Assembly.Load(new AssemblyName("Saleman.Service"));

            context.Services.AddScopedAssemblies(nameof(Service), assembly);
            context.Services.AddScoped<UserManager<IdentityUser>, UserManager<IdentityUser>>();
            context.Services.AddScoped<SignInManager<IdentityUser>, SignInManager<IdentityUser>>();
        }

        public void UnInitialize(InitializationContext context)
        {
        }
    }
}