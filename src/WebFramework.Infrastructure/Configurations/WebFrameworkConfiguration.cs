using System;
using Microsoft.Extensions.Configuration;

namespace WebFramework.Infrastructure.Configurations
{
    public class WebFrameworkConfiguration : IWebFrameworkConfiguration
    {
        protected readonly IConfiguration Configuration;

        public WebFrameworkConfiguration(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public string AdminEmail
        {
            get { return Configuration.GetValue<string>("AdminEmail"); }
        }

        public string DefaultTimeZone
        {
            get { return Configuration.GetValue<string>("DefaultTimeZone"); }
        }

        public WebFrameworkClientSecrets Secrets
        {
            get
            {
                return new WebFrameworkClientSecrets
                {
                    ClientId = Configuration.GetValue<string>($"web:{WebFrameworkKeys.ClientId}"),
                    ClientSecret = Configuration.GetValue<string>($"web:{WebFrameworkKeys.ClientSecret}"),
                    AuthenUri = Configuration.GetValue<string>($"web:{WebFrameworkKeys.AuthenUri}"),
                    TokenUri = Configuration.GetValue<string>($"web:{WebFrameworkKeys.TokenUri}"),
                    RedirectUri = Configuration.GetValue<string>($"web:{WebFrameworkKeys.RedirectUri}")
                };
            }
        }

        public string[] CalendarScopes
        {
            get
            {
                var scopes = Configuration.GetValue<string>("CalendarScopes");

                return !string.IsNullOrEmpty(scopes) ? scopes.Split(',') : null;
            }
        }

        public string FileStorageFolder => Configuration.GetValue<string>("FileStorageFolder");
        public string CacheProvider => Configuration.GetValue<string>("CacheProvider");

        public int CacheExpiration => Configuration.GetValue<int>("CacheExpiration");
    }
}