namespace Saleman.Web.Infrastructure.Initialization
{
    using WebFramework.Infrastructure.Attributes;
    using WebFramework.Infrastructure.Helpers;
    using WebFramework.Infrastructure.Initialization;
    using Saleman.Model.Entities;
    using Saleman.Model.ServiceObjects;
    using Saleman.Web.Infrastructure.Factories;
    using Saleman.Web.Infrastructure.Helpers;
    using Saleman.Web.ViewModel;

    [InitializableModule]
    public class InitializationModule : IInitializableModule
    {
        public void Initialize(InitializationContext context)
        {
            // Factory
            context.Services.AddSingleton<IEntityFactory, AutoMapperObjectFactory>();
            context.Services.AddSingleton<IViewModelFactory, AutoMapperObjectFactory>();
            context.Services.AddSingleton<IServiceObjectFactory, AutoMapperObjectFactory>();
            context.Services.AddSingleton<IMediaAssetEntityFactory, MediaAssetEntityFactory>();
            context.Services.AddSingleton<ISaleAuditTransformer, SaleAuditTransformer>();

            // Utility
            context.Services.AddSingleton<IHttpUtility, WebUtilityAdapter>();
        }

        public void UnInitialize(InitializationContext context)
        {
        }
    }
}