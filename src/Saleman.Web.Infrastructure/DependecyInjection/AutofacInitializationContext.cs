namespace Saleman.Web.Infrastructure.DependencyInjection
{
    using WebFramework.Infrastructure.Initialization;

    public class AutofacInitializationContext : InitializationContext
    {
        public AutofacInitializationContext(IServiceCollectionBuilder services) : base(services)
        {
        }
    }
}