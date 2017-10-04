namespace WebFramework.Infrastructure.Configurations
{
    public interface IWebFrameworkConfiguration
    {
        WebFrameworkClientSecrets Secrets { get; }
        string AdminEmail { get; }
        string DefaultTimeZone { get; }
        string[] CalendarScopes { get; }
        string FileStorageFolder { get; }
        string CacheProvider { get; }
        int CacheExpiration { get; }
    }
}