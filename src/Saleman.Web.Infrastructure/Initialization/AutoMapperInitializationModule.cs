namespace Saleman.Web.Infrastructure.Initialization
{
    using AutoMapper;
    using AutomapProfiles;
    using WebFramework.Infrastructure.Attributes;
    using WebFramework.Infrastructure.Helpers;
    using WebFramework.Infrastructure.Initialization;
    using System.Linq;

    [InitializableModule]
    public class AutoMapperInitializationModule : IInitializableModule
    {
        public void Initialize(InitializationContext context)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                var profiles = AssemblyHelper.LoadByType(typeof(Profile));

                if (profiles != null && profiles.Any())
                {
                    foreach (var profile in profiles)
                    {
                        cfg.AddProfile(profile);
                    }
                }
            });

            configuration.AssertConfigurationIsValid();

            context.Services.AddSingleton<IMapper>(configuration.CreateMapper());
        }

        public void UnInitialize(InitializationContext context)
        {
        }
    }
}