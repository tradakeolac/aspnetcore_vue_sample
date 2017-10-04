namespace WebFramework.Infrastructure.Initialization
{
    public class InitializationContext
    {
        public InitializationContext(IServiceCollectionBuilder services)
        {
            this.Services = services;
        }

        public IServiceCollectionBuilder Services { get; }
    }
}