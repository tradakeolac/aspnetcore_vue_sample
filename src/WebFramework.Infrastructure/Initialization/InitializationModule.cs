namespace WebFramework.Infrastructure.Initialization
{
    using Configurations;
    using WebFramework.Infrastructure.Attributes;
    using WebFramework.Infrastructure.DependencyInjection;
    using WebFramework.Infrastructure.Ultility;

    [InitializableModule]
    public class InitializationModule : IInitializableModule
    {
        public void Initialize(InitializationContext context)
        {
            // Register service
            context.Services.AddSingleton<IDateTimeAdapter, DateTimeAdapter>();
            context.Services.AddSingleton<IWebFrameworkConfiguration, WebFrameworkConfiguration>();
            context.Services.AddSingletonGeneric(typeof(IServiceResolver<>), typeof(DefaultServiceResolver<>));
        }

        public void UnInitialize(InitializationContext context)
        {
        }
    }
}